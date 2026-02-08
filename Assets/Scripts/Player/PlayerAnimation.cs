using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator _animator;

    public void PlayMeleeAttack()
    {
        _animator.SetTrigger("MeleeAttack");
    }

    public void PlayRangedAttack()
    {
        _animator.SetTrigger("RangedAttack");
    }
}
