using UnityEngine;

[CreateAssetMenu(fileName = "BossBulletPath", menuName = "Scriptable Objects/BossAttacks/BossBulletPath")]
public class BossBulletPath : BossAttackData
{
    public GameObject BulletPrefab;

    public BulletPathType PathType;
    public float BulletSpeed = 10f;
    public enum BulletPathType
    {
        Straight,
        SineWave,
        Circular,
        ConeMaze
    }

    public override EffectHandle Indicator(GameObject boss, Transform target)
    {
        // Implementation for the indicator of the bullet path attack
        return new EffectHandle(
            update: deltaTime => { },
            cleanup: () => { }
        );
    }

    public override EffectHandle Execute(GameObject boss, Transform target)
    {
        // Implementation for executing the bullet path attack
        if(PathType == BulletPathType.ConeMaze) return ConeMazePattern(boss.transform, target);
        return null;
    }

    private EffectHandle ConeMazePattern(Transform boss, Transform target)
    {
        float coneAngle = 90f;
        int bulletAmount = 10;
        Vector3 targetAngle = (target.position - boss.position).normalized;
        Vector3 floorDirection = Quaternion.Euler(0, 0, -coneAngle/2) * targetAngle;
        Vector3 ceilDirection = Quaternion.Euler(0, 0, coneAngle/2) * targetAngle;
        Quaternion bulletDirectionIncrement = Quaternion.Euler(0, 0, coneAngle / bulletAmount);

        Vector3 currentVector = floorDirection;
        float timeElapsed = 0f;
        int currentBullet = 0;
        float bulletTimeSection = ActiveTime/(bulletAmount - 1);


        void Update(float dt)
        {
            if(currentBullet >= bulletAmount) return;
            while(timeElapsed >= bulletTimeSection * currentBullet)
            {
                SpawnBullet(boss.position, currentVector);
                currentVector = bulletDirectionIncrement * currentVector;
                currentBullet++;
            }
            timeElapsed += dt;
        }

        return new EffectHandle(
            update: Update,
            cleanup: () => { }
        );
    }

    private GameObject SpawnBullet(Vector3 position, Vector3 vector)
    {
        GameObject bullet = Instantiate(BulletPrefab, position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity =  vector * BulletSpeed;
        bullet.transform.position = position;
        return bullet;
    }
}
