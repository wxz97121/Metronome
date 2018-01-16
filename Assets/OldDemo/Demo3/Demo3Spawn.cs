using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo3Spawn : MonoBehaviour {
    private bool PreparingCopter = false;
    public GameObject Copter;
    public float Copter_Time = 8;
    // Use this for initialization
    IEnumerator RefreshCopter()
    {
        yield return new WaitForSeconds(Copter_Time);
        if (Random.value > 0.5)
        {
            GameObject newWine = (GameObject)GameObject.Instantiate(Copter, new Vector3(-1300, 505, 0), new Quaternion());
        }
        else
        {
            GameObject newWine = (GameObject)GameObject.Instantiate(Copter, new Vector3(850, 505, 0), new Quaternion());
        }
        PreparingCopter = false;
    }


    void Update()
    {
        if (!PreparingCopter)
            if (GameObject.FindGameObjectWithTag("Copter") == null)
            {
                PreparingCopter = true;
                StartCoroutine(RefreshCopter());
            }
    }
}
