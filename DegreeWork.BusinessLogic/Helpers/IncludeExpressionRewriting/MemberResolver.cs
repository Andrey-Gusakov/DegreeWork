using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DegreeWork.BusinessLogic.Helpers.IncludeExpressionRewriting
{
    class MemberResolver : IExpressionResolver
    {
        private MemberExpression expression;

        public MemberResolver(MemberExpression expression)
        {
            this.expression = expression;
        }

        public bool IsReachType(Type type)
        {
            return expression.Member.DeclaringType == type;
        }

        public Expression UnderlyingExpression
        {
            get { return expression.Expression; }
        }

        public Expression UpdateExpression(Expression newGetter)
        {
            return expression.Update(newGetter);
        }
    }
}
