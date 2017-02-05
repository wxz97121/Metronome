using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameMode : MonoBehaviour
{
    public Character2D Player1;
    public Character2D Player2;
    public Sprite[] Win1Sprite;
    public Sprite[] Win2Sprite;
    public Sprite[] LoseSprite;
    public bool pause = false;
    public bool Preparing = false;
    public bool PreparingWine = false;
    public GameObject yincha;
    public float yincha_Time = 10;
    public GameObject Wine;
    public float Wine_Time = 8;
    public float minForce = 40000;
    public float maxForce = 1000000;

    public float gscale = 180;
    public float maxspeed = 1500;
    public float moveforce = 2000;
    public float jumpforce = 70000;
    public int goaway = 50000;
    public float wavespeed = 3;
    public float lineardrag = 0;
    // Use this for initialization
    void Start()
    {
        PreparingWine = false;
    }
    IEnumerator RefreshWine()
    {
        yield return new WaitForSeconds(Wine_Time);
        if (Random.value > 0.5)
        {
            GameObject newWine=(GameObject)GameObject.Instantiate(Wine, new Vector3(-2300, -718, 0), new Quaternion());
            newWine.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(minForce, maxForce), 0));
        }
        else
        {
            GameObject newWine = (GameObject)GameObject.Instantiate(Wine, new Vector3(1600, -718, 0), new Quaternion());
            newWine.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1*Random.Range(minForce, maxForce), 0));
        }
        PreparingWine = false;
    }

    IEnumerator Refresh(GameObject Item)
    {
        yield return new WaitForSeconds(yincha_Time);
        GameObject.Instantiate(yincha,new Vector3(Random.Range(-2000, 1300),900,0),new Quaternion());
        Preparing = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(PreparingWine);
        if (!Preparing)
            if (GameObject.FindGameObjectWithTag("Yincha") == null)
            {
                Preparing = true;
                StartCoroutine(Refresh(yincha));
            }
        if (!PreparingWine)
            if (GameObject.FindGameObjectWithTag("Wine") == null)
            {
                PreparingWine = true;
                StartCoroutine(RefreshWine());
            }
        if (Player1.HP == 0)
        {
            if (Player1.life == 1)
            {
                StartCoroutine(EndGame(2));
            }
            else if (Player1.life != 0)
            {
                Player1.life--;
                StartCoroutine(Player1.Respawn());
            }
        }
        if (Player2.HP == 0)
        {
            if (Player2.life == 1)
            {
                StartCoroutine(EndGame(1));
            }
            else if (Player2.life != 0)
            {
                Player2.life--;
                StartCoroutine(Player2.Respawn());
            }
        }


    }

    public IEnumerator EndGame(int Winner)
    {
        pause = true;
        Player1.Disable = true;
        Player2.Disable = true;
        int nowSprite1 = 0;
        int nowSprite2 = 0;
        if (Winner == 1)
        {
            Player2.life--;
            for (int i = 1; i <= 35; i++)
            {
                foreach (SpriteRenderer s in Player1.GetComponentsInChildren<SpriteRenderer>()) if (s.tag != "Player") Destroy(s);
                foreach (SpriteRenderer s in Player2.GetComponentsInChildren<SpriteRenderer>()) if (s.tag != "Player") Destroy(s);
                nowSprite1 = (nowSprite1 + 1) % Win1Sprite.Length;
                nowSprite2 = (nowSprite2 + 1) % LoseSprite.Length;
                Player1.GetComponent<SpriteRenderer>().sprite = Win1Sprite[nowSprite1];
                Player2.GetComponent<SpriteRenderer>().sprite = LoseSprite[nowSprite2];
                yield return new WaitForSeconds(0.2f);
            }

        }
        else if (Winner == 2)
        {
            Player1.life--;
            for (int i = 1; i <= 25; i++)
            {
                foreach (SpriteRenderer s in Player1.GetComponentsInChildren<SpriteRenderer>()) if (s.tag != "Player") Destroy(s);
                foreach (SpriteRenderer s in Player2.GetComponentsInChildren<SpriteRenderer>()) if (s.tag != "Player") Destroy(s);
                nowSprite1 = (nowSprite1 + 1) % Win2Sprite.Length;
                nowSprite2 = (nowSprite2 + 1) % LoseSprite.Length;
                Player2.GetComponent<SpriteRenderer>().sprite = Win2Sprite[nowSprite1];
                Player1.GetComponent<SpriteRenderer>().sprite = LoseSprite[nowSprite2];
                yield return new WaitForSeconds(0.2f);
            }
        }
        SceneManager.LoadScene("Demo2");
    }
}
