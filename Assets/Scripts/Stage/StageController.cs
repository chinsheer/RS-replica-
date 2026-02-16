using System.Collections;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private BossData[] _bossDatas;
    [SerializeField] private GameObject _player;

    [SerializeField] private BossHealthUI _bossHealthUI;
    [SerializeField] private int _startIndex = 0;

    private void Start()
    {
        StartCoroutine(RunStage());
    }

    private IEnumerator RunStage()
    {
        for (int i = _startIndex; i < _bossDatas.Length; i++)
        {
            BossData bossData = _bossDatas[i];
            GameObject bossGO = Instantiate(bossData.BossPrefab, bossData.SpawnPosition, Quaternion.identity);
            EnemyController enemyController = bossGO.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.Initialize(bossData, _player);
            }
            else
            {
                Debug.LogError("Boss prefab does not have an EnemyController component.");
            }

            EnemyHealth enemyHealth = bossGO.GetComponent<EnemyHealth>();
            _bossHealthUI.Initialize(enemyHealth);
            bool bossDefeated = false;
            if (enemyHealth != null)
            {
                enemyHealth.OnDeath += () => { bossDefeated = true; };
            }
            else
            {
                Debug.LogError("Boss prefab does not have an EnemyHealth component.");
                bossDefeated = true; // Skip waiting if no health component
            }

            while (!bossDefeated)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2f);
        }
    }
}
