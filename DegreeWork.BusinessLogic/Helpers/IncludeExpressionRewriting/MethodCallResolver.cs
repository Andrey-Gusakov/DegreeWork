using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers.IncludeExpressionRewriting
{
    class MethodCallResolver : IExpressionResolver
    {
        private MethodCallExpression expression;
        private IExpressionResolver innerResolver;

        public MethodCallResolver(MethodCallExpression expression, Func<Expression, IExpressionResolver> resolverBuilder)
        {
            this.expression = expression;
            Expression firstArgExpr = expression.Arguments[0];
            this.innerResolver = resolverBuilder(firstArgExpr);
        }

        public bool IsReachType(Type type)
        {
            return innerResolver.IsReachType(type);
        }

        public Expression UnderlyingExpression
        {
            get { return innerResolver.UnderlyingExpression; }
        }

        public Expression UpdateExpression(Expression newGetter)
        {
            List<Expression> arguments = expression.Arguments.ToList();
            arguments[0] = innerResolver.UpdateExpression(newGetter);
            return expression.Update(null, arguments);
        }
    }
}
