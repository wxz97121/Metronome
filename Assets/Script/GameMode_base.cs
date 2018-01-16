using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class GameMode_base : MonoBehaviour
{
    public bool pause = false;
    public int maxJumpTimes = 2;
    public float DamageTime = 0.75f;
    public float gscale = 1.425f;
    public bool airControl = true;
    public float maxspeed = 550;
    public float moveforce = 15000;
    public float jumpforce = 1500;
    public float jumpforce2 = 1800;
    public int goaway = 30000;
    public float wavespeed = 4.5f;
    public float lineardrag = 0;
    public float flyForce = 0;
    public AnimationCurve RushCurve;
    public float CD = 1;
    public float meltSpeed = 1;
    public int AttackDamage = 5;
    public int UpAttackDamage = 10;
    public int HP_of_Life = 15;
    public int InitLife = 3;
    [HideInInspector]
    public Vector3 RespawnLocation;
    //public abstract IEnumerator EndGame(string Winner);
    public UnityEvent RefreshLocation;
    private GameObject[] AllPlayer;
    public Vector3 GetRespawnLocation()
    {
        RefreshLocation.Invoke();
        return RespawnLocation;
    }
    private void Awake()
    {
        Random.InitState(19961018);
        AllPlayer = GameObject.FindGameObjectsWithTag("Player");
    }
    protected void Update()
    {
        if (pause) return;

        int nowAlivePlayer = 0;
        GameObject Winner = null;
        foreach (GameObject g in AllPlayer)
        {
            if (g.GetComponent<Character2D>().life > 0)
            { nowAlivePlayer++; Winner = g; }
        }
        if (nowAlivePlayer == 1) StartCoroutine(EndGame(Winner));
        if (nowAlivePlayer == 0) SceneManager.LoadScene("MainMenu");
    }
    public IEnumerator EndGame(GameObject WinnerObject)
    {
        if (WinnerObject == null) yield break;
        pause = true;
        if (WinnerObject.GetComponent<Character2D>() != null)
            WinnerObject.GetComponent<Character2D>().Win();
        int Winner = WinnerObject.name[WinnerObject.name.Length - 1] - '0';
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("MainMenu");
    }
}
