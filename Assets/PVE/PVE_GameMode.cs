using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVE_GameMode : GameMode_base {
    public Vector3 Respawn_Pos;
    public override Vector3 RespawnLocation()
    {
        return Respawn_Pos;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
