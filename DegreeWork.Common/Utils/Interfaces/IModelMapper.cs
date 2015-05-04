using DegreeWork.Common.Entities;
using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Utils.Interfaces
{
    public interface IModelMapper
    {
        //IWrappedEntity<TDestination> MapToWrapper<TDestination>(object obj) where TDestination : class;

        TDestination Map<TDestination>(object obj) where TDestination : class;

        TDestination MapWithSecurity<TDestination, TSource>(TSource obj) 
            where TDestination : class, ISecureEntity
            where TSource: ISecurityContextHolder;

        User MapSerurity<TSource>(TSource obj) where TSource : ISecurityContextHolder;
    }
}
