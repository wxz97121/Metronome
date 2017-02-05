using UnityEngine;
using System.Collections;

public class Wine : MonoBehaviour {

    public Sprite[] WineSprite;
    private int nowWineSprite = 0;
    private SpriteRenderer WineRenderer;
    //public float Rate = 0.2f;
    public float Duration = 5;
    private bool used = false;
    // Use this for initialization
    void Start()
    {
        WineRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WineUpdate());
    }
    IEnumerator WineUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (used) yield break;
            nowWineSprite = (nowWineSprite + 1) % WineSprite.Length;
            WineRenderer.sprite = WineSprite[nowWineSprite];
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;
        if (other.gameObject.tag == "Player")
        {
            used = true;
            other.GetComponent<Character2D>().wined = true;
            other.GetComponent<Character2D>().m_MaxSpeed *= 2f;
            other.GetComponent<Character2D>().MoveForce *= 1f;
            Destroy(WineRenderer);
            Destroy(GetComponent<SpriteRenderer>());
            StartCoroutine(reset(other.GetComponent<Character2D>()));
        }
    }
    // Update is called once per frame
    public IEnumerator reset(Character2D m_Player)
    {
        yield return new WaitForSeconds(Duration);
        m_Player.wined = false;
        m_Player.m_MaxSpeed /= 2f;
        m_Player.MoveForce /= 1f;
        Destroy(gameObject);
    }
}
