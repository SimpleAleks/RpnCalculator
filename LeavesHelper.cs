namespace RpnCalculator;

public static class LeavesHelper
{
    public static bool LeavesLevelEqual(this Leaf[] leaves)
    {
        var level = leaves[0].Level;
        return leaves.All(l => l.Level == level);
    }
}