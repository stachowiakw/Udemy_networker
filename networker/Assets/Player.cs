using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    GameSessionManager myGameSessionManager;

    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeetCollider;
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbingSpeed = 3f;
    [SerializeField] float defaultGravityScale = 3f;
    private Animator myAnimator;
    private float h;
    private float v;

    bool isAlive = true;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        myRigidbody.gravityScale = defaultGravityScale;
        myGameSessionManager = FindObjectOfType<GameSessionManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isAlive) { return; }

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Jump();
        ClimbLadder();
        Die();
        if (h != 0) { Run(); }
        else { myAnimator.SetBool("Running", false); }
    }

    private void Run()
    {
        if (h < Mathf.Epsilon)       {transform.localScale = new Vector3(-1, 1, 1);}
        else if (h > Mathf.Epsilon)  {transform.localScale = new Vector3(1, 1, 1);}
        myAnimator.SetBool("Running", true);
        myRigidbody.velocity = new Vector2(h*runSpeed, myRigidbody.velocity.y);
    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump") && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
            myRigidbody.velocity = jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        if (myRigidbody.IsTouchingLayers(LayerMask.GetMask("Ladders")))

        {
            if (Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon)
            {
                myAnimator.SetBool("Climbing", true);
            }
            else
            {
                myAnimator.SetBool("Climbing", false);
            }
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, v * climbingSpeed);
            myRigidbody.gravityScale = 0;
        }
        else
        {
            myAnimator.SetBool("Climbing", false);
            if (myRigidbody.gravityScale == 0)
            { myRigidbody.gravityScale = defaultGravityScale; }
        }
    }

    private void Die()
    {
        if (myRigidbody.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            if (!isAlive) { return; }
            isAlive = false;
            myAnimator.SetBool("Die", true);
            myRigidbody.velocity = new Vector2(25, 25);
            myGameSessionManager.LevelProcedureOnDeath();
        }
        
    }
}

