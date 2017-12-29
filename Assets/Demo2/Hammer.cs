using UnityEngine;
using System.Collections;

public class Hammer : MonoBehaviour {

    public Sprite[] HammerSprite;
    private int nowSprite = 0;
    public float Duration = 10;
    private bool used = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(HammerUpdate());
    }
    IEnumerator HammerUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (used) yield break;
            nowSprite = (nowSprite + 1) % HammerSprite.Length;
            GetComponent<SpriteRenderer>().sprite = HammerSprite[nowSprite];
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;
        if (other.gameObject.tag == "Player")
        {
            used = true;
            other.GetComponentInChildren<Wave>().ChangeHammer();
            other.GetComponent<Character2D>().AttackDamage = 20;
            //other.GetComponent<Character2D>().DamageTime = 1.2f;
            //other.GetComponent<Character2D>().GoAwayDist = 90000;
            Destroy(GetComponent<SpriteRenderer>());
            StartCoroutine(reset(other.GetComponent<Character2D>()));
        }
    }
    // Update is called once per frame
    public IEnumerator reset(Character2D m_Player)
    {
        yield return new WaitForSeconds(Duration);
        if (m_Player.GetComponentInChildren<Wave>().isHammer)
            m_Player.GetComponentInChildren<Wave>().ChangeHammer();
        if (m_Player.GetComponent<Character2D>().AttackDamage == 15)
            m_Player.GetComponent<Character2D>().AttackDamage = 5;
        m_Player.GetComponent<Character2D>().DamageTime = 0.9f;
        //m_Player.GetComponent<Character2D>().GoAwayDist = 65000;
        Destroy(gameObject);
    }
}
