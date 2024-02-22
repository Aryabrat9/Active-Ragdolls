using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Height_Balance_Force : MonoBehaviour
{
    public Transform RaycastSource;
    public Rigidbody upRightBody;
    public Rigidbody hips;
    public Rigidbody damp;

    public float constUpForce = 170f;
    public float upRightTorque = 100f;

    float yForce = 0;
    float distance;
    float velocity;

    public float dampingForce;


    void Start()
    {
        //upRightBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(RaycastSource.position, Vector3.down, out hit))
        {
            Debug.DrawRay(RaycastSource.position, Vector3.down, Color.red);
            distance = hit.distance;
            if (distance < 1.3)
            {
                if (distance < 0.8)
                {
                    if (distance < 0.6)
                        yForce += 5f;
                    else
                        yForce += 1f;
                }
                else if (distance > 0.9)
                {
                    if (distance >= 1)
                        yForce -= 5f;
                    else
                        yForce -= 1f;
                }
            }
            else
                yForce = 0;
        }
        upRightBody.AddForce(0, yForce, 0);
        upRightBody.AddForce(Vector3.up * constUpForce);

        var rot = Quaternion.FromToRotation(transform.up, Vector3.up);
        hips.AddTorque(new Vector3(rot.x, rot.y, rot.z)* upRightTorque);
        velocity = hips.velocity.magnitude;
        //Debug.Log(velocity);
        
        if (velocity > 0.5)
        {
            Vector3 opposeForceDir = -hips.velocity;
            damp.AddForce(opposeForceDir.normalized * dampingForce, ForceMode.Acceleration);
        }
    }


}
