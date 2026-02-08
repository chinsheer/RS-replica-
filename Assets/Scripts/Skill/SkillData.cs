using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Objects/SkillData")]
public abstract class SkillData : ScriptableObject
{
    public float CooldownTime;
    public float ChargeTime;
    public float ExecuteTime;
    public float RecoveryTime;
    public Sprite SkillIcon;
    
    public abstract EffectHandle Charge(GameObject user);
    public abstract EffectHandle Execute(GameObject user);
    public abstract EffectHandle Recover(GameObject user);
}
