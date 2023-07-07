namespace RpnCalculator.Exceptions;

public class LeafLevelNotEqual : Exception
{
    public LeafLevelNotEqual()
    {
    }

    public LeafLevelNotEqual(string? message) : base(message)
    {
    }
}