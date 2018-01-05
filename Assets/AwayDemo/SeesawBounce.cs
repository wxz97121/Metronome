using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawBounce : MonoBehaviour
{

    public float multipleBounce;
    private float angularStart;
    private Rigidbody2D rigidbodyBounce;
    public float LowerAngle = -25, UpperAngle = 25;
    private Vector3 InitPos;
    private void Awake()
    {
        angularStart = transform.localEulerAngles.z;
        rigidbodyBounce = this.GetComponent<Rigidbody2D>();
        InitPos = rigidbodyBounce.position;

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float nowz = transform.localEulerAngles.z;
        while (nowz < 0) nowz += 360;
        while (nowz > 180) nowz -= 360;
        rigidbodyBounce.AddTorque((angularStart - nowz) * multipleBounce);
    }
    private void LateUpdate()
    {
        float nowz = transform.localEulerAngles.z;
        //while (nowz < 0) nowz += 360;
        //while (nowz > 180) nowz -= 360;
        if (nowz > 180 && nowz < 360 + LowerAngle) rigidbodyBounce.rotation = LowerAngle;
        if (nowz < 180 && nowz > UpperAngle) rigidbodyBounce.rotation = UpperAngle;
        rigidbodyBounce.position=InitPos;
        //print(transform.localEulerAngles.z);
    }
}
