
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;


    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
       // transform.LookAt(target);
    }





}
