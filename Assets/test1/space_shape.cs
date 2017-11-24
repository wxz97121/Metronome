using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class space_shape : MonoBehaviour {


	public KeyCode UP;
	public KeyCode Down;
	public GameMode3 gameMode;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

		if (Input.GetKey(UP))
		{
			rigidbody.AddForce(new Vector2(0,gameMode.moveforce));
		}
		if (Input.GetKey(Down))
		{
			rigidbody.AddForce(new Vector2(0, -gameMode.moveforce));
		}

		if (rigidbody.velocity.magnitude >= gameMode.maxspeed)
		{
			rigidbody.velocity = rigidbody.velocity.normalized * gameMode.maxspeed;
		}


	}



}
