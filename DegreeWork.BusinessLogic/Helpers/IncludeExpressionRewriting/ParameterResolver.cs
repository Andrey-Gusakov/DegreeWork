using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers.IncludeExpressionRewriting
{
    class ParameterResolver : IExpressionResolver
    {
        private ParameterExpression expression;

        public ParameterResolver(ParameterExpression expression)
        {
            this.expression = expression;
        }

        public bool IsReachType(Type type)
        {
            return expression.Type == type;
        }

        public Expression UnderlyingExpression
        {
            get { throw new NotSupportedException(); }
        }

        public Expression UpdateExpression(Expression newGetter)
        {
            throw new NotSupportedException();
        }
    }
}
