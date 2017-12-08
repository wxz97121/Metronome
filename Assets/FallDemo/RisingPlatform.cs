using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    public Vector3 BeginPos, TargetPos;
    public double MoveTime;
    private double BeginTime;

    private void Awake()
    {
        BeginTime = Time.time;
    }
    private void FixedUpdate()
    {
        //foreach (var m_Rigid in RigidOnPlatform)
        //    print(m_Rigid.velocity);
        double lamda = (Time.time - BeginTime) / MoveTime;
        if (lamda >= 0.99)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 newPos = Vector3.Lerp(BeginPos, TargetPos, (float)lamda);
            //Vector3 newSpeed = (newPos - transform.position) / Time.fixedDeltaTime;
            GetComponent<Rigidbody2D>().MovePosition(newPos);
            //print(newSpeed);
            //ClearSpeed(newSpeed);
        }
    }
}

