using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    private Rigidbody2D myRigidbody;
    [SerializeField] float runSpeed = 3f;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float climbingSpeed = 3f;
    [SerializeField] float defaultGravityScale = 3f;
    private Animator myAnimator;
    private float h;
    private float v;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        Jump();
        ClimbLadder();
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
        if (CrossPlatformInputManager.GetButtonDown("Jump") && myRigidbody.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpForce);
            myRigidbody.velocity = jumpVelocityToAdd;
        }
    }

    private void ClimbLadder()
    {
        if (myRigidbody.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, v * climbingSpeed);
            myRigidbody.gravityScale = 0;
        }
        else
        {
            if (myRigidbody.gravityScale == 0)
            { myRigidbody.gravityScale = defaultGravityScale; }
        }
    }
}

