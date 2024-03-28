using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    readonly int moveX = Animator.StringToHash("moveX");
    readonly int moveY = Animator.StringToHash("moveY");
    readonly int isMoving = Animator.StringToHash("isMoving");
    readonly int gotKilled = Animator.StringToHash("gotKilled");
    readonly int revived = Animator.StringToHash("revived");
    readonly int isAttacking = Animator.StringToHash("isAttacking");

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void HandleDeadAnimation()
    {
        anim.SetTrigger(gotKilled);
    }

    public void HandleMoveBoolAnimation(bool value)
    {
        anim.SetBool(isMoving, value);
    }

    public void HandleMovingAnimation(Vector2 direction)
    {
        anim.SetFloat(moveX, direction.x);
        anim.SetFloat(moveY, direction.y);
    }

    public void setAttackingAnimation(bool value)
    {
        anim.SetBool(isAttacking, value);
    }

    public void HandleReviveAnimation()
    {
        HandleMovingAnimation(Vector2.down);
        anim.SetTrigger(revived);
    }

}
