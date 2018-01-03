//using UnityEngine;
//using System.Collections;
//using UnityEngine.SceneManagement;
//public class GameMode : GameMode_base
//{
//    [HideInInspector]
//    public Character2D Player1;
//    [HideInInspector]
//    public Character2D Player2;
//    public Sprite[] Win1Sprite;
//    public Sprite[] Win2Sprite;
//    public Sprite[] LoseSprite;
//    public float RespawnLeft = -2000;
//    public float RespawnRight = 1300;
//    public float Respawnheight = 900;
//    //public override Vector3 RespawnLocation()
//    //{
//    //    float x = Random.Range(RespawnLeft, RespawnRight);
//    //    float y = Respawnheight;
//    //    return new Vector3(x, y, 0);
//    //}
//    private void Awake()
//    {
//        Player1 = GameObject.Find("Player1").GetComponent<Character2D>();
//        Player2 = GameObject.Find("Player2").GetComponent<Character2D>();
//    }
//    //void Update()
//    //{
//    //    DebugSpeed = Player1.GetComponent<Rigidbody2D>().velocity.x;
//    //    if (Player1.HP <= 0)
//    //    {
//    //        if (Player1.life == 1)
//    //        {
//    //            StartCoroutine(EndGame(2));
//    //        }
//    //        else if (Player1.life != 0)
//    //        {
//    //            Player1.life--;
//    //            StartCoroutine(Player1.Respawn());
//    //        }
//    //    }
//    //    if (Player2.HP <= 0)
//    //    {
//    //        if (Player2.life == 1)
//    //        {
//    //            StartCoroutine(EndGame(1));
//    //        }
//    //        else if (Player2.life != 0)
//    //        {
//    //            Player2.life--;
//    //            StartCoroutine(Player2.Respawn());
//    //        }
//    //    }
//    //}
//    private void Update()
//    {
//        if (pause) return;
        
//        int nowAlivePlayer = 0;
//        GameObject Winner=null;
//        var AllPlayer = GameObject.FindGameObjectsWithTag("Player");
//        foreach (GameObject g in AllPlayer)
//        {
//            if (g.GetComponent<Character2D>().life > 0)
//            { nowAlivePlayer++; Winner = g; }
//        }
//        if (nowAlivePlayer == 1) StartCoroutine(EndGame(Winner));
//    }
//    public IEnumerator EndGame(GameObject WinnerObject)
//    {
//        if (WinnerObject == null) yield break;
//        pause = true;   
//        Player1.Disable = true;
//        Player2.Disable = true;
//        //int nowSprite1 = 0;
//        //int nowSprite2 = 0;
//        if (WinnerObject.GetComponent<Character2D>() != null)
//            WinnerObject.GetComponent<Character2D>().Win();
//        int Winner = WinnerObject.name[WinnerObject.name.Length - 1] - '0';
//        //if (Winner == 1)
//        //{
//        //    Player2.life--;
//        //    for (int i = 1; i <= 35; i++)
//        //    {
//        //        foreach (SpriteRenderer s in Player1.GetComponentsInChildren<SpriteRenderer>()) if (s.tag != "Player") Destroy(s);
//        //        foreach (SpriteRenderer s in Player2.GetComponentsInChildren<SpriteRenderer>()) if (s.tag != "Player") Destroy(s);
//        //        nowSprite1 = (nowSprite1 + 1) % Win1Sprite.Length;
//        //        nowSprite2 = (nowSprite2 + 1) % LoseSprite.Length;
//        //        Player1.GetComponent<SpriteRenderer>().sprite = Win1Sprite[nowSprite1];
//        //        Player2.GetComponent<SpriteRenderer>().sprite = LoseSprite[nowSprite2];
//        //        yield return new WaitForSeconds(0.2f);
//        //    }

//        //}
//        //else if (Winner == 2)
//        //{
//        //    Player1.life--;
//        //    for (int i = 1; i <= 25; i++)
//        //    {
//        //        foreach (SpriteRenderer s in Player1.GetComponentsInChildren<SpriteRenderer>()) if (s.tag != "Player") Destroy(s);
//        //        foreach (SpriteRenderer s in Player2.GetComponentsInChildren<SpriteRenderer>()) if (s.tag != "Player") Destroy(s);
//        //        nowSprite1 = (nowSprite1 + 1) % Win2Sprite.Length;
//        //        nowSprite2 = (nowSprite2 + 1) % LoseSprite.Length;
//        //        Player2.GetComponent<SpriteRenderer>().sprite = Win2Sprite[nowSprite1];
//        //        Player1.GetComponent<SpriteRenderer>().sprite = LoseSprite[nowSprite2];
//        //        yield return new WaitForSeconds(0.2f);
//        //    }
//        //}
//        yield return new WaitForSeconds(6);
//        SceneManager.LoadScene("MainMenu");
//    }
//}
