using UnityEngine;

public class PlayerSkill
{
    private SkillData _skillData;
    private float _nextAvailableTime = 0f;
    
    public SkillData SkillData => _skillData;

    public PlayerSkill(SkillData skillData)
    {
        _skillData = skillData;
    }

    public bool IsAvailable()
    {
        return Time.time >= _nextAvailableTime;
    }

    public float GetCooldownRemaining()
    {
        return Mathf.Max(0f, _nextAvailableTime - Time.time);
    }

    public void StartCooldown(GameObject user)
    {
        _nextAvailableTime = Time.time + _skillData.CooldownTime;
    }
}
