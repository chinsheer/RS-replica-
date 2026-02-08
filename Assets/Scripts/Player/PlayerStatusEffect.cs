using UnityEngine;

public class PlayerStatusEffect : MonoBehaviour
{
    private float _invincibleUntil;
    private bool _isInZoneInvincibility = false;

    public bool IsInvincible => (Time.time < _invincibleUntil) || _isInZoneInvincibility;

    public void GrantInvincible(float duration)
    {
        _invincibleUntil = Time.time + duration;
    }

    public void GrantZoneInvincibility()
    {
        _isInZoneInvincibility = true;
    }

    public void RevokeZoneInvincibility()
    {
        _isInZoneInvincibility = false;
    }

}