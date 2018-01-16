using UnityEngine;
using System.Collections;

public class HammerColliderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (GetComponentInParent<Wave>().isHammer==true)
        GetComponentInParent<Wave>().OnTriggerEnter2D(other);
    }
}
