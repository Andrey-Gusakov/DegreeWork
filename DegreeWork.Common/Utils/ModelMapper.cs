using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using DegreeWork.Common.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Utils
{
    public class ModelMapper : IModelMapper
    {
        private MappingRegistration configurator;

        public ModelMapper(MappingRegistration configurator)
        {
            this.configurator = configurator;
        }

        /*public IWrappedEntity<TDestination> MapToWrapper<TDestination>(object obj) where TDestination : class
        {
            TDestination entity = Map<TDestination>(obj);
            IWrappedEntity<TDestination> result = null;

            if(entity != null)
                result = new WrappedEntity<TDestination>(entity, obj);

            return result;
        }*/

        public TDestination Map<TDestination>(object obj) where TDestination : class
        {
            if(obj == null)
                return null;

            Type firstType = ObjectContext.GetObjectType(obj.GetType());
            Type secondType = ObjectContext.GetObjectType(typeof(TDestination));

            Delegate mapper = configurator.GetDelegate(firstType, secondType);

            TDestination result = null;
            if(mapper != null)
                result = mapper.DynamicInvoke(obj) as TDestination;

            return result;
        }

        public TDestination MapWithSecurity<TDestination, TSource>(TSource obj)
            where TDestination : class, ISecureEntity
            where TSource : ISecurityContextHolder
        {
            TDestination result = Map<TDestination>(obj);
            result.User = new Entities.User() {
                Id = obj.UserContext.Id,
                Name = obj.UserContext.Name
            };
            return result;
        }

        public Entities.User MapSerurity<TSource>(TSource obj) where TSource : ISecurityContextHolder
        {
            /*IUserRepository user = repositoryGetter();
            user.*/
            return new Entities.User() {
                Id = obj.UserContext.Id,
                Name = obj.UserContext.Name
            };
        }
    }
}
