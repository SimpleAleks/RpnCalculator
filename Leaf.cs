using System.Linq.Expressions;
using RpnCalculator.Exceptions;

namespace RpnCalculator;

public class Leaf
{
    private readonly Expression _content;
    private readonly int _level;
    
    public Leaf(Leaf[] leaves, char operation)
    {
        if (!leaves.Any()) throw new ArgumentException(nameof(leaves));
        if (!leaves.LeavesLevelEqual()) throw new LeafLevelNotEqual("Leaves level must be equal!");
        
        
    }

    private Expression LeavesToExpression(Leaf[] leaves, int start, int end, string operation)
    {
        if (start == end)return leaves[0]._content;
        var avg = (end - start + 1) / 2;
        return typeof(Expression).GetMethod(operation).Invoke(
            null,
            LeavesToExpression(leaves, )
            )
    }

    public int Level => _level;

    public Expression Content => _content;
}