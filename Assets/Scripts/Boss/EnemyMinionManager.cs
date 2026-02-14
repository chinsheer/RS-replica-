using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMinionManager : MonoBehaviour
{
    private List<MinionController> _minions = new List<MinionController>();
    public List<MinionController> Minions => _minions;
    private int _maxMinions;
    public int MaxMinions => _maxMinions;

    public void Initialize(int maxMinions)
    {
        _maxMinions = maxMinions;
    }
    
    public void RemoveMinion(MinionController minion)
    {
        if (_minions.Contains(minion))
        {
            GameObject.Destroy(minion.gameObject);
            _minions.Remove(minion);
        }
    }
    
    public void RegisterMinion(MinionController minion)
    {
        if (!_minions.Contains(minion))
        {
            _minions.Add(minion);
        }
    }
}
