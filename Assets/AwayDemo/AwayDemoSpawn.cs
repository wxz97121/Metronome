using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwayDemoSpawn : MonoBehaviour {

    public float minRefreshTime,MaxRefreshTime;
    public GameObject Box;
	// Use this for initialization
	void Start () {
        StartCoroutine(AutoSpawn());
	}
	IEnumerator AutoSpawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minRefreshTime, MaxRefreshTime));
            Instantiate(Box, new Vector3(Random.Range(-1700, 1000), 900, 0), new Quaternion());
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
