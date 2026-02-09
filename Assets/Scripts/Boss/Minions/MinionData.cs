using UnityEngine;

[CreateAssetMenu(fileName = "MinionData", menuName = "Scriptable Objects/MinionData")]
public class MinionData : ScriptableObject
{
    [Header("Prefab")]
    public GameObject prefab;

    [Header("Stats")]
    public int maxHp = 20;

    [Header("Attack")]
    public AttackData[] attacks;
}
