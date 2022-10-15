using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    public Player player;

    private Vector2 movementDir = Vector2.zero;
    private bool jumped = false ;


    PlayerInput pi;

    private void Start()
    {
    //   pi = new PlayerInput();
    //   pi.bindingMask = InputBinding
    //   pi.Player.Enable();
    //   pi.Enable();
    //   var devices = pi.Player;
    //   var devices = pi.FindBinding
    //   Debug.Log(devices);
     // pi.Player.Jump.performed += Jump;


      // var devices = "";
      // foreach (var item in InputSystem.devices)
      // {
      //     devices += "\n"+item.shortDisplayName + item.ToString();
      // }
      //
      // Debug.Log($"Number of connected devices { InputSystem.devices.Count}\n" +
      //     $"{devices}");




    }


    void Update()
    {

        //var v = pi.Player.Movement.ReadValue<Vector2>();
        //Debug.Log(v);

        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log("Fixed");
        //horizontal = pi.Player.Movement.ReadValue<Vector2>().x;
        rb.velocity = new Vector2(movementDir.x * speed, rb.velocity.y);

        Debug.Log($"Grounded: {IsGrounded()}");

    }

    public void OnMoved(InputAction.CallbackContext context)
    {
        Debug.Log("OnMoved");
        movementDir = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("OnJump");

        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }


    public enum Player
    {
        Player1,
        Player2
    }


}
