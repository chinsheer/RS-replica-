using System.Collections;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour, IStatusReceiver
{
    private float _invincibleUntil;
    private bool _isInZoneInvincibility = false;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private Hurtbox _hurtbox;

    public void Initialize(SpriteRenderer body)
    {
        _body = body;
    }

    public bool IsInvincible => (Time.time < _invincibleUntil) || _isInZoneInvincibility;

    public void GrantInvincible(float duration)
    {
        _invincibleUntil = Time.time + duration;
        StartCoroutine(FlashInvincibility(duration, 0.1f));
    }

    public void GrantZoneInvincibility()
    {
        _isInZoneInvincibility = true;
    }

    public void RevokeZoneInvincibility()
    {
        _isInZoneInvincibility = false;
    }

    public StatusEffectManager GetStatusEffect()
    {
        return this;
    }

    // Status effects visuals
    public IEnumerator FlashInvincibility(float duration, float flashInterval)
    {
        float elapsedTime = 0f;
        Color originalColor = _body.color;
        Color flashColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
        _hurtbox.ChangeLayer("Invincible");
        while (elapsedTime < duration)
        {
            _body.color = (_body.color == originalColor) ? flashColor : originalColor;
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }
        _hurtbox.ChangeLayer("Player");
        _body.color = originalColor;
    }
}