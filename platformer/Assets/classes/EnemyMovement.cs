using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    //config
    [SerializeField] private float moveSpeed = 1f;

    //state

    //cached component references
    private Rigidbody2D mRigidbody2D;

	void Start () {
        mRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        if (IsFacingRight()) {
            mRigidbody2D.velocity = new Vector2(moveSpeed, 0f);
        } else {
            mRigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
        }
	}

    private bool IsFacingRight() {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        transform.localScale = new Vector2(-(Mathf.Sign(mRigidbody2D.velocity.x)), 1f);
    }
}
