using UnityEngine;
using System.Collections;

public enum AnimationState
{
    Idle,
    Walking,
    Attacking
};

public class AnimationStateController : MonoBehaviour {
    public AnimationState currentAnimationState;
    public Animator mAnimator;

    private Vector3 mLastPos = Vector3.zero;
    private float timeSinceMoved = 0;
	
	void Start () {
        currentAnimationState = AnimationState.Idle;
        mAnimator = GetComponent<Animator>();
        AnimationState temp = AnimationState.Idle;
        UpdateAnimationState(temp);
	}

    private void Update()
    {
        if (mLastPos == transform.position)
        {
            timeSinceMoved += Time.deltaTime;
            if (timeSinceMoved > .25)
            {
                UpdateAnimationState(AnimationState.Idle);
                timeSinceMoved = 0;
            }
        }
        mLastPos = transform.position;
    }

    public void UpdateAnimationState(AnimationState newAnimationState)
    {
        currentAnimationState = newAnimationState;
        if (mAnimator != null)
        {
            switch (currentAnimationState)
            {
                case AnimationState.Idle:
                    mAnimator.SetBool("IsWalking", false);
                    mAnimator.SetBool("IsIdle", true);
                    mAnimator.SetBool("IsAttacking", false);
                    break;
                case AnimationState.Walking:
                    mAnimator.SetBool("IsWalking", true);
                    mAnimator.SetBool("IsIdle", false);
                    mAnimator.SetBool("IsAttacking", false);
                    break;
                case AnimationState.Attacking:
                    mAnimator.SetBool("IsWalking", false);
                    mAnimator.SetBool("IsIdle", false);
                    mAnimator.SetBool("IsAttacking", true);
                    break;
                default:
                    break;
            }
        }
    }
}
