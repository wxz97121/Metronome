using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copter : MonoBehaviour {
    public Sprite[] CopterSprite;
    private int nowCopterSprite = 0;
    private SpriteRenderer CopterRenderer;
    //public float Rate = 0.2f;
    public float Duration = 5;
    private bool used = false;
    public int Left;
    public int Right;
    public float Rate;
    public float Velocity;
    // Use this for initialization
    void Update()
    {
        if (transform.position.x < Left && Velocity < 0) Velocity *= -1;
        if (transform.position.x > Right && Velocity > 0) Velocity *= -1;
        transform.position += new Vector3(Velocity,0,0);
    }
    void Start()
    {
        CopterRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(CopterUpdate());
    }
    IEnumerator CopterUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (used) yield break;
            nowCopterSprite = (nowCopterSprite + 1) % CopterSprite.Length;
            CopterRenderer.sprite = CopterSprite[nowCopterSprite];
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;
        if (other.gameObject.tag == "Player")
        {
            used = true;
            other.GetComponent<Character2D>().isFly = true;
            other.GetComponent<Character2D>().CopterRenderer.gameObject.SetActive(true);
            other.GetComponent<Character2D>().ChangeRotateSpeed(0.9f / Rate);
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            Destroy(CopterRenderer);
            Destroy(GetComponent<SpriteRenderer>());
            StartCoroutine(reset(other.GetComponent<Character2D>()));
        }
    }
    // Update is called once per frame
    public IEnumerator reset(Character2D m_Player)
    {
        yield return new WaitForSeconds(Duration);
        m_Player.isFly = false;
        m_Player.CopterRenderer.gameObject.SetActive(false);
        m_Player.ChangeRotateSpeed(GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMode>().wavespeed);
        Destroy(gameObject);
    }
}
