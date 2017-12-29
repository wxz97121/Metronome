using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawBounce : MonoBehaviour {

    public float multipleBounce;
    float angularStar;
    Rigidbody2D rigidbodyBounce;

    private void Awake()
    {
        angularStar = transform.localEulerAngles.z;
        rigidbodyBounce = this.GetComponent<Rigidbody2D>();        


    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float nowz = transform.localEulerAngles.z;
        while (nowz < 0) nowz += 360;
        while (nowz > 180) nowz -= 360;
        rigidbodyBounce.AddTorque((angularStar - nowz) * multipleBounce);

        


		
	}
}
