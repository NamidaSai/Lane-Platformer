using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;

    private void FixedUpdate()
    {
        GetComponent<PlayerMover>().Move(moveInput);

    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump()
    {
        GetComponent<PlayerMover>().Jump();
    }
}
