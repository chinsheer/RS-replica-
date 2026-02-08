using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [SerializeField] private List<SkillData> _skillDataList;
    private PlayerSkill[] _skills;

    public PlayerSkill Skill(int index) => _skills[index];

    private Coroutine _castRoutine;
    private int _castingIndex = -1;
    private PlayerAnimation _playerAnimation;

    public bool IsCasting => _castRoutine != null;

    public void TryCast(int index)
    {
        if (IsCasting) return;
        _castingIndex = index;
        _castRoutine = StartCoroutine(CastRoutine(index, _skills[index]));
    }

    void Awake()
    {
        _skills = new PlayerSkill[_skillDataList.Count];
        for (int i = 0; i < _skillDataList.Count; i++)
        {
            _skills[i] = new PlayerSkill(_skillDataList[i]);
        }
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    IEnumerator CastRoutine(int index, PlayerSkill skill)
    {
        _castingIndex = index;
        if (!skill.IsAvailable())
        {
            _castingIndex = -1;
            _castRoutine = null;
            yield break;
        }

        float charge = skill.SkillData.ChargeTime;
        float execute = skill.SkillData.ExecuteTime;
        float recover = skill.SkillData.RecoveryTime;

        // CHARGE
        EffectHandle chargeHandle = skill.SkillData.Charge(gameObject);
        // No Idea where should I determine the animation
        if(index == 0)
        {
            _playerAnimation.PlayMeleeAttack();
        }
        else if(index == 1)
        {
            _playerAnimation.PlayRangedAttack();
        }

        yield return new WaitForSeconds(charge);
        chargeHandle?.Dispose();

        // EXECUTE
        EffectHandle executeHandle = skill.SkillData.Execute(gameObject);
        yield return new WaitForSeconds(execute);
        executeHandle?.Dispose();
        skill.StartCooldown(gameObject);

        // RECOVER
        EffectHandle recoverHandle = skill.SkillData.Recover(gameObject);
        yield return new WaitForSeconds(recover);
        recoverHandle?.Dispose();

        _castingIndex = -1;
        _castRoutine = null;
    }
}
