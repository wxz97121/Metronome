using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
    
public class GameMode_Fall : GameMode_base
{
    public float DebugSpeed;
    [HideInInspector]
    public Character2D Player1;
    [HideInInspector]
    public Character2D Player2;
    public Sprite[] Win1Sprite;
    public Sprite[] Win2Sprite;
    public Sprite[] LoseSprite;
    public float Respawnheight = 100;
    //如果要添加 Update,记得调用Base的Update
}
