using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Ice : MonoBehaviour
{
    private GameObject Player1, Player2;
    private float nowTime = 0;
    private bool isDisappearing;
    public float minExistTime, maxExistTime, minDisappearTime, maxDisappearTime;
    IEnumerator DisAppear()
    {
        isDisappearing = true;
        float ExistTime = Random.Range(minExistTime, maxExistTime);
        GetComponent<SpriteRenderer>().DOFade(0.2f, ExistTime);
        yield return new WaitForSeconds(ExistTime);

        GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.35f);
        GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(0.15f);

        yield return new WaitForSeconds(Random.Range(minDisappearTime, maxDisappearTime));

        GetComponent<SpriteRenderer>().DOFade(1, 0.15f);
        yield return new WaitForSeconds(0.15f);
        GetComponent<Collider2D>().isTrigger = false;
        isDisappearing = false;
    }
    IEnumerator Show()
    {
        isDisappearing = true;

        yield return new WaitForSeconds(Random.Range(minDisappearTime, maxDisappearTime));

        GetComponent<SpriteRenderer>().DOFade(1, 0.35f);
        yield return new WaitForSeconds(0.35f);
        GetComponent<Collider2D>().enabled = true;
        isDisappearing = false;
    }

    private void FixedUpdate()
    {
        if (isDisappearing) return;
        //print(LayerMask.NameToLayer("Player"));
        if (GetComponent<BoxCollider2D>().IsTouching(Player1.GetComponent<Collider2D>()) || GetComponent<BoxCollider2D>().IsTouching(Player2.GetComponent<Collider2D>()))
        {
            print("YES");
            nowTime += Time.fixedDeltaTime;
            var tmpColor = GetComponent<SpriteRenderer>().color;
            tmpColor.a = (Mathf.Lerp(1, 0.1f, nowTime / minExistTime));
            GetComponent<SpriteRenderer>().color = tmpColor;
        }
        if (nowTime > minExistTime)
        {
            var tmpColor = GetComponent<SpriteRenderer>().color;
            tmpColor.a = 0;
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = tmpColor;
            nowTime = 0;
            StartCoroutine(Show());
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.collider.tag == "Player") isDisappearing = true;
    }
    private void Start()
    {
        //StartCoroutine(DisAppear());
    }
    private void Awake()
    {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
    }
}
