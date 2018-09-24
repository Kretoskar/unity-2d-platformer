using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    //config
    [SerializeField] private float mRunSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;

    //state
    private bool mIsDead;

    //cached component references
    private Rigidbody2D mRigidbody;
    private Animator mAnimator;
    private CapsuleCollider2D mBodyCollider2D;
    private BoxCollider2D mFeetCollider2D;
    private PlayerStats mPlayerStats;
    private float mGravityScaleAtStart;

    private void Start() {
        mIsDead = false;

        mRigidbody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mBodyCollider2D = GetComponent<CapsuleCollider2D>();
        mFeetCollider2D = GetComponent<BoxCollider2D>();
        mPlayerStats = FindObjectOfType<PlayerStats>();
        mGravityScaleAtStart = mRigidbody.gravityScale;
    }

    private void Update() {
        if (!mIsDead) {
            Climb();
            Run();
            Jump();
            FlipSprite();
        }
        Die();
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal");
        string runningAnimationName = "Running";

        Vector2 playerVelocity = new Vector2(controlThrow * mRunSpeed, mRigidbody.velocity.y);
        mRigidbody.velocity = playerVelocity;

        mAnimator.SetBool(runningAnimationName, IsPlayerMovingHorizontal());
    }

    private void Jump() {
        if (!mFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            return;
        }
        if (Input.GetButtonDown("Jump")) {
            Vector2 jumpVelocity = new Vector2(0f, jumpSpeed);
            mRigidbody.velocity += jumpVelocity;
        }
    }

    private void Climb() {
        if (!mBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))) {
            mAnimator.SetBool("Climbing", false);
            mRigidbody.gravityScale = mGravityScaleAtStart;
            return;
        }
        float controlThrow = Input.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(mRigidbody.velocity.x, controlThrow * climbSpeed);
        mRigidbody.velocity = climbVelocity;
        mRigidbody.gravityScale = 0;

        mAnimator.SetBool("Climbing", true);
    }

    private void FlipSprite() {
        if(IsPlayerMovingHorizontal()) {
            transform.localScale = new Vector2(Mathf.Sign(mRigidbody.velocity.x), 1f);
        }
    }

    private void Die() {
        if(mPlayerStats.GetCurrHealth() == 0) {
            mIsDead = true;
            mRigidbody.velocity = new Vector2(0f, 0f);
            mAnimator.SetTrigger("Dying");
        }
    }

    private bool IsPlayerMovingHorizontal() {
        return Mathf.Abs(mRigidbody.velocity.x) > Mathf.Epsilon;
    }
}
