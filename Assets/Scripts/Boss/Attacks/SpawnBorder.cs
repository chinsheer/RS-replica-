using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Spawn Border", menuName = "Scriptable Objects/Attacks/Spawn Border")]
public class SpawnBorder : AttackData
{
    [SerializeField] private GameObject _borderPrefab;
    [SerializeField] private float _duration = 5f;

    public override IEnumerator Indicator(IBossContext ctx)
    {
        yield return null;
    }

    public override IEnumerator Execute(IBossContext ctx)
    {
        GameObject border = Instantiate(_borderPrefab, ctx.Boss.position, Quaternion.identity);
        border.transform.localScale = new Vector3(20f, 20f, 1f);
        SpriteRenderer borderSprite = border.GetComponent<SpriteRenderer>();
        borderSprite.color = Color.red;
        border.layer = LayerMask.NameToLayer("Wall");

        DamageAttribute damageAttribute = new DamageAttribute
        {
            DamageAmount = 1,
        };
    

        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        if(border != null)
        {
            Destroy(border);
        }
    }    

    public override IEnumerator Recover(IBossContext ctx)
    {
        yield return new WaitForSeconds(RecoverTime);
    }
}
