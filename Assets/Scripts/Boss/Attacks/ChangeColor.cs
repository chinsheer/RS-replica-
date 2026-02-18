using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Change Color", menuName = "Scriptable Objects/Attacks/Change Color")]
public class ChangeColor : AttackData
{
    public Color color;

    public override IEnumerator Indicator(IBossContext ctx)
    {
        // No indicator needed for color change, but you could add one here if desired
        yield return new WaitForSeconds(ChargeTime);
    }

    public override IEnumerator Execute(IBossContext ctx)
    {
        float elapsedTime = 0f;
        Color colorDiff = color - ctx.BossSR.color;
        while(elapsedTime < ActiveTime)
        {
            ctx.BossSR.color += colorDiff * (Time.deltaTime / ActiveTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    public override IEnumerator Recover(IBossContext ctx)
    {
        yield return new WaitForSeconds(RecoverTime);
    }
}
