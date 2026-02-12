public sealed class PatternEdgeInstance
{
    public PatternInstance Next { get; }
    public WeightRule[] Rules;
    private readonly PatternEdgeData _edge;

    public PatternEdgeInstance(PatternInstance next, PatternEdgeData edge)
    {
        Next = next;
        _edge = edge;
    }

    public int GetWeight(IBossContext ctx)
    {
        if (Next == null) return 0;

        int w = _edge.BaseWeight;

        if (Rules != null)
        {
            for (int i = 0; i < Rules.Length; i++)
            {
                var rule = Rules[i];
                if (rule == null) continue;

                if (rule.Block(ctx, this))
                    return 0;

                w += rule.ModifyWeight(ctx, this);
            }
        }
        return w < 0 ? 0 : w;
    }
}