using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public IDamageable DamageOwner { get; private set; }
    public IStatusReceiver StatusOwner { get; private set; }

    void Awake()
    {
        DamageOwner = GetComponentInParent<IDamageable>();
        StatusOwner = GetComponentInParent<IStatusReceiver>();
    }

    public void ChangeLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
}