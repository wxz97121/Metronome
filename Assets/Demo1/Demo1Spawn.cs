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
    IEnumerator RefreshWine()
    {
        yield return new WaitForSeconds(Wine_Time);
        if (Random.value > 0.5)
        {
            GameObject newWine = (GameObject)GameObject.Instantiate(Wine, new Vector3(-2300, -718, 0), new Quaternion());
            newWine.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(minForce, maxForce), 0));
        }
        else
        {
            GameObject newWine = (GameObject)GameObject.Instantiate(Wine, new Vector3(1600, -718, 0), new Quaternion());
            newWine.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1 * Random.Range(minForce, maxForce), 0));
        }
        PreparingWine = false;
    }

    IEnumerator Refresh(GameObject Item)
    {
        yield return new WaitForSeconds(yincha_Time);
        GameObject.Instantiate(yincha, new Vector3(Random.Range(-2000, 1300), 900, 0), new Quaternion());
        Preparing = false;
    }
    void Update()
    {
        Debug.Log(PreparingWine);
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
