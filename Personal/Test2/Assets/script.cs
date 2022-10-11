using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class script : MonoBehaviour
{
    private Rigidbody rigidbody3d;
    public float smooth = 0.105f;
    public float runSpeed = 5.0f;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody3d = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.anyKey)
        {
            var direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
           
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(-direction), smooth);
            transform.Translate(direction * runSpeed * Time.deltaTime, Space.World);

        }

    }
}
