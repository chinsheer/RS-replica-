using UnityEngine;

[CreateAssetMenu(fileName = "Missile", menuName = "Scriptable Objects/Skills/Missile")]
public class Missile : SkillData
{
    public GameObject MissilePrefab;
    public float Speed = 5f;
    public Color MissileColor = new Color(1f, 181/255f, 0f);
    public float Damage = 20f;
    public int MissileAmount = 2;

    public override EffectHandle Charge(GameObject user)
    {
        return null;
    }

    public override EffectHandle Execute(GameObject user)
    {
        GameObject target = GameObject.FindGameObjectWithTag("Enemy");
        if (target == null)
        {
            return null;
        }

        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = Damage
        };

        GameObject[] instance = new GameObject[MissileAmount];

        for (int i = 0; i < MissileAmount; i++)
        {
            GameObject missileInstance = Instantiate(MissilePrefab, user.transform.position, user.transform.rotation);
            Vector2 StartVelocity = Random.insideUnitCircle.normalized * Speed * 0.3f;
            missileInstance.GetComponent<Rigidbody2D>().linearVelocity = StartVelocity;
            missileInstance.GetComponent<SpriteRenderer>().color = MissileColor;
            HomingProjectile homingProjectile = missileInstance.GetComponent<HomingProjectile>();
            homingProjectile.Target = target.transform;
            homingProjectile.Speed = Speed;
            homingProjectile.Damage = damageAttribute;
            instance[i] = missileInstance;
            missileInstance.layer = LayerMask.NameToLayer("PlayerAttack");
        }
        return null;
    }

    public override EffectHandle Recover(GameObject user)
    {
        return null;
    }
}
