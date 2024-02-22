using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{

    public Transform body;
    public bool LockX;
    public bool LockY;
    public bool LockZ;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = body.position;
        Quaternion rot = transform.rotation;
        if (LockX)
            rot.x = 0;
        if (LockY)
            rot.y = 0;
        if (LockZ)
            rot.z = 0;
        transform.rotation = rot;
    }
}
