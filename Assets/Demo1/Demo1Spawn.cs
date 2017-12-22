using UnityEngine;
using System.Collections;

public class Demo1Spawn : MonoBehaviour
{
    public bool Preparing = false;
    public bool PreparingWine = false;
    public GameObject yincha;
    public float yincha_Time = 10;
    public GameObject Wine;
    public float Wine_Time = 8;
    public float minForce = 40000;
    public float maxForce = 1000000;
    public Transform[] ForkPos;
    IEnumerator RefreshWine()
    {
        yield return new WaitForSeconds(Wine_Time);
        if (Random.value > 0.5)
        {
            GameObject newWine = Instantiate(Wine, new Vector3(-2300, -718, 0), new Quaternion());
            newWine.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(minForce, maxForce), 0));
        }
        else
        {
            GameObject newWine = Instantiate(Wine, new Vector3(1600, -718, 0), new Quaternion());
            newWine.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * Random.Range(minForce, maxForce), 0));
        }
        PreparingWine = false;
    }

    IEnumerator Refresh(GameObject Item)
    {
        yield return new WaitForSeconds(yincha_Time);
        GameObject.Instantiate(yincha, new Vector3(Random.Range(-1700, 1000), 900, 0), new Quaternion());
        //GameObject.Instantiate(yincha, new Vector3(-302, 900, 0), new Quaternion());
        //int now = (int) Mathf.Floor(Random.Range(0,ForkPos.Length-0.001f));
        //GameObject.Instantiate(yincha,ForkPos[now].position , new Quaternion());
        Preparing = false;
    }
    void Update()
    {
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
    }
}
