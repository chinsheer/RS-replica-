using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Extend Sword", menuName = "Scriptable Objects/Attacks/Extend Sword Template")]
public class ExtendSwordTemplate : AttackData
{
    [SerializeField] private GameObject _swordPrefab;
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private float width = 0.5f;

    [SerializeField] private int MaxSessions = 3;
    [SerializeField] private float SessionInterval = 5f;
    [SerializeField] private float SwordIndicatorTime = 0.5f;
    [SerializeField] private float SwordActiveTime = 0.5f;
    [SerializeField] private int MaxStack = 3;

    private event Action _cleanUpAction;

    public List<ExtendSword>[] SpawnedSwords;

    public override IEnumerator Indicator(IBossContext ctx)
    {
        SpawnedSwords = new List<ExtendSword>[MaxSessions];
        float swordInterval = SwordIndicatorTime + SwordActiveTime;
        for(int i = 0; i < MaxSessions; i++)
        {
            SpawnedSwords[i] = new List<ExtendSword>();
            for(float time = i * SessionInterval; time < ActiveTime; time += swordInterval)
            {
                ExtendSword swordData = CreateInstance<ExtendSword>();
                swordData._swordPrefab = _swordPrefab;
                swordData._indicatorPrefab = _indicatorPrefab;
                swordData.width = width;
                swordData.ChargeTime = SwordIndicatorTime;
                swordData.ActiveTime = SwordActiveTime;
                swordData.RecoverTime = 0f;
                _cleanUpAction += swordData.CleanUp;
                SpawnedSwords[i].Add(swordData);
            }
        }
        yield return null;
    }

    public override IEnumerator Execute(IBossContext ctx)
    {
        float swordInterval = SwordIndicatorTime + SwordActiveTime;
        for(int sessionIndex = 0; sessionIndex < MaxSessions; sessionIndex++)
        {
            ctx.Runner.StartCoroutine(SpawnSword(ctx, SpawnedSwords[sessionIndex]));
            yield return new WaitForSeconds(SessionInterval);
        }
        yield return new WaitForSeconds(ActiveTime - ((MaxSessions - 1) * SessionInterval));
    }

    public override IEnumerator Recover(IBossContext ctx)
    {
        _cleanUpAction?.Invoke();
        yield return new WaitForSeconds(RecoverTime);
    }

    public IEnumerator SpawnSword(IBossContext ctx, List<ExtendSword> swordSession)
    {
        Vector2 lastHitPoint = ctx.Boss.position;
        int i = 0;
        while(i < swordSession.Count)
        {
            if(i - MaxStack >= 0)
            {
                swordSession[i - MaxStack].CleanUp();
            }
            ExtendSword swordData = swordSession[i];
            swordData.StartPosition = lastHitPoint;
            yield return swordData.Indicator(ctx);
            yield return swordData.Execute(ctx);
            yield return swordData.Recover(ctx);
            lastHitPoint = swordData.LastHitPoint;
            
            i++;
        }
    }
}