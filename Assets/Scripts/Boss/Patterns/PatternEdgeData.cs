using UnityEngine;

[CreateAssetMenu(fileName = "PatternEdgeData", menuName = "Scriptable Objects/PatternEdgeData")]
public class PatternEdgeData : ScriptableObject
{
    public PatternData NextPattern;
    public int BaseWeight;
}
