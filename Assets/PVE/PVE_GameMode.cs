using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVE_GameMode : GameMode_base {
    public Vector3 Respawn_Pos;
	public Character2D m_Char;
    //public override Vector3 RespawnLocation()
    //{
    //    return Respawn_Pos;
    //}
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Char.HP == 0)
			m_Char.StartCoroutine (m_Char.Respawn());
	}
}
