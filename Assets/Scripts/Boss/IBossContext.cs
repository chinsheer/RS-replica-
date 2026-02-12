using UnityEngine;

public interface IBossContext
{
    Transform Boss { get; }
    Transform Player { get; }

    float currentHealth { get; }
    float maxHealth { get; }
}