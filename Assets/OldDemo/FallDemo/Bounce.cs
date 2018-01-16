using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    Rigidbody2D rigidbodyPlayer;

    public Vector2 velocityOfBounce;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>())
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity += velocityOfBounce;
            }
            else
            {
                print("Can't Found Rigidbody");
            }
        }
    }
}
