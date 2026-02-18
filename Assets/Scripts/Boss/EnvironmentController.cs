using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public Wall WallInstance;
    public Wall PlayerWallInstance;

    public void Awake()
    {
        if (WallInstance == null) WallInstance = GameObject.Find("Border").GetComponent<Wall>();
        if (PlayerWallInstance == null) PlayerWallInstance = GameObject.Find("PlayerBorder").GetComponent<Wall>();
    }
}
