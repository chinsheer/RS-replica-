using System.Collections;
using Unity.Mathematics;
using UnityEngine;
[CreateAssetMenu(fileName = "Spin Laser", menuName = "Scriptable Objects/Attacks/Spin Laser")]
public class SpinLaser : AttackData
{
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float width = 2f;
    [SerializeField] private float length = 10f;
    [SerializeField] private float _speed = 60f;
    [SerializeField] private float StartAngle = 0f;

    private Vector2 _lastDirection;

    public override IEnumerator Indicator(IBossContext ctx)
    {
        GameObject indicator = Instantiate(_indicatorPrefab, ctx.Boss.position, Quaternion.identity);
        SpriteRenderer indicatorSprite = indicator.GetComponent<SpriteRenderer>();
        indicatorSprite.size = new Vector2(length, width);
        indicatorSprite.color = new Color(1f, 0f, 0f, 0.3f);
        indicator.layer = LayerMask.NameToLayer("EnemyAttackIndicator");

        indicator.transform.Rotate(0f, 0f, StartAngle);
        
        float elapsedTime = 0f;
        while (elapsedTime < ChargeTime)
        {
            elapsedTime += Time.deltaTime;
            indicator.transform.Rotate(0f, 0f, RotateAngle(Time.deltaTime));
            yield return null;
        }
        _lastDirection = indicator.transform.right;
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
        laserSprite.color = Color.red;
        laser.transform.right = _lastDirection;

        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = 1,
        };
        laser.GetComponent<Laser>().Damage = damageAttribute;
        laser.layer = LayerMask.NameToLayer("EnemyAttack");

        float elapsedTime = 0f;
        while (elapsedTime < ActiveTime)
        {
            elapsedTime += Time.deltaTime;
            laser.transform.Rotate(0f, 0f, RotateAngle(Time.deltaTime));
            yield return null;
        }

        if(laser != null)
        {
            Destroy(laser);
        }
    }

    public override IEnumerator Recover(IBossContext ctx)
    {
        yield return new WaitForSeconds(RecoverTime);
    }

    float RotateAngle(float time)
    {
        return _speed * time;
    }
}
