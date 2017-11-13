using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode_base : MonoBehaviour {

    public bool pause = false;
    public int maxJumpTimes = 2;
    public float DamageTime=0.75f;
    public float gscale = 590;
    public bool airControl = true;
    public float maxspeed = 550;
    public float moveforce = 15000;
    public float jumpforce = 1500;
    public float jumpforce2 = 1800;
    public int goaway = 30000;
    public float wavespeed = 4.5f;
    public float lineardrag = 0;
    public float flyForce=0;
    public AnimationCurve RushCurve;
    public float CD = 1;
    public abstract Vector3 RespawnLocation();

}
