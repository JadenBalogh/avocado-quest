using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float moveTime;
    [SerializeField] private Transform target;

    private Vector2 currVel;

    private void Update()
    {
        Vector3 targetPos = Vector2.SmoothDamp(transform.position, target.position, ref currVel, moveTime);
        transform.position = targetPos + Vector3.forward * transform.position.z;
    }
}
