using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SummonAsteroids", menuName = "Scriptable Objects/Attacks/Summon Asteroids")]
public class SummonAsteroids : AttackData
{
    [SerializeField] private GameObject _asteroidPrefab;
    [SerializeField] private int _numberOfAsteroids = 50;

    public override IEnumerator Indicator(IBossContext ctx)
    {
        yield return new WaitForSeconds(ChargeTime); // Example indicator time
    }

    public override IEnumerator Execute(IBossContext ctx)
    {
        // Instantiate the asteroid at the boss's position
        GameObject asteroid = Instantiate(_asteroidPrefab, ctx.Boss.position, Quaternion.identity);

        int currentAsteroids = 0;
        float elapsedTime = 0f;
        float spawnInterval = ActiveTime / _numberOfAsteroids; // Time between each asteroid spawn
        while (currentAsteroids < _numberOfAsteroids)
        {
            float nextSpawnTime = spawnInterval * currentAsteroids;
            while (elapsedTime < nextSpawnTime)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            // Instantiate the asteroid at the boss's position
            asteroid = Instantiate(_asteroidPrefab, ctx.Boss.position, Quaternion.identity);
            Rigidbody2D rb = asteroid.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Add a random force to the asteroid to make it move
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                float randomForce = Random.Range(1f, 2f);
                rb.AddForce(randomDirection * randomForce, ForceMode2D.Impulse);
            }
            currentAsteroids++;
        }
        yield return null;
    }

    public override IEnumerator Recover(IBossContext ctx)
    {
        // Recovery logic after the attack can go here
        yield return new WaitForSeconds(RecoverTime); // Example recovery time
    }
}
