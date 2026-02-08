using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public IDamageable Owner { get; private set; }

    void Awake()
    {
        Owner = GetComponentInParent<IDamageable>();
    }
}