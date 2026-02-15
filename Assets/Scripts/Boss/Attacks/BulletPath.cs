using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "BulletPath", menuName = "Scriptable Objects/Attacks/BossBulletPath")]
public class BulletPath : AttackData
{
    [Header("Bullet")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 3f;
    [SerializeField] private string _enemyAttackLayerName = "EnemyAttack";

    [Header("Pattern")]
    [SerializeField] private BulletPathType _pathType = BulletPathType.ConeMaze;
    [SerializeField] private int _bulletAmount = 10;
    [SerializeField] private float _sectorAngle = 90f;
    public enum BulletPathType
    {
        Straight,
        RandomSpread,
        Circular,
        ConeMaze,
        StraightBurst,
    }

    public override IEnumerator Indicator(IBossContext ctx)
    {
        // Implementation for the indicator of the bullet path attack
        return null;
    }

    public override IEnumerator Execute(IBossContext ctx)
    {
        // Implementation for executing the bullet path attack
        if(_pathType == BulletPathType.ConeMaze) return ConeMazePattern(ctx);
        if(_pathType == BulletPathType.StraightBurst) return StraightBurstPattern(ctx);
        if(_pathType == BulletPathType.RandomSpread) return RandomSpreadPattern(ctx);

        return null;
    }

    private IEnumerator RandomSpreadPattern(IBossContext ctx)
    {
        Vector3 targetAngle = (ctx.Player.position - ctx.Boss.position).normalized;
        float floorAngle = -_sectorAngle / 2;
        float ceilAngle = _sectorAngle / 2;

        float elapsedTime = 0f;
        int currentBullet = 0;
        float bulletTimeSection = ActiveTime / _bulletAmount;
        while (elapsedTime < ActiveTime)
        {
            if (currentBullet >= _bulletAmount) yield break;
            while (elapsedTime >= bulletTimeSection * currentBullet)
            {
                float randomAngle = Random.Range(floorAngle, ceilAngle);
                Vector3 randomDirection = Quaternion.Euler(0, 0, randomAngle) * targetAngle;
                SpawnBullet(ctx.Boss.position, randomDirection);
                currentBullet++;
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    private IEnumerator ConeMazePattern(IBossContext ctx)
    {
        if(_bulletPrefab == null || _bulletAmount <= 0)
        {
            if (ActiveTime > 0f)
                yield return new WaitForSeconds(ActiveTime);
            yield break;
        }

        Vector3 targetAngle = (ctx.Player.position - ctx.Boss.position).normalized;
        Vector3 floorDirection = Quaternion.Euler(0, 0, -_sectorAngle/2) * targetAngle;
        Vector3 ceilDirection = Quaternion.Euler(0, 0, _sectorAngle/2) * targetAngle;
        Quaternion bulletDirectionIncrement = Quaternion.Euler(0, 0, _sectorAngle / _bulletAmount);

        Vector3 currentVector = floorDirection;
        float timeElapsed = 0f;
        int currentBullet = 0;
        float bulletTimeSection = ActiveTime/(_bulletAmount - 1);

        while (timeElapsed < ActiveTime)
        {
            if (currentBullet >= _bulletAmount) yield break;
            while(timeElapsed >= bulletTimeSection * currentBullet)
            {
                SpawnBullet(ctx.Boss.position, currentVector);
                currentVector = bulletDirectionIncrement * currentVector;
                currentBullet++;
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    private IEnumerator StraightBurstPattern(IBossContext ctx)
    {
        if(_bulletPrefab == null || _bulletAmount <= 0)
        {
            if (ActiveTime > 0f)
                yield return new WaitForSeconds(ActiveTime);
            yield break;
        }

        Vector3 targetDirection = (ctx.Player.position - ctx.Boss.position).normalized;

        float timeElapsed = 0f;
        float bulletTimeSection = ActiveTime/(_bulletAmount - 1);
        int currentBullet = 0;
        while (timeElapsed >= bulletTimeSection * currentBullet)
        {
            SpawnBullet(ctx.Boss.position, targetDirection);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private GameObject SpawnBullet(Vector3 position, Vector3 vector)
    {
        GameObject bullet = Instantiate(_bulletPrefab, position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity =  vector * _bulletSpeed;
        bullet.transform.position = position;
        bullet.transform.right = vector;
        bullet.layer = LayerMask.NameToLayer(_enemyAttackLayerName);
        return bullet;
    }
}
