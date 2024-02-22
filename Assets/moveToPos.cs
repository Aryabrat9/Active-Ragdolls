using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class moveToPos : MonoBehaviour
{
    public Transform body;
    public Rigidbody moveBody;
    public Transform target;
    public float speed = 5f;
    public float damp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Vector3 lookPos = body.position - target.position;
        // lookPos.y = 0;

        // var rotation = Quaternion.LookRotation(lookPos);
        // body.rotation = Quaternion.Slerp(body.rotation, rotation, Time.deltaTime * damp);

        Vector3 move = target.position - transform.position;

        body.LookAt(target);
        moveBody.AddForce(move.normalized * Time.deltaTime * speed, ForceMode.VelocityChange);
    }
}
