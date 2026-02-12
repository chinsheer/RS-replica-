using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternRunner : MonoBehaviour
{
    [SerializeField] private PatternData _startPattern;
    [SerializeField] private PatternData[] _allPatterns;

    private Dictionary<PatternData, PatternInstance> _map;
    private PatternInstance _current;

    void Start()
    {
        StartCoroutine(Loop());
    }

    public void Initialize(PatternData start, PatternData[] all)
    {
        _startPattern = start;
        _allPatterns = all;
        _map = BuildGraph(_allPatterns);
        _current = _map[_startPattern];
    }

    private IEnumerator Loop()
    {
        while (_current != null)
        {
            int running = 0;
            foreach (var routine in _current.GetRoutines(gameObject.GetComponent<IBossContext>(), () => running--))
            {
                if (routine == null) continue;
                running++;
                StartCoroutine(routine);
            }
            while (running > 0)
            {
                yield return null;
            }
            _current = _current.ChooseNext(gameObject.GetComponent<IBossContext>());
        }
    }

    private Dictionary<PatternData, PatternInstance> BuildGraph(PatternData[] all)
    {
        var map = new Dictionary<PatternData, PatternInstance>();

        for (int i = 0; i < all.Length; i++)
        {
            var data = all[i];
            if (data != null && !map.ContainsKey(data))
                map[data] = new PatternInstance(data);
        }

        foreach (var kv in map)
        {
            PatternData data = kv.Key;
            PatternInstance inst = kv.Value;

            foreach (var edge in data.Edges)
            {
                if (edge.NextPattern == null) continue;
                if (!map.TryGetValue(edge.NextPattern, out var nextInst)) continue;

                inst.AddEdge(nextInst, edge);
            }
        }

        return map;
    }
}