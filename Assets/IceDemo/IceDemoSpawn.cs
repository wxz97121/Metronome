using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceDemoSpawn : MonoBehaviour {
    public GameObject Item;
    public float RefreshTime;
    public bool Preparing;
    IEnumerator Refresh(GameObject Item)
    {
        yield return new WaitForSeconds(RefreshTime);
        Instantiate(Item, new Vector3(Random.Range(-2000, 1300), 900, 0), new Quaternion());
        //GameObject.Instantiate(yincha, new Vector3(-302, 900, 0), new Quaternion());
        //int now = (int) Mathf.Floor(Random.Range(0,ForkPos.Length-0.001f));
        //GameObject.Instantiate(yincha,ForkPos[now].position , new Quaternion());
        Preparing = false;
    }
    void Update()
    {
        if (!Preparing)
            if (GameObject.FindGameObjectWithTag(Item.tag) == null)
            {
                Preparing = true;
                StartCoroutine(Refresh(Item));
            }
    }
}
