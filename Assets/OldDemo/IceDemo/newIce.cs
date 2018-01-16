using System.Collections;
using UnityEngine;
using DG.Tweening;

public class newIce : MonoBehaviour
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
        foreach (var Coli in GetComponentsInChildren<Collider2D>())
            Coli.isTrigger = true;
        yield return new WaitForSeconds(0.15f);

        yield return new WaitForSeconds(Random.Range(minDisappearTime, maxDisappearTime));

        GetComponent<SpriteRenderer>().DOFade(1, 0.15f);
        yield return new WaitForSeconds(0.15f);
        foreach (var Coli in GetComponentsInChildren<Collider2D>())
            Coli.isTrigger = false;
        isDisappearing = false;
    }
    IEnumerator Show()
    {
        isDisappearing = true;

        yield return new WaitForSeconds(Random.Range(minDisappearTime, maxDisappearTime));

        GetComponent<SpriteRenderer>().DOFade(1, 0.35f);
        yield return new WaitForSeconds(0.35f);
        foreach (var Coli in GetComponentsInChildren<Collider2D>())
            Coli.enabled = true;
        isDisappearing = false;
    }

    private void FixedUpdate()
    {
        //if (isDisappearing) return;
        ////print(LayerMask.NameToLayer("Player"));
        //float MeltingSpeed = 0;
        //if (GetComponent<BoxCollider2D>().IsTouching(Player1.GetComponent<Collider2D>()))
        //    MeltingSpeed += Player1.GetComponent<Character2D>().meltSpeed;
        //if (GetComponent<BoxCollider2D>().IsTouching(Player2.GetComponent<Collider2D>()))
        //    MeltingSpeed += Player1.GetComponent<Character2D>().meltSpeed; 
        //nowTime += Time.fixedDeltaTime*MeltingSpeed;
        //var tmpColor = GetComponent<SpriteRenderer>().color;
        //tmpColor.a = (Mathf.Lerp(1, 0.1f, nowTime / minExistTime));
        //GetComponent<SpriteRenderer>().color = tmpColor;
        //if (nowTime > minExistTime)
        //{
        //    tmpColor = GetComponent<SpriteRenderer>().color;
        //    tmpColor.a = 0;
        //    foreach (var Coli in GetComponentsInChildren<Collider2D>())
        //        Coli.enabled = false;
        //    GetComponent<SpriteRenderer>().color = tmpColor;
        //    nowTime = 0;
        //    StartCoroutine(Show());
        //}
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!isDisappearing && collision.collider.tag == "Player")
            StartCoroutine(DisAppear());
        //if (collision.collider.tag == "Player") isDisappearing = true;
    }
    private void Start()
    {

    }
    private void Awake()
    {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
    }
}
