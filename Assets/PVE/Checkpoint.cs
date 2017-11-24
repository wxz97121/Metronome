using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player") return;
		GameObject.FindGameObjectWithTag ("GameController").GetComponent<PVE_GameMode>().Respawn_Pos = transform.position;
		Destroy (gameObject);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
