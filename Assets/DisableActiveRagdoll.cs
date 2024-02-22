using System.Collections;
using System.Collections.Generic;
using DitzelGames.FastIK;
using Unity.VisualScripting;
using UnityEngine;

public class DisableActiveRagdoll : MonoBehaviour
{
    Height_Balance_Force HeightBalanceScript;
    IKFootSolver iKFootSolverScript;
    FastIKFabric LIKScript;
    FastIKFabric RIKScript;

    public GameObject LFoot;
    public GameObject RFoot;
    
    public bool SetDeactive;

    public ConfigurableJoint configJoint;
    JointDrive XaxisDrive;
    JointDrive YZaxisDrive;
    Rigidbody rb;

    void Start()
    {
        HeightBalanceScript = GetComponent<Height_Balance_Force>();
        iKFootSolverScript = GetComponent<IKFootSolver>();
        LIKScript = LFoot.GetComponent<FastIKFabric>();
        RIKScript = RFoot.GetComponent<FastIKFabric>();
        XaxisDrive = configJoint.angularXDrive;
        YZaxisDrive = configJoint.angularYZDrive;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(WaitandDisable());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.impulse.magnitude);
        if (collision.gameObject.tag != "Player" && SetDeactive)
        {
            rb.AddForce(collision.impulse * 2, ForceMode.Impulse);
            StartCoroutine(WaitandDisable());
        }
    }

    IEnumerator WaitandDisable()
    {
        HeightBalanceScript.enabled = false;
        yield return new WaitForSeconds(4f);
        XaxisDrive.positionSpring = 0;
        XaxisDrive.positionDamper = 0;
        YZaxisDrive.positionSpring = 0;
        YZaxisDrive.positionDamper = 0;
        configJoint.angularXDrive = XaxisDrive;
        configJoint.angularYZDrive = YZaxisDrive;
        //yield return new WaitForSeconds(2f);
        iKFootSolverScript.enabled = false;
        LIKScript.enabled = false;
        RIKScript.enabled = false;
        yield return new WaitForSeconds(2f);
        StartCoroutine(WaitandEnable());
    }

    IEnumerator WaitandEnable()
    {
        yield return new WaitForSeconds(2f);
        XaxisDrive.positionSpring = 900;
        XaxisDrive.positionDamper = 100;
        YZaxisDrive.positionSpring = 900;
        YZaxisDrive.positionDamper = 100;
        configJoint.angularXDrive = XaxisDrive;
        configJoint.angularYZDrive = YZaxisDrive;
        yield return new WaitForSeconds(2f);
        iKFootSolverScript.enabled = true;
        HeightBalanceScript.enabled = true;
        //yield return new WaitForSeconds(2f);
        LIKScript.enabled = true;
        RIKScript.enabled = true;
    }
}
