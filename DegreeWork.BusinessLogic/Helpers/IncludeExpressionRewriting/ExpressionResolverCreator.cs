using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers.IncludeExpressionRewriting
{
    public class ExpressionResolverCreator
    {
        public static IExpressionResolver Create(Expression expression)
        {
            if(expression is MemberExpression)
                return new MemberResolver(expression as MemberExpression);
            else if(expression is MethodCallExpression)
                return new MethodCallResolver(expression as MethodCallExpression, ExpressionResolverCreator.Create);
            else if(expression is ParameterExpression)
                return new ParameterResolver(expression as ParameterExpression);
            else
                throw new ArgumentException("Unsupported");
        }
    }
}
