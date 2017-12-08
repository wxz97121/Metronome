using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRefresher : MonoBehaviour
{
    public Transform LeftPos;
    public Transform RightPos;
    public float MinRefreshTime = 2;
    public float MaxRefreshTime = 3;
    public GameObject PlatformPrefab;
    public double Uptime;
    private float Lastx = 100000;
    [HideInInspector]
    public GameObject LastPlat;
    public float DistLimit;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Refresh());
    }
    private int Refreshindex = 0;
    IEnumerator Refresh()
    {
        while (true)
        {

            float Newx = Random.Range(LeftPos.position.x, RightPos.position.x);
            while (Mathf.Abs(Newx - Lastx) < DistLimit)
            {
                Newx = Random.Range(LeftPos.position.x, RightPos.position.x);
            }
            Lastx = Newx;
            GameObject newPlat = Instantiate(PlatformPrefab, new Vector3(Lastx, LeftPos.position.y, 100), Quaternion.identity);
            var m_Comp = newPlat.AddComponent<RisingPlatform>();
            m_Comp.BeginPos = newPlat.transform.position;
            m_Comp.TargetPos = newPlat.transform.position + new Vector3(0, 2700, 0);
            m_Comp.MoveTime = Uptime;
            LastPlat = newPlat;
            yield return new WaitForSeconds(Random.Range(MinRefreshTime, MaxRefreshTime));
            //Refreshindex = (Refreshindex + 1) % PlatformPrefab.Length;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
