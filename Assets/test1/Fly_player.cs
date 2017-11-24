using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_player : MonoBehaviour {


	[HideInInspector] public float enginePower;
	[HideInInspector] public float upAdd;
	[HideInInspector] public float hightAdd;




	public Fly_GameMode fly_GameMode;
	public KeyCode upKey;


	[SerializeField]private float nowPower;
	private float nowUpAdd;

	private float nowHightAdd;



	private float NowUpAdd()
	{
		if (fly_GameMode.upAddOn)
		{
			if (nowPower <= enginePower)
				nowPower += upAdd*Time.deltaTime;
			return nowPower/enginePower;

		}

		return enginePower;
	}
	private float NowHightAdd()
	{
		if (fly_GameMode.hightAddOn)
		{


		}
		return 1;

	}



	// Use this for initialization
	void Start () {

		nowPower = enginePower;
		
	}
	
	// Update is called once per frame
	void Update () {



		
	}

	private void FixedUpdate()
	{
		if (this.GetComponent<Rigidbody2D>())
		{

			Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

			if (Input.GetKey(upKey)){
				NowUpAdd();
				rigidbody.AddForce(new Vector2(0, enginePower));
			}

			if (fly_GameMode.upAddOn)
			{
				rigidbody.AddForce(new Vector2(0, nowPower));
			}


			if(nowPower >= 0)
			{
				nowPower -= fly_GameMode.updecrease*Time.deltaTime;
			}
			if(nowPower < 0)
			{
				nowPower = 0;
			}

		}
	}


	

}
