using UnityEngine;
using System.Collections;

public class Demo2Spawn : MonoBehaviour {
    private bool PreparingHammer = false;
    public GameObject Hammer;
    public float Hammer_Time = 10;
    private bool PreparingWine = false;
    public GameObject Wine;
    public float Wine_Time = 8;
    // Use this for initialization
    IEnumerator RefreshWine()
    {
        yield return new WaitForSeconds(Wine_Time);
        if (Random.value > 0.5)
        {
            GameObject newWine = (GameObject)GameObject.Instantiate(Wine, new Vector3(-2000, 90, 0), new Quaternion());
        }
        else
        {
            GameObject newWine = (GameObject)GameObject.Instantiate(Wine, new Vector3(1300, 90, 0), new Quaternion());
        }
        PreparingWine = false;
    }
    IEnumerator RefreshHammer()
    {
        yield return new WaitForSeconds(Hammer_Time);
        Instantiate(Hammer, new Vector3(Random.Range(-800, 0), 900, 0), new Quaternion());
        PreparingHammer = false;
    }

    void Update()
    {
        if (!PreparingWine)
            if (GameObject.FindGameObjectWithTag("Wine") == null)
            {
                PreparingWine = true;
                StartCoroutine(RefreshWine());
            }
        if (!PreparingHammer)
            if (GameObject.FindGameObjectWithTag("Hammer") == null)
            {
                PreparingHammer = true;
                StartCoroutine(RefreshHammer());
            }
    }
    }
