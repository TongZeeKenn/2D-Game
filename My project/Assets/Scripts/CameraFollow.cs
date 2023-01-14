using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;

    private Vector3 velocity = Vector3.zero;


    void FixedUpdate()
    {
        Vector3 movePosition = new Vector3(target.position.x, target.position.y) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
