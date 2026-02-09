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
    [SerializeField] private float _coneAngle = 90f;
    public enum BulletPathType
    {
        Straight,
        SineWave,
        Circular,
        ConeMaze
    }

    public override IEnumerator Indicator(GameObject boss, Transform target)
    {
        // Implementation for the indicator of the bullet path attack
        return null;
    }

    public override IEnumerator Execute(GameObject boss, Transform target)
    {
        // Implementation for executing the bullet path attack
        if(_pathType == BulletPathType.ConeMaze) return ConeMazePattern(boss.transform, target);
        return null;
    }

    private IEnumerator ConeMazePattern(Transform boss, Transform target)
    {
        if(_bulletPrefab == null || _bulletAmount <= 0)
        {
            if (ActiveTime > 0f)
                yield return new WaitForSeconds(ActiveTime);
            yield break;
        }

        Vector3 targetAngle = (target.position - boss.position).normalized;
        Vector3 floorDirection = Quaternion.Euler(0, 0, -_coneAngle/2) * targetAngle;
        Vector3 ceilDirection = Quaternion.Euler(0, 0, _coneAngle/2) * targetAngle;
        Quaternion bulletDirectionIncrement = Quaternion.Euler(0, 0, _coneAngle / _bulletAmount);

        Vector3 currentVector = floorDirection;
        float timeElapsed = 0f;
        int currentBullet = 0;
        float bulletTimeSection = ActiveTime/(_bulletAmount - 1);

        while (timeElapsed < ActiveTime)
        {
            if (currentBullet >= _bulletAmount) yield break;
            while(timeElapsed >= bulletTimeSection * currentBullet)
            {
                SpawnBullet(boss.position, currentVector);
                currentVector = bulletDirectionIncrement * currentVector;
                currentBullet++;
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    private GameObject SpawnBullet(Vector3 position, Vector3 vector)
    {
        GameObject bullet = Instantiate(_bulletPrefab, position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity =  vector * _bulletSpeed;
        bullet.transform.position = position;
        bullet.layer = LayerMask.NameToLayer(_enemyAttackLayerName);
        return bullet;
    }
}
