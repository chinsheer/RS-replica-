using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternRunner : MonoBehaviour
{
    private PatternData[] _allPatterns;
    private PatternInstance _current;
    private Action stopRoutine;

    public void Initialize(PatternData start, PatternData[] all)
    {
        _allPatterns = all;
        Dictionary<PatternData, PatternInstance> map = BuildGraph();
        _current = map[start];
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        PatternInstance current = _current;
        Action nullCurrent = () => current = null;
        stopRoutine = nullCurrent;
        while (current != null)
        {
            int running = 0;
            foreach (var routine in current.GetRoutines(gameObject.GetComponent<IBossContext>(), () => running--))
            {
                if (routine == null) continue;
                running++;
                StartCoroutine(routine);
            }
            while (running > 0)
            {
                yield return null;
            }
            if(current == null)
            {
                yield break;
            } 
            current = current.ChooseNext(gameObject.GetComponent<IBossContext>());
        }
    }

    public void Stop()
    {
        _current = null;
        stopRoutine?.Invoke();
        stopRoutine = null;
    }


    public Dictionary<PatternData, PatternInstance> BuildGraph()
    {
        var map = new Dictionary<PatternData, PatternInstance>();

        for (int i = 0; i < _allPatterns.Length; i++)
        {
            var data = _allPatterns[i];
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