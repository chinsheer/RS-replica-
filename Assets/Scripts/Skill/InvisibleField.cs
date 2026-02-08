using UnityEngine;

[CreateAssetMenu(fileName = "InvisibleField", menuName = "Scriptable Objects/Skills/InvisibleField")]
public class InvisibleField : SkillData
{
    public float Duration = 5.0f;
    public GameObject InvincibleZonePrefab;

    public override EffectHandle Charge(GameObject user)
    {
        return null;
    }

    public override EffectHandle Execute(GameObject user)
    {
        GameObject zoneInstance = Instantiate(InvincibleZonePrefab, user.transform.position, Quaternion.identity);
        GameObject.Destroy(zoneInstance, Duration);
        return null;
    }

    public override EffectHandle Recover(GameObject user)
    {
        return null;
    }
}
