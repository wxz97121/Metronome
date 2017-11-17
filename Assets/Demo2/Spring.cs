using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

	public int playerLayer;
	public Vector2 springSpeed;


	// Use this for initialization
	void Start () {



		
	}
	
	// Update is called once per frame
	void Update () {
		



	}

	private void OnTriggerEnter2D(Collider2D other)
	{

		if(other.gameObject.layer == playerLayer)
		{

		
			if (other.GetComponent<Rigidbody2D>())
			{
				nowFly(other.GetComponent<Rigidbody2D>());
			}
			else
			{
				print("can't found Rigidbody2D");
			}


		}



	}


	private void nowFly(Rigidbody2D player)
	{

		player.velocity = springSpeed;

	}
}
