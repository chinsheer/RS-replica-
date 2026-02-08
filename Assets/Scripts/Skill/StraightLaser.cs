using UnityEngine;

[CreateAssetMenu(fileName = "StraightLaser", menuName = "Scriptable Objects/Skills/StraightLaser")]
public class StraightLaser : SkillData
{
    public float width = 0.2f;
    public float length = 60f;
    public float Damage = 50f;
    public GameObject LaserPrefab;
    public Color LaserColor = new Color(1f, 181/255f, 0f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override EffectHandle Charge(GameObject user)
    {
        return null;
    }

    public override EffectHandle Execute(GameObject user)
    {
        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = Damage,
            DamageSource = user
        };
        
        Vector3 spawnPosition = user.transform.position + new Vector3(0f, 0.2f, 0f);
        GameObject laserInstance = Instantiate(LaserPrefab, spawnPosition, user.transform.rotation);
        laserInstance.transform.localScale = new Vector3(length, width, 1);
        laserInstance.GetComponent<SpriteRenderer>().color = LaserColor;
        laserInstance.GetComponent<Fade>().FadeOut(0.3f);
        laserInstance.GetComponent<Laser>().Damage = damageAttribute;
        laserInstance.layer = LayerMask.NameToLayer("PlayerAttack");
        return null;
    }

    public override EffectHandle Recover(GameObject user)
    {
        return null;
    }
}
