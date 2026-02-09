using UnityEngine;
using System.Collections;

public class BossMinionSpawn : BossAttackData
{
    public GameObject MinionPrefab;

    public override IEnumerator Indicator(GameObject boss, Transform target)
    {
        // No indicator for minion spawn
        return null;
    }
    public override IEnumerator Execute(GameObject boss, Transform target)
    {
        GameObject minion = Instantiate(MinionPrefab, boss.transform.position, Quaternion.identity);
        // Additional setup for the minion can be done here

        yield return new WaitForSeconds(ActiveTime);
    }
}
