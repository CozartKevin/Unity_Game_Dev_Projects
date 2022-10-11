using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Vector2 _currentMove;
    public float gravity = 20.0f;
    public float jumpHeight = 20.0f;
    public float jumpForce = 15.0f;
    private float _currentJump = 0;
    public bool isGrounded;
    public float RotateSpeed = 5f;
    private GameObject ball;


    private Material mat;
    private Rigidbody rb;

    private Vector3 m_EulerAngleVelocity;


    //TEST
    



    // Start is called before the first frame update
    void Start()
    {
      
        
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      

    }


    void FixedUpdate()
    {
        //Movement
        m_EulerAngleVelocity = new Vector3(_currentMove.x, _currentMove.y, 0);
        Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);

        Vector3 moveVelocity = _moveSpeed * (_currentMove.x * Vector3.right + _currentMove.y * Vector3.forward);
        //  Vector3 moveThisFrame = Time.deltaTime * moveVelocity;
          rb.AddForce(moveVelocity * Time.deltaTime, ForceMode.Impulse);

        //Jump
        if (isGrounded && _currentJump > 0)
        {
            rb.AddForce(new Vector3(0,jumpHeight * jumpForce,0), ForceMode.Impulse);
            isGrounded = false;
        }


        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(rb.velocity.x, 0, rb.velocity.z)), Time.deltaTime * RotateSpeed);

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _currentMove = context.ReadValue<Vector2>();


    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _currentJump = context.ReadValue<float>();
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

}
