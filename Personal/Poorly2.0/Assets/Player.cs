using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public bool drawDebugRaycasts = true;

    public Poorly20 controls;
    private Rigidbody2D rb2D;
    //private Bullet bullet;

    [Header("Movement Properties")]
    private Vector2 direction;  //vector2 input from InputMaster for left and right buttons pressed for movement of player.
    public float playerSpeed = 5f;
    private float isCrouching = 0f;
    public float crouchSize = 0.6f;


    [Header("Jump Properties")]
    private float isJumping = 0; //float input from InputMaster for jump button press
    public float jumpForce = 10.0f; //force of jump
    private float maxJumpForce = 15f;


    [Header("Environment Check Properties")]
    public float playerOffSet = 0.4f;
    public float groundDistance = 0.65f;
    public float headDistance = 0.65f;
    public LayerMask groundLayer;



    [Header("Status Flags")]
    public bool isOnGround;
    public bool hasJumped; //delete once we get a ground check implimented.
    public bool doubleJumpAllowed;
    private bool isMoving;

    private float playerHeight;
    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

       
        Debug.Log("PrintOnAwake: script was enabled");
        controls = new Poorly20();
        controls.Player.Fire.started += context => onFire();

       controls.Player.Move.performed += context => direction = context.ReadValue<Vector2>(); //sets direction Vector2 to + or - values depending on direction once keys are pressed
       controls.Player.Move.canceled += context => direction = context.ReadValue<Vector2>();  //sets direction Vector2 to 0 once keys are released



    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Example1");
    }
    public void onFire()
    {
        Debug.Log("Fire Button Pressed");
    }
    public void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            print("b key was pressed");
        }
    }
    private void FixedUpdate()
    {

        RaycastHit2D leftSideGrounded = Raycast(new Vector2(-playerOffSet, 0f), Vector2.down, groundDistance);
        RaycastHit2D rightSideGrounded = Raycast(new Vector2(playerOffSet, 0f), Vector2.down, groundDistance);

        if (leftSideGrounded || rightSideGrounded) //Set Statis Flag for isOnGround
        {
            //hasJumped = false;
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
            // hasJumped = true;
        }

      
        rigidbodyMovement();
    }

    private void rigidbodyMovement()
    {

        //rb2D.transform.parent.localPosition = new Vector2(direction.x * playerSpeed, rb2D.velocity.y);
      
            rb2D.velocity = new Vector2(direction.x * playerSpeed, rb2D.velocity.y); //Movement velocity

       

    }

    //Below leveraged from playerMovement.cs from the RobbiePlatformer from Live training Unite Berlin 2019  https://www.youtube.com/watch?v=j29NgzV8Dw4

    //These two Raycast methods wrap the Physics2D.Raycast() and provide some extra
    //functionality
    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        //Call the overloaded Raycast() method using the ground layermask and return 
        //the results
        return Raycast(offset, rayDirection, length, groundLayer);
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        //Record the player's position
        Vector2 pos = transform.position;

        //Send out the desired raycasr and record the result
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        //If we want to show debug raycasts in the scene...
        if (drawDebugRaycasts)
        {
            //...determine the color based on if the raycast hit...
            Color color = hit ? Color.red : Color.green;
            //...and draw the ray in the scene view
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }

        //Return the results of the raycast
        return hit;
    }


    private void OnEnable()
    {
        controls.Player.Enable();

    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }

}

