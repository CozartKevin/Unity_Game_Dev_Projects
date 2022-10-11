using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpUpForce;
    public LayerMask groundLayer;
    private float startTime = 0f;
    public float holdTime = 1.5f;
    public float dashSpeed;
    public float dashLength = 15;
    private bool isHangTime = false;
    private float timer = 0;
    private int AkeyCount = 0;
    private int DkeyCount = 0;
    private bool hasJumped = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();


    }


    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.velocity.y);
        Debug.Log(movement);
       
            rb2d.velocity = movement;

        
        
              

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startTime = Time.time;
            timer = startTime;
          
          
                Jump();
            
        }


        if (Input.GetKeyDown(KeyCode.A))
        {

            if (AkeyCount >= 2)
            {
                Debug.Log("Inside of Key press A");

                rb2d.MovePosition(new Vector2(rb2d.position.x - dashLength, rb2d.position.y));
                //rb2d.velocity = new Vector2(-dashSpeed, 0);
                isHangTime = false;
                timer = 0;
                DkeyCount = 0;
                AkeyCount = 0;
            }
            else
            {
                ++AkeyCount;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (DkeyCount >= 2)
            {
                Debug.Log("Inside of Key press D");     
                Debug.Log(dashSpeed);
          
                rb2d.MovePosition(new Vector2(rb2d.position.x + dashLength, rb2d.position.y));
                // rb2d.velocity = new Vector2(dashSpeed, oldYVelocity);
                isHangTime = false;
                timer = 0;
                DkeyCount = 0;
                AkeyCount = 0;
            }
            else
            {
                ++DkeyCount;
            }
        }

       
      //  Debug.Log("Outside 4");

    }
    void Jump()
    {
        if (!IsGrounded())
        {
            return;
        }
        else
        {
            rb2d.velocity = new Vector2(0, jumpUpForce);
        }
    }

    bool IsGrounded()
    {
        Vector2 position = rb2d.transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            hasJumped = false;
            return true;
        }

        return false;
    }


}