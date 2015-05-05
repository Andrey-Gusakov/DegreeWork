using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers.IncludeExpressionRewriting
{
    interface IExpressionResolver
    {
        bool IsReachType(Type type);
        Expression UnderlyingExpression { get; }
        Expression UpdateExpression(Expression newGetter);
    }
}
