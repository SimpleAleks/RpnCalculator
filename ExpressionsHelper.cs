using System.Linq.Expressions;

namespace RpnCalculator;

public static class ExpressionsHelper
{
    public static string CastCharToOperationName(char op)
    {
        return op switch
        {
            '+' => nameof(Expression.Add),
            '*' => nameof(Expression.Multiply),
            _ => throw new NotImplementedException()
        };
    }
    
    
    
    
    
    
    
    
    
    public static Expression<Func<int>> ConvertToExpression(this string stringExpression)
    {
        int cursor = 0;
        var shareStorage = new List<Expression>();
        var numberQueue = new Queue<int>();
        var isLastOp = false;
        while (cursor < stringExpression.Length)
        {
            var atom = stringExpression[cursor];
            if (char.IsDigit(atom))
            {
                numberQueue.Enqueue(Int32.Parse(atom.ToString()));
                isLastOp = false;
            }
            else if (Operations.Contains(atom))
            {
                if (isLastOp)
                {
                    
                }
                else
                {
                    shareStorage.Add(SelectAndBuildOperation(atom, numberQueue));
                }
                isLastOp = true;
            }
            cursor++;
        }
        
        var body = Expression.Add(
            shareStorage[0],
            shareStorage[1]);
        return Expression.Lambda<Func<int>>(body, false, null);
    }

    private static Expression SelectAndBuildOperation(char atom, Queue<int> numberQueue)
    {
        return atom switch
        {
            '+' => CreateAddExpression(numberQueue),
            '*' => CreateMultiplicationExpression(numberQueue),
            _ => throw new NotImplementedException()
        };
    }

    private static Expression CreateMultiplicationExpression(Queue<int> numberQueue)
    {
        if (numberQueue.Count < 2) throw new ArgumentException(null, nameof(numberQueue));

        const int intType = 0;
        Expression? expression = null;
        while (numberQueue.Any())
        {
            if (expression is null)
            {
                expression = Expression.Multiply(
                    Expression.Constant(numberQueue.Dequeue(), intType.GetType()),
                    Expression.Constant(numberQueue.Dequeue(), intType.GetType()));
                continue;
            }
            expression = Expression.Multiply(
                expression,
                Expression.Constant(numberQueue.Dequeue(), intType.GetType()));
        }
        
        return expression ?? throw new NullReferenceException(nameof(expression));
    }

    private static Expression CreateAddExpression(Queue<int> numberQueue)
    {
        if (numberQueue.Count < 2) throw new ArgumentException(null, nameof(numberQueue));
        
        const int intType = 0;
        Expression? expression = null;
        while (numberQueue.Any())
        {
            if (expression is null)
            {
                expression = Expression.Add(
                    Expression.Constant(numberQueue.Dequeue(), intType.GetType()),
                    Expression.Constant(numberQueue.Dequeue(), intType.GetType()));
                continue;
            }
            expression = Expression.Add(
                expression,
                Expression.Constant(numberQueue.Dequeue(), intType.GetType()));
        }

        return expression ?? throw new NullReferenceException(nameof(expression));
    }

    private static IEnumerable<char> Operations
    {
        get
        {
            yield return '+';
            yield return '*';
        }
    }
}