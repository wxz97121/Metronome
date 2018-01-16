using UnityEngine;
using System.Collections;

public class Tuningfork : MonoBehaviour
{
    public Sprite[] WaveSprite;
    private int nowWaveSprite = 0;
    public SpriteRenderer WaveRenderer;
    public float Rate = 0.2f;
    public float Duration = 5;
    private bool used = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaveUpdate());
    }
    IEnumerator WaveUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Rate);
            if (used) yield break;
            nowWaveSprite = (nowWaveSprite + 1) % WaveSprite.Length;
            WaveRenderer.sprite = WaveSprite[nowWaveSprite];
            if (transform.position.y < -2) Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;
        if (other.gameObject.tag == "Player")
        {
            used = true;
            other.GetComponent<Character2D>().ChangeRotateSpeed(0.9f / Rate);
            GetComponent<CircleCollider2D>().enabled = false;
            other.GetComponent<Character2D>().wined = false;
            Destroy(WaveRenderer);
            Destroy(GetComponent<SpriteRenderer>());
            StartCoroutine(reset(other.GetComponent<Character2D>()));
        }
    }
    // Update is called once per frame
    public IEnumerator reset(Character2D m_Player)
    {
        yield return new WaitForSeconds(Duration);
        m_Player.ChangeRotateSpeed(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMode_base>().wavespeed);
        Destroy(gameObject);
    }

}
