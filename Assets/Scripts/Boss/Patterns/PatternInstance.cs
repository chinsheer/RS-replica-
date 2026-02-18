using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System;

public sealed class PatternInstance
{
    private readonly PatternData _data;
    private List<PatternEdgeInstance> _edges = new List<PatternEdgeInstance>();
    private float _patternTime;
    public PatternData Data => _data;
    public float PatternTime => _patternTime;

    public PatternInstance(PatternData data)
    {
        _data = data;
        _patternTime = 0f;
    }

    public void AddEdge(PatternInstance nextPattern, PatternEdgeData edgeData)
    {
        _edges.Add(new PatternEdgeInstance(nextPattern, edgeData));
    }

    public PatternInstance ChooseNext(IBossContext ctx)
    {
        int n = _edges.Count;
        if (n == 0) return null;

        // Calculate total weights
        int[] w = new int[n];
        int total = 0;

        for (int i = 0; i < n; i++)
        {
            w[i] = _edges[i].GetWeight(ctx);
            total += w[i];
        }

        if (total <= 0) return null;

        // Randomly select based on weights
        int roll = (int)UnityEngine.Random.Range(0f, (float)total);
        int acc = 0;

        for (int i = 0; i < n; i++)
        {
            acc += w[i];
            if (roll < acc)
                return _edges[i].Next;
        }

        return _edges[n - 1].Next;
    }

    public IEnumerator<PatternEdgeInstance> GetEdges()
    {
        foreach (var edge in _edges)
        {
            yield return edge;
        }
    }

    public List<IEnumerator> GetRoutines(IBossContext ctx, Action onComplete)
    {
        List<IEnumerator> routines = new List<IEnumerator>();
        foreach (var attack in _data.Attacks)
        {
            if (attack != null)
            {
                routines.Add(AttackRunner(ctx, attack, onComplete));
            }
        }
        return routines;
    }

    public IEnumerator AttackRunner(IBossContext ctx, AttackData attack, Action onComplete = null)
    {
        yield return attack.Indicator(ctx);
        yield return attack.Execute(ctx);
        yield return attack.Recover(ctx);
        onComplete?.Invoke();
    }
    
}   