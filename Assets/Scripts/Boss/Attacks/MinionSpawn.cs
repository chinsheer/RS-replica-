using UnityEngine;
using System.Collections;

public class BossMinionSpawn : AttackData
{
    [SerializeField] private GameObject _minionPrefab;

    public override IEnumerator Indicator(GameObject boss, Transform target)
    {
        // No indicator for minion spawn
        return null;
    }

    public override IEnumerator Execute(GameObject boss, Transform target)
    {
        GameObject minion = Instantiate(_minionPrefab, boss.transform.position, Quaternion.identity);
        // Additional setup for the minion can be done here

        yield return new WaitForSeconds(ActiveTime);
    }

    public IEnumerator LaserMinion(Transform target)
    {
        while (true)
        {
            yield return null;
        }
    }
}
