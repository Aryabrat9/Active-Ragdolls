using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    Vector3 currentPositionL;
    Vector3 currentPositionR;
    Vector3 newPositionL;
    Vector3 newPositionR;

    public Transform RaycastBody;
    public Transform LeftTarget;
    public Transform RightTarget;

    public float footSpacing = 0.2f;
    public float stepDistance = 0.5f;
    public float stepHeight = 0.3f;
    public float stepOffset = 0.3f;
    public float speed = 5f;

    bool LStepping;
    bool RStepping;

    float DistL;
    float DistR;


    void Start()
    {
        currentPositionL = LeftTarget.position;
        currentPositionR = RightTarget.position;
    }

    void LateUpdate()
    {   
        RaycastHit hitL;
        if (Physics.Raycast(RaycastBody.position + (-RaycastBody.right * footSpacing), Vector3.down, out hitL))
        {
            Vector3 normalL = (hitL.point - currentPositionL).normalized;
            normalL.y = 0;
            newPositionL = hitL.point + normalL * stepOffset;

            Debug.DrawRay(RaycastBody.position + (-RaycastBody.right * footSpacing), Vector3.down, Color.red);
        }
        RaycastHit hitR;
        if (Physics.Raycast(RaycastBody.position + (RaycastBody.right * footSpacing), Vector3.down, out hitR))
        {
            Vector3 normalR = (hitR.point - currentPositionR).normalized;
            normalR.y = 0;
            newPositionR = hitR.point + normalR * stepOffset;

            Debug.DrawRay(RaycastBody.position + (RaycastBody.right * footSpacing), Vector3.down, Color.red);
        }

        DistL = Vector3.Distance(currentPositionL, newPositionL);
        DistR = Vector3.Distance(currentPositionR, newPositionR);
        
        //LeftTarget.position = Vector3.Lerp(LeftTarget.position, currentPositionL, Time.deltaTime * speed);
        //RightTarget.position = Vector3.Lerp(RightTarget.position, currentPositionR, Time.deltaTime * speed);

        LeftTarget.position = currentPositionL;
        RightTarget.position = currentPositionR;


        if (!LStepping && !RStepping)
        {
            if (DistL > stepDistance && DistR > stepDistance)
            {   
                if (DistL > DistR)
                {
                    StartCoroutine(StepL());
                }
                else if (DistL < DistR)
                {
                    StartCoroutine(StepR());
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(currentPositionL, 0.1f);
        Gizmos.DrawWireSphere(currentPositionR, 0.1f);
    }

    IEnumerator StepL()
    {
        LStepping = true;
        Vector3 midpos = (currentPositionL + newPositionL)/2;
        //currentPositionL = new Vector3(midpos.x, midpos.y + stepHeight, midpos.z);
        //yield return new WaitForSeconds(0.3f);
        float lerp = 0;
        while (lerp < 1)
        {
        currentPositionL = Vector3.Lerp(currentPositionL, newPositionL, lerp);
        currentPositionL.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
        lerp += Time.deltaTime * speed;
        yield return null;
        }
        //yield return new WaitForSeconds(0.2f);        
        LStepping = false;
        yield return null;
    }

    IEnumerator StepR()
    {
        RStepping = true;
        Vector3 midpos = (currentPositionR + newPositionR)/2;
        //currentPositionR = new Vector3(midpos.x, midpos.y + stepHeight, midpos.z);
        //yield return new WaitForSeconds(0.3f);
        //currentPositionR = newPositionR;
        float lerp = 0;
        while (lerp < 1)
        {
            currentPositionR = Vector3.Lerp(currentPositionR, newPositionR, lerp);
            currentPositionR.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
            lerp += Time.deltaTime * speed;
            yield return null;
        }
        
        //yield return new WaitForSeconds(0.2f);
        RStepping = false;
        yield return null;
    }
}
