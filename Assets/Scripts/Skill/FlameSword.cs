using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FlameSword", menuName = "Scriptable Objects/Skills/FlameSword")]
public class FlameSword : SkillData
{
    public GameObject SwordPrefab;
    public Color SwordColor = new Color(1f, 181/255f, 0f);
    public float Damage = 20f;

    public override EffectHandle Charge(GameObject user)
    {
        return null;
    }

    public override EffectHandle Execute(GameObject user)
    {
        Vector3 spawnPosition = user.transform.position + new Vector3(1f, 0f, 0f);
        GameObject swordInstance = Instantiate(SwordPrefab, spawnPosition, user.transform.rotation);
        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = Damage,
            DamageSource = user
        };
        swordInstance.GetComponent<SpriteRenderer>().color = SwordColor;
        swordInstance.GetComponent<Melee>().Damage = damageAttribute;
        swordInstance.GetComponent<Fade>().FadeOut(0.3f);
        swordInstance.layer = LayerMask.NameToLayer("PlayerAttack");
        return null;
    }

    public override EffectHandle Recover(GameObject user)
    {
        return null;
    }
}
