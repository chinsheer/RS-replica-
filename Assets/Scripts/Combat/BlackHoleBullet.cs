using UnityEngine;

public class BlackHoleBullet : MonoBehaviour
{
    public GameObject _blackHolePrefab;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(_blackHolePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
