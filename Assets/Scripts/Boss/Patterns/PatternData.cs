using UnityEngine;

[CreateAssetMenu(fileName = "PatternData", menuName = "Scriptable Objects/PatternData")]
public class PatternData : ScriptableObject
{
    public AttackData[] Attacks;
    public PatternEdgeData[] Edges;
}
