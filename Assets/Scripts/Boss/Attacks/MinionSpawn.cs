using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Minion Spawn", menuName = "Scriptable Objects/Attacks/Minion Spawn")]
public class BossMinionSpawn : AttackData
{
    [SerializeField] private MinionData _minionData;
    [SerializeField] private Vector2 _spawnOffset;

    public override IEnumerator Indicator(IBossContext ctx)
    {
        // No indicator for minion spawn
        return null;
    }

    public override IEnumerator Execute(IBossContext ctx)
    {
        GameObject minion = Instantiate(_minionData.Prefab, ctx.Boss.position, Quaternion.identity);
        MinionController controller = minion.GetComponent<MinionController>();
        Vector2 randomOffset = Random.insideUnitCircle.normalized * 4f;
        controller.Initialize(_minionData, ctx.Player, (Vector2)ctx.Boss.position + _spawnOffset + randomOffset);
        // Additional setup for the minion can be done here

        yield return new WaitForSeconds(ActiveTime);
    }

    public override IEnumerator Recover(IBossContext ctx)
    {
        yield return new WaitForSeconds(RecoverTime);
    }
}
