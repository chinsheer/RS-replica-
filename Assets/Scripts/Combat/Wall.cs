using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Wall : MonoBehaviour
{
    private GameObject[] _walls = new GameObject[4];
    public  Vector3 MovingVelocity = Vector3.zero;
    public Vector2 SizeVelocity = Vector2.zero;

    public enum WallState { Outer, Full, Half }

    private List<Vector3> _fullBorderPositions;
    private List<Vector3> _outerBorderPositions;
    private List<Vector3> _halfBorderPositions;

    private List<Vector2> _fullBorderSizes;
    private List<Vector2> _outerBorderSizes;
    private List<Vector2> _halfBorderSizes;
    void Awake()
    {
        _walls[0] = gameObject.transform.Find("Top").gameObject;
        _walls[1] = gameObject.transform.Find("Bottom").gameObject;
        _walls[2] = gameObject.transform.Find("Left").gameObject;
        _walls[3] = gameObject.transform.Find("Right").gameObject; 

        _outerBorderPositions = new List<Vector3>
        {
            new Vector3(0, 6, 0),   // Top
            new Vector3(0, -6, 0),  // Bottom
            new Vector3(-12, 0, 0),  // Left
            new Vector3(12, 0, 0)    // Right
        };

        _outerBorderSizes = new List<Vector2>
        {
            new Vector2(24, 1),   // Top
            new Vector2(24, 1),  // Bottom
            new Vector2(1, 12),  // Left
            new Vector2(1, 12)    // Right
        };

        _fullBorderPositions = new List<Vector3>
        {
            new Vector3(0, 5, 0),   // Top
            new Vector3(0, -5, 0),  // Bottom
            new Vector3(-11, 0, 0),  // Left
            new Vector3(11, 0, 0)    // Right
        };

        _fullBorderSizes = new List<Vector2>
        {
            new Vector2(24, 1),   // Top
            new Vector2(24, 1),  // Bottom
            new Vector2(1, 12),  // Left
            new Vector2(1, 12)    // Right
        };

        _halfBorderPositions = new List<Vector3>
        {
            new Vector3(0, 2.5f, 0),   // Top
            new Vector3(0, -2.5f, 0),  // Bottom
            new Vector3(-6f, 0, 0),  // Left
            new Vector3(6f, 0, 0)    // Right
        };   

        _halfBorderSizes = new List<Vector2>
        {
            new Vector2(12, 0.1f),   // Top
            new Vector2(12, 0.1f),  // Bottom
            new Vector2(0.1f, 5),  // Left
            new Vector2(0.1f, 5)    // Right
        }; 
    }

    public void ActivateWall(int index)
    {
        _walls[index].SetActive(true);
    }

    public void DeactivateWall(int index)
    {
        _walls[index].SetActive(false);
    }

    public IEnumerator Transition(float duration, WallState targetState)
    {
        List<Vector3> targetPositions = new List<Vector3>();
        List<Vector2> targetSizes = new List<Vector2>();
        if(targetState == WallState.Outer)
        {
            targetPositions = _outerBorderPositions;
            targetSizes = _outerBorderSizes;
        }
        else if(targetState == WallState.Full)
        {
            targetPositions = _fullBorderPositions;
            targetSizes = _fullBorderSizes;
        }
        else if(targetState == WallState.Half)
        {
            targetPositions = _halfBorderPositions;
            targetSizes = _halfBorderSizes;
        }
        float elapsed = 0f;
        Vector3[] mVelocities = new Vector3[4];
        Vector2[] sVelocities = new Vector2[4];
        for(int i = 0; i < _walls.Length; i++)
        {
            mVelocities[i] = Vector3.zero;
            sVelocities[i] = Vector2.zero;
        }
        Vector3[] startPos = new Vector3[4];
        Vector2[] startSizes = new Vector2[4];
        SpriteRenderer[] srs = new SpriteRenderer[4];
        BoxCollider2D[] cols = new BoxCollider2D[4];

        for(int i = 0; i < _walls.Length; i++)
        {
            startPos[i] = _walls[i].transform.position;
            startSizes[i] = _walls[i].GetComponent<SpriteRenderer>().size;
            srs[i] = _walls[i].GetComponent<SpriteRenderer>();
            cols[i] = _walls[i].GetComponent<BoxCollider2D>();
        }

        while (elapsed < duration)
        {
            for(int i = 0; i < _walls.Length; i++)
            {
                float t = elapsed / duration;
                t = t * t * (3f - 2f * t);
                _walls[i].transform.position = Vector3.Lerp(startPos[i], targetPositions[i], t);
                Vector2 _s = Vector2.Lerp(startSizes[i], targetSizes[i], t);
                srs[i].size = _s;
                cols[i].size = _s;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
