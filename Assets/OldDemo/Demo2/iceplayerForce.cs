//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class iceplayerForce : MonoBehaviour {

//	public int playerLayer;

//	public float iceMoveForce;

//	public float iceMaxSpeed;

//	//public GameMode2 gameMode2;




//	[SerializeField]
//	private float MoveForce;
//	[SerializeField]
//	private float MaxSpeed;


//	private void Awake()
//	{
//		if(gameMode2!= null)
//		{
//			MoveForce = gameMode2.moveforce;
//			MaxSpeed = gameMode2.maxspeed;
//		}
//		else
//		{
//			print("gameMode2 is Null");
//		}

//	}

//	// Use this for initialization
//	void Start () {


		
//	}
	
//	// Update is called once per frame
//	void Update () {



		
//	}


//	private void OnCollisionEnter2D(Collision2D collision)
//	{
		
//		if(collision.gameObject.layer == playerLayer)
//		{

//			if (collision.gameObject.GetComponent<Character2D>())
//			{
//				Character2D playerNow = collision.gameObject.GetComponent<Character2D>();

//				playerNow.MoveForce = iceMoveForce;
//				playerNow.m_MaxSpeed = iceMaxSpeed;

//			}
//			else
//			{
//				print("找不到Character2S");
//			}


//		}

//	}

//	private void OnCollisionExit2D(Collision2D collision)
//	{

//		if (collision.gameObject.layer == playerLayer)
//		{

//			if (collision.gameObject.GetComponent<Character2D>())
//			{
//				Character2D playerNow = collision.gameObject.GetComponent<Character2D>();
				

//				playerNow.MoveForce = MoveForce;
//				playerNow.m_MaxSpeed = MaxSpeed;
//			}
//			else
//			{
//				print("找不到Character2S");
//			}


//		}


//	}
//}
