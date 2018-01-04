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
    public float minUptime;
    public float maxUptime;
    private float Lastx = 100000;
    [HideInInspector]
    public GameObject LastPlat;
    public float DistLimit;
    public float RespawnHeight = 550;
    // Use this for initialization
    public float BoardLen;
    public float POfShake = 0.08f;
    public float minXSpeed = 2000;
    public float maxXSpeed = 2250;
    public GameObject LRPlatformPrefab;
    void Start()
    {
        StartCoroutine(Refresh());
    }
    private int Refreshindex = 0;
    IEnumerator Refresh()
    {
        while (true)
        {
            //for(int i=0; i < 2; i++)

            float Newx = Random.Range(LeftPos.position.x, RightPos.position.x);
            while (Mathf.Abs(Newx - Lastx) < DistLimit)
            {
                Newx = Random.Range(LeftPos.position.x, RightPos.position.x);
            }
            //if (i == 1)
            //{
            //    while (Mathf.Abs(Newx-positionX1) < (378*2))
            //    {
            //        Newx = Random.Range(LeftPos.position.x, RightPos.position.x);
            //    }
            //}

            Lastx = Newx;
            GameObject newPlat = Instantiate(PlatformPrefab, new Vector3(Lastx, LeftPos.position.y, 100), Quaternion.identity);
            var m_Comp = newPlat.AddComponent<RisingPlatform>();
            m_Comp.BeginPos = newPlat.transform.position;
            m_Comp.TargetPos = newPlat.transform.position + new Vector3(0, 2700, 0);
            //if (Random.value < POfShake) m_Comp.TargetPos += new Vector3((Random.value > 0.5 ? 1 : -1) * Random.Range(minXSpeed, maxXSpeed), 0, 0);
            m_Comp.MoveTime = Random.Range(minUptime, maxUptime);
            LastPlat = newPlat;
            //Random.InitState(((int)LastPlat.transform.position.x));

            //if (Random.value < POfShake) //有概率从左右刷出
            //{
            //    GameObject newLRPlat = Instantiate(LRPlatformPrefab, new Vector3(2316, -114, 0), Quaternion.identity);
            //    m_Comp = newLRPlat.AddComponent<RisingPlatform>();
            //    m_Comp.BeginPos = newLRPlat.transform.position;
            //    m_Comp.TargetPos = newLRPlat.transform.position - new Vector3(5000, 0, 0);
            //    //if (Random.value < POfShake) m_Comp.TargetPos += new Vector3((Random.value > 0.5 ? 1 : -1) * Random.Range(minXSpeed, maxXSpeed), 0, 0);
            //    m_Comp.MoveTime = Random.Range(minUptime, maxUptime);
            //}

            yield return new WaitForSeconds(Random.Range(MinRefreshTime, MaxRefreshTime));
            //Refreshindex = (Refreshindex + 1) % PlatformPrefab.Length;
        }
    }
    [SerializeField]
    public void FallRefresh()
    {
        GetComponent<GameMode_base>().RespawnLocation = LastPlat.transform.position + new Vector3(Random.Range(-BoardLen / 2, BoardLen / 2), RespawnHeight, 0);
    }
}
