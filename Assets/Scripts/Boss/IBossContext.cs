using UnityEngine;

public interface IBossContext
{
    Transform Boss { get; }
    Transform Player { get; }
    SpriteRenderer BossSR { get; }
    MonoBehaviour Runner { get; }
    Wall WallInstance { get; }
    Wall PlayerWallInstance { get; }

    float currentHealth { get; }
    float maxHealth { get; }
}