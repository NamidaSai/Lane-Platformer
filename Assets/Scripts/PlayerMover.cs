﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float flySpeed = 5f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float floatHeight = 1f;
    [SerializeField] float liftForce = 10f;
    [SerializeField] float damping = 1f;

    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    private Animator animator;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Fall();
    }

    public void Move(Vector2 moveThrottle)
    {
        float moveSpeedX = 0f;
        bool playerIsTouchingGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (playerIsTouchingGround)
        {
            animator.SetFloat("MoveBlend", Mathf.Abs(myRigidbody.velocity.x));
            moveSpeedX = moveSpeed * Time.deltaTime * moveThrottle.x;
        }
        else
        {
            animator.SetFloat("MoveBlend", 0);
            moveSpeedX = flySpeed * Time.deltaTime * moveThrottle.x;
        }

        Vector2 moveForce = new Vector2(moveSpeedX, 0f);
        myRigidbody.velocity += moveForce;
        
        FlipSprite(moveThrottle);
    }

    private void FlipSprite(Vector2 moveThrottle)
    {
        bool playerHasHorizontalSpeed = (Mathf.Abs(moveThrottle.x) > Mathf.Epsilon);
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveThrottle.x), transform.localScale.y, transform.localScale.z);
        }
    }


    public void Jump()
    {
        bool playerIsTouchingGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (!playerIsTouchingGround) { return; }

        float currentJumpSpeed = jumpSpeed * 100f;
        Vector2 jumpForce = new Vector2(0f, currentJumpSpeed);
        myRigidbody.AddForce(jumpForce);

        animator.SetTrigger("Jump");
    }

    private void Fall()
    {
        bool playerHasDownwardSpeed = (myRigidbody.velocity.y < Mathf.Epsilon);
        bool playerIsTouchingGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (!playerHasDownwardSpeed) { return; }

        RaycastHit2D hit = Physics2D.Raycast(myRigidbody.position, -Vector2.up);

        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - myRigidbody.position.y);
            float heightError = floatHeight - distance;
            float endLineY = myRigidbody.position.y - heightError;
            Vector2 endLinePosition = new Vector2(myRigidbody.position.x, endLineY);
            Debug.DrawLine(myRigidbody.position, endLinePosition);

            float force = liftForce * heightError - myRigidbody.velocity.y * damping;

            myRigidbody.AddForce(Vector3.up * force);
        }

        animator.SetBool("isFalling", !playerIsTouchingGround);
    }
}
