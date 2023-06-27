using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovmentScript : MonoBehaviour
{
    [SerializeField] float movementSpeed = 8f;
    [SerializeField] float jumpSpeed = 8f;
    [SerializeField] float climbSpeed = 8f;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myCollider;

    
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value) 
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return;}

        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }

    }

    void ClimbLadder() 
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))) 
        {
            Vector2 playerClimbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
            myRigidbody.velocity = playerClimbVelocity;

        }

    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * movementSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool plazerHasHoriyontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", plazerHasHoriyontalSpeed);
        
    }

    void FlipSprite()
    {
        bool plazerHasHoriyontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (plazerHasHoriyontalSpeed)
        {
           transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f); 
        }
        

    }
}
