using UnityEngine;

[CreateAssetMenu(fileName = "BossLaser", menuName = "Scriptable Objects/BossAttacks/BossLaser")]
public class BossLaser : BossAttackData
{
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float width = 2f;
    [SerializeField] private float length = 10f;

    private Vector2 _lastDirection;

    public override EffectHandle Indicator(GameObject boss, Transform target)
    {
        _lastDirection = (target.position - boss.transform.position).normalized;
        GameObject indicator = Instantiate(_indicatorPrefab, boss.transform.position, Quaternion.identity);
        indicator.transform.right = _lastDirection;
        SpriteRenderer indicatorSprite = indicator.GetComponent<SpriteRenderer>();
        indicatorSprite.size = new Vector2(length, width);
        indicatorSprite.color = new Color(1f, 0f, 0f, 0.3f);

        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = 0,
            DamageSource = boss
        };
        indicator.GetComponent<Laser>().Damage = damageAttribute;

        return new EffectHandle(
            update: deltaTime => { },
            cleanup: () => Destroy(indicator)
        );
    }

    public override EffectHandle Execute(GameObject boss, Transform target)
    {
        GameObject laser = Instantiate(_laserPrefab, boss.transform.position, Quaternion.identity);
        SpriteRenderer laserSprite = laser.GetComponent<SpriteRenderer>();
        laserSprite.size = new Vector2(length, width);
        laserSprite.color = Color.red;
        laser.transform.right = _lastDirection;
        laser.GetComponent<Fade>().FadeOut(1f);

        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = 1,
            DamageSource = boss
        };
        laser.GetComponent<Laser>().Damage = damageAttribute;
        laser.layer = LayerMask.NameToLayer("EnemyAttack");

        return new EffectHandle(
            update: deltaTime => { },
            cleanup: () => Destroy(laser)
        );
    }
}
