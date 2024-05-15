using Builder4You.Exceptions;
using Builder4You.Interfaces;
using System.Linq.Expressions;
using System.Reflection;

namespace Builder4You.Implementations
{
    public class Builder<TResource> : IBuilder<TResource>, IBuilderAsync<TResource>
        where TResource : IBuildeable, new()
    {
        protected TResource? Resource;
        protected List<Action<TResource>> Actions = [];
        protected List<Task> Tasks = [];
        public TResource Create()
        {
            Resource ??= new TResource();
            Actions.ForEach(action => action(Resource));

            return Resource;
        }

        public Task<TResource> CreateAsync()
        {
            return Task.WhenAll(Tasks)
                .ContinueWith(allCompleted =>
                {
                    switch (allCompleted.Status)
                    {
                        case TaskStatus.Faulted:
                            throw allCompleted.Exception!;
                        case TaskStatus.Canceled:
                            // TODO: CreateAsync cancellable
                            break;
                        // TODO: Analyze the rest of possible task.status
                    }

                    return Create();
                });
        }

        public IBuilder<TResource> With<TProperty>(Expression<Func<TResource, TProperty>> property, TProperty value)
        {
            if (property.Body.NodeType == ExpressionType.MemberAccess)
            {
                var member = (property.Body as MemberExpression)!.Member;
                if (member.MemberType == MemberTypes.Property)
                {
                    var get = (member as PropertyInfo)!.GetMethod;
                    var set = (member as PropertyInfo)!.SetMethod;

                    if (set is not null)
                        Actions.Add(Builder<TResource>.BuildSetPropertyAction(member.Name, value));
                    else
                        throw new PropertyNotSetteableException(member.Name);
                } 
                else if (member.MemberType == MemberTypes.Field)
                {
                    var fieldInfo = member as FieldInfo;
                    if (fieldInfo!.IsPublic)
                        Actions.Add(Builder<TResource>.BuildSetFieldAction(fieldInfo.Name, value));
                    else
                        throw new FieldNotPublicException(fieldInfo.Name);
                }
                else
                {
                    throw new Exception($"It is not a property access. Access Type: {member.MemberType}");
                }
            }
            else
            {
                throw new RequiredMemberAccessException(property);
            }

            return this;
        }

        public IBuilderAsync<TResource> WithAsync<TProperty>(Expression<Func<TResource, TProperty>> property, Task<TProperty> valueTask)
        {
            Tasks.Add(valueTask.ContinueWith(completed =>
            {
                With(property, valueTask.Result);
            }));

            return this;
        }

        IBuilderAsync<TResource> IBuilderAsync<TResource>.With<TProperty>(Expression<Func<TResource, TProperty>> property, TProperty value)
        {
            With(property, value); 
            
            return this;
        }

        private static Action<TResource> BuildSetPropertyAction<TProperty>(string propertyName, TProperty value)
        {
            return (TResource resource) 
                => resource.GetType().GetProperty(propertyName)!.SetValue(resource, value);
        }

        private static Action<TResource> BuildSetFieldAction<TProperty>(string fieldName, TProperty value)
        {
            return (TResource resource)
                => resource.GetType().GetField(fieldName)!.SetValue(resource, value);
        }
    }
}
