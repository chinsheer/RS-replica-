using Unity.Mathematics;
using UnityEngine;

public class HearthUI : MonoBehaviour
{
    public PlayerHealth Player;
    public GameObject Hearth;

    private int _maxHearth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _maxHearth = Player.MaxHP;
        for (int i = 0; i < Player.CurrentHP; i++)
        {
            Instantiate(Hearth, transform);
        }
        Player.OnHealthChanged += RefreshHearts;
    }

    void RefreshHearts(int currentHP)
    {
        for (int i = 0; i < Player.MaxHP; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(i < Player.CurrentHP);
        }
    }
}
