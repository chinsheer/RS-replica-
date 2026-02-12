using System.Collections.Generic;
using UnityEngine;

public class EnemyMinionManager : MonoBehaviour
{
    private List<MinionController> _minions = new List<MinionController>();
    public List<MinionController> Minions => _minions;
    
    public void RegisterMinion(MinionController minion)
    {
        if (!_minions.Contains(minion))
        {
            _minions.Add(minion);
        }
    }
}
