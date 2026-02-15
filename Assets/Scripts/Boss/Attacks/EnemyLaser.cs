using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Laser", menuName = "Scriptable Objects/Attacks/Laser")]
public class EnemyLaser : AttackData
{
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float width = 2f;
    [SerializeField] private float length = 10f;
    [SerializeField] private float fadeOutDuration = 0.3f;

    private Vector2 _lastDirection;

    public override IEnumerator Indicator(IBossContext ctx)
    {
        _lastDirection = (ctx.Player.position - ctx.Boss.position).normalized;
        GameObject indicator = Instantiate(_indicatorPrefab, ctx.Boss.position, Quaternion.identity);
        indicator.transform.right = _lastDirection;
        SpriteRenderer indicatorSprite = indicator.GetComponent<SpriteRenderer>();
        indicatorSprite.size = new Vector2(length, width);
        indicatorSprite.color = new Color(1f, 0f, 0f, 0.3f);

        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = 0
        };
        indicator.GetComponent<Laser>().Damage = damageAttribute;
        indicator.layer = LayerMask.NameToLayer("EnemyAttackIndicator");

        yield return new WaitForSeconds(ChargeTime);
        if(indicator != null)
        {
            Destroy(indicator);
        }
    }

    public override IEnumerator Execute(IBossContext ctx)
    {
        GameObject laser = Instantiate(_laserPrefab, ctx.Boss.position, Quaternion.identity);
        SpriteRenderer laserSprite = laser.GetComponent<SpriteRenderer>();
        laserSprite.size = new Vector2(length, width);
        laser.transform.right = _lastDirection;
        laserSprite.color = new Color(1f, 0f, 0f, 1f);
        laser.GetComponent<Fade>().FadeOut(fadeOutDuration);

        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = 1,
        };
        laser.GetComponent<Laser>().Damage = damageAttribute;
        laser.layer = LayerMask.NameToLayer("EnemyAttack");

        yield return new WaitForSeconds(ActiveTime);
        if(laser != null)
        {
            Destroy(laser);
        }
    }
}
