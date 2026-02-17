using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Wall Manipulate", menuName = "Scriptable Objects/Attacks/Wall Manipulate")]
public class WallManipulate : AttackData
{
    public Wall.WallState WallState;
    public bool ToActivate = false;
    public bool ToDeactivate = false;
    public bool IsPlayerWall = false;

    public override IEnumerator Indicator(IBossContext ctx)
    {
        if(ToActivate)
        {
            if (IsPlayerWall)
            {
                ctx.PlayerWallInstance.ActivateWall(0);
                ctx.PlayerWallInstance.ActivateWall(1);
                ctx.PlayerWallInstance.ActivateWall(2);
                ctx.PlayerWallInstance.ActivateWall(3);
            }
            else
            {
                ctx.WallInstance.ActivateWall(0);
                ctx.WallInstance.ActivateWall(1);
                ctx.WallInstance.ActivateWall(2);
                ctx.WallInstance.ActivateWall(3);
            }
        }
        yield return new WaitForSeconds(ChargeTime);
    }

    public override IEnumerator Execute(IBossContext ctx)
    {
        if (IsPlayerWall)
        {
            yield return ctx.PlayerWallInstance.Transition(ActiveTime, WallState);
        }
        else
        {
            yield return ctx.WallInstance.Transition(ActiveTime, WallState);
        }
    }
    public override IEnumerator Recover(IBossContext ctx)
    {
        if(ToDeactivate)
        {
            if (IsPlayerWall)
            {
                ctx.PlayerWallInstance.DeactivateWall(0);
                ctx.PlayerWallInstance.DeactivateWall(1);
                ctx.PlayerWallInstance.DeactivateWall(2);
                ctx.PlayerWallInstance.DeactivateWall(3);
            }
            else
            {
                ctx.WallInstance.DeactivateWall(0);
                ctx.WallInstance.DeactivateWall(1);
                ctx.WallInstance.DeactivateWall(2);
                ctx.WallInstance.DeactivateWall(3);
            }
        }
        yield return new WaitForSeconds(RecoverTime);
    }
}