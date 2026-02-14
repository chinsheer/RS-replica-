using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BossPhase
{
    [Range(0f, 1f)] public float HPThreshold;
    public PatternData StartPattern;
    public PatternData[] AllPatterns;
}
