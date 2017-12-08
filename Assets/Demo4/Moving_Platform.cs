using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

public class Moving_Platform : MonoBehaviour
{
    List<Rigidbody2D> RigidOnPlatform;
    public Transform[] TargetPoint;
    private Vector3[] TargetPos;
    public double[] MoveTime;
    private double LastTime = 0;
    private int Nowindex = 0;
    //private float SumDist = 0;
    //private double SumTime = 0;
    //Moving_Platform(Transform m_Trans,double m_Time)
    //{
    //    TargetPoint = new Transform[0];
    //    TargetPoint[0] = m_Trans;
    //    MoveTime = new double[0];
    //    MoveTime[0] = m_Time;
    //}
    public void Awake()
    {
        Assert.IsTrue(MoveTime.Length == TargetPoint.Length);
        RigidOnPlatform = new List<Rigidbody2D>();
        LastTime = Time.time;
        Nowindex = 0;
        TargetPos = new Vector3[TargetPoint.Length];
        for (int i = 0; i < TargetPoint.Length; i++)
            TargetPos[i] = TargetPoint[i].position;
        transform.position = TargetPos[0];
    }

    // Update is called once per frame
    private void ClearSpeed(Vector3 newSpeed)
    {
        foreach (var m_Rigid in RigidOnPlatform)
        {
            m_Rigid.velocity = newSpeed;
        }
    }
    private void FixedUpdate()
    {
        //foreach (var m_Rigid in RigidOnPlatform)
        //    print(m_Rigid.velocity);
        double lamda = (Time.time - LastTime) / MoveTime[Nowindex];
        if (lamda > 1)
        {
            LastTime = Time.time;
            Nowindex = (Nowindex + 1) % TargetPos.Length;
            GetComponent<Rigidbody2D>().MovePosition(TargetPos[Nowindex]);
            Vector3 newSpeed = TargetPos[(Nowindex + 1) % TargetPos.Length] - TargetPos[Nowindex];
            newSpeed /= (float)MoveTime[Nowindex];
            //print(newSpeed);
            ClearSpeed(newSpeed);
        }
        else
        {
            Vector3 newPos = Vector3.Lerp(TargetPos[Nowindex], TargetPos[(Nowindex + 1) % TargetPos.Length], (float)lamda);
            //Vector3 newSpeed = (newPos - transform.position) / Time.fixedDeltaTime;
            GetComponent<Rigidbody2D>().MovePosition(newPos);
            //print(newSpeed);
            //ClearSpeed(newSpeed);
        }
    }
    IEnumerator DisAppear()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.35f);
        GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(0.15f);

        yield return new WaitForSeconds(1.5f);
        GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.15f);
        GetComponent<Collider2D>().isTrigger = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //print(collision.gameObject);
        RigidOnPlatform.Add(collision.otherRigidbody);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        RigidOnPlatform.Remove(collision.otherRigidbody);
    }
}
