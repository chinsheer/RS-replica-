using UnityEngine;

[CreateAssetMenu(fileName = "WeightRule", menuName = "Scriptable Objects/WeightRule")]
public abstract class WeightRule : ScriptableObject
{
    public abstract int ModifyWeight(IBossContext ctx, PatternEdgeInstance edge);

    public virtual bool Block(IBossContext ctx, PatternEdgeInstance edge) => false;
}