using UnityEngine;

public interface IBossContext
{
    Transform Boss { get; }
    Transform Player { get; }
    MonoBehaviour Runner { get; }

    float currentHealth { get; }
    float maxHealth { get; }
}