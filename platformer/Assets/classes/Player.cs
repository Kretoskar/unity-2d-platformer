using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    
    //config
    [SerializeField] private float mRunSpeed = 5f;
    [SerializeField] private float jumpSpeed = 5f;

    //state

    //cached component references
    private Rigidbody2D mRigidbody;
    private Animator mAnimator;
    private CapsuleCollider2D mBodyCollider2D;
    private BoxCollider2D mFeetCollider2D;

    private void Start() {
        mRigidbody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mBodyCollider2D = GetComponent<CapsuleCollider2D>();
        mFeetCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update() {
        Run();
        FlipSprite();
        Jump();
    }

    private void Run() {
        float controlThrow = Input.GetAxis("Horizontal");
        string runningAnimationName = "Running";

        Vector2 playerVelocity = new Vector2(controlThrow * mRunSpeed, mRigidbody.velocity.y);
        mRigidbody.velocity = playerVelocity;

        mAnimator.SetBool(runningAnimationName, IsPlayerMoving());
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

    private void FlipSprite() {
        if(IsPlayerMoving()) {
            transform.localScale = new Vector2(Mathf.Sign(mRigidbody.velocity.x), 1f);
        }
    }

    private bool IsPlayerMoving() {
        return Mathf.Abs(mRigidbody.velocity.x) > Mathf.Epsilon;
    }
}
