using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;
[CreateAssetMenu(fileName = "Extend Sword", menuName = "Scriptable Objects/Attacks/Extend Sword")]
public class ExtendSword : AttackData
{
    public GameObject _indicatorPrefab;
    public GameObject _swordPrefab;
    public float width = 2f;
    public Vector3 StartPosition;
    // Static list to keep track of spawned swords and the last sword instance for chaining purposes
    private Vector2 _lastDirection;
    private Vector2 _lastHitPoint;
    private GameObject _currentSwordInstance;
    public Vector2 LastHitPoint => _lastHitPoint;
    public override IEnumerator Indicator(IBossContext ctx)
    {
        GameObject indicator = Instantiate(_indicatorPrefab, StartPosition, Quaternion.identity);
        Vector3 direction = (ctx.Player.position - StartPosition).normalized;
        _lastDirection = direction;
        SpriteRenderer indicatorSprite = indicator.GetComponent<SpriteRenderer>();
        BoxCollider2D indicatorCollider = indicator.GetComponent<BoxCollider2D>();
        indicatorSprite.drawMode = SpriteDrawMode.Sliced;

        // Calculate the collision point with the border to determine the length of the sword
        Vector2 hitPoint = CalculateBorderCollision(ctx);
        _lastHitPoint = hitPoint;
        float length = Vector2.Distance(StartPosition, _lastHitPoint);
        indicator.transform.right = direction;
        indicatorSprite.size = new Vector2(length * 3f, width);
        indicatorCollider.size = new Vector2(length * 3f, width);
        indicatorSprite.color = new Color(1f, 0f, 0f, 0.3f);
        indicator.layer = LayerMask.NameToLayer("EnemyAttackIndicator");

        yield return new WaitForSeconds(ChargeTime);
        if(indicator != null)
        {
            Destroy(indicator);
        }
    }

    public override IEnumerator Execute(IBossContext ctx)
    {
        GameObject sword = Instantiate(_swordPrefab, StartPosition, Quaternion.identity);
        SpriteRenderer swordSprite = sword.GetComponent<SpriteRenderer>();
        BoxCollider2D swordCollider = sword.GetComponent<BoxCollider2D>();
        swordSprite.drawMode = SpriteDrawMode.Sliced;
        swordSprite.size = new Vector2(Vector2.Distance(StartPosition, _lastHitPoint) * 3f, width);
        swordCollider.size = new Vector2(Vector2.Distance(StartPosition, _lastHitPoint) * 3f, width);
        sword.transform.right = _lastDirection;
        swordSprite.color = new Color(1f, 0f, 0f, 1f);

        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = 1,
        };
        // Don't have sword asset yet, so using laser for now. Change this when sword is ready.
        sword.GetComponent<Laser>().Damage = damageAttribute;
        sword.layer = LayerMask.NameToLayer("EnemyAttack");
        StartPosition = _lastHitPoint;
        _currentSwordInstance = sword;

        yield return new WaitForSeconds(ActiveTime);
    }

    public override IEnumerator Recover(IBossContext ctx)
    {
        yield return new WaitForSeconds(RecoverTime);
    }

    public void CleanUp()
    {
        Destroy(_currentSwordInstance);
    }

    private Vector2 CalculateBorderCollision(IBossContext ctx)
    {
        Vector2 direction = (ctx.Player.position - StartPosition).normalized;
        RaycastHit2D[] hits = Physics2D.RaycastAll(StartPosition, direction, Mathf.Infinity, LayerMask.GetMask("Wall"));
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                // Skip the very close hit (distance 0 or very small)
                if (hit.distance > 1f) return hit.point;
            }
            return hits[0].point;
        }
        return StartPosition; // Fallback point if no collision
    }

}
