using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    //config
    [SerializeField] private float mRunSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;

    //state

    //cached component references
    private Rigidbody2D mRigidbody;
    private Animator mAnimator;
    private CapsuleCollider2D mBodyCollider2D;
    private BoxCollider2D mFeetCollider2D;
    private float gravityScaleAtStart;

    private void Start() {
        mRigidbody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mBodyCollider2D = GetComponent<CapsuleCollider2D>();
        mFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = mRigidbody.gravityScale;
    }

    private void Update() {
        Climb();
        Run();
        Jump();
        FlipSprite();
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
            mRigidbody.gravityScale = gravityScaleAtStart;
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

    private bool IsPlayerMovingHorizontal() {
        return Mathf.Abs(mRigidbody.velocity.x) > Mathf.Epsilon;
    }
}
