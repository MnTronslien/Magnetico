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
    public float speed = 8f;
    public float jumpingPower = 16f;
    public float downforce = 1.2f;
    private bool isFacingRight = true;

    private Vector2 movementDir = Vector2.zero;
    private bool jumped = false ;



    public bool isPlayerOne = false;
    public bool isPlayerTwo = false;

    public Color playerColor;

    private void Start()
    {



        if (!MyScoreManager.playerOneAlive)
        {
            MyScoreManager.playerOneAlive = true;
            Debug.Log($"{gameObject.name} assigned as player one");
            isPlayerOne = true;
        }
        else if (!MyScoreManager.playerTwoAlive)
        {
            MyScoreManager.playerTwoAlive = true;
            Debug.Log($"{gameObject.name} assigned as player two");
            isPlayerTwo = true;
        }


        playerColor  = Random.ColorHSV();

        GetComponent<Renderer>().material.color = playerColor;
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

    public void Death()
    {
        Debug.Log("A player died");
        if (isPlayerOne)
        {
            MyScoreManager.instance.AddScoreForPlayerOneDeath();
            MyScoreManager.playerOneAlive = false;
        }
        else if (isPlayerTwo)
        {
            MyScoreManager.instance.AddScoreForPlayerTwoDeath();
            MyScoreManager.playerTwoAlive = false;
        }
        Destroy(gameObject);
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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Projectile>())
        {

            if (collision.gameObject.GetComponent<Projectile>().captured)
            {
                if (collision != null) Death();
            }
        }
    }

    private void FixedUpdate()
    {
        //Debug.Log("Fixed");
        //horizontal = pi.Player.Movement.ReadValue<Vector2>().x;
        rb.velocity = new Vector2(movementDir.x * speed, rb.velocity.y);


        if(rb.velocity.y < -0.1){
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y + downforce);
        }

        //Debug.Log($"Grounded: {IsGrounded()}");

    }

    public void OnMoved(InputAction.CallbackContext context)
    {
       // Debug.Log("OnMoved");
        movementDir = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
 //       Debug.Log("OnJump");

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
