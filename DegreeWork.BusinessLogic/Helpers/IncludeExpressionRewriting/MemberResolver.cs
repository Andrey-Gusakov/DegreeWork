using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers.IncludeExpressionRewriting
{
    class MemberResolver : IExpressionResolver
    {
        private MemberExpression expression;

        public MemberResolver(MemberExpression expression)
        {
            this.expression = expression;
        }

        public override bool IsReachType(Type type)
        {
            return expression.Member.DeclaringType == type;
        }

        public override Expression UnderlyingExpression
        {
            get { return expression.Expression; }
        }

        public override Expression UpdateExpression(Expression newGetter)
        {
            return expression.Update(newGetter);
        }
    }
}
