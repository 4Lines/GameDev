using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform FollowTarget;
    public Vector3 TargetOffset;
    public float MoveSpeed = 2f;
    Camera cam;
    private Transform _myTransform;

    // Use this for initialization
    void Start()
    {
        _myTransform = transform;
    }

    public void SetTarget(Transform aTransform)
    {
        FollowTarget = aTransform;
    }

    private void LateUpdate()
    {
        if (FollowTarget != null)
            _myTransform.position = Vector3.Lerp(_myTransform.position, FollowTarget.position + TargetOffset, MoveSpeed * Time.deltaTime);
    }

}