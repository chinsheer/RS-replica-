using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SpriteRenderer))]
public class CircleOutlined : MonoBehaviour
{
    public int segments = 100;
    public Color color = Color.white;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LineRenderer line = GetComponent<LineRenderer>();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        float radius = sprite.sprite.bounds.extents.x;
        line.widthMultiplier = 0.05f;
        line.startColor = color;
        line.endColor = color;

        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += 360f / segments;
        }   
    }

}
