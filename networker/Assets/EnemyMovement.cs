using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    Rigidbody2D myRigidBody;
    BoxCollider2D myEdgeDetector;
    [SerializeField] float moveSpeed = 1f;
	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
        myEdgeDetector = GetComponentInChildren<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        if (IsNearThePlatformsEnd())
        {
            FlipEnemyMovement();
        }

    }

    bool IsNearThePlatformsEnd()
    {
        if (myEdgeDetector.IsTouchingLayers(LayerMask.GetMask("Ground")))
            { return false; }
        else
            { return true; }
    }

    void FlipEnemyMovement()
    {
        moveSpeed = moveSpeed * (-1);
        transform.localScale = new Vector2(transform.localScale.x * (-1), transform.localScale.y);
    }
}
