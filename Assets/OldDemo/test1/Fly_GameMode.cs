using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_GameMode : MonoBehaviour {

	public Fly_player player1;
	public Fly_player player2;
	public float enginePower;
	public bool upAddOn;
	public float upAdd;
	public bool hightAddOn;
	public float hightAdd;
	public float updecrease;


	private void Awake()
	{


	}




	// Use this for initialization
	void Start () {

		if (player1 != null)
			player1.enginePower = enginePower;
		if (player2 != null)
			player2.enginePower = enginePower;
		if (upAddOn)
		{
			if (player1 != null) player1.upAdd = upAdd;
			if (player2 != null) player2.upAdd = upAdd;
		}
		if (hightAddOn)
		{
			if (player1 != null) player1.hightAdd = hightAdd;
			if (player2 != null) player2.hightAdd = hightAdd;
		}



	}

	// Update is called once per frame
	void Update () {








	}
}
