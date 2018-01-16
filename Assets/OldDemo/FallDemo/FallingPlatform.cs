using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FallingPlatform : MonoBehaviour
{
    // Use this for initialization
    public float DownTime = 5;
    public float DownDist = 150;
    void Start()
    {
        StartCoroutine(Down());
    }
    IEnumerator Down()
    {
        while (true)
        {
            yield return new WaitForSeconds(DownTime);
            transform.DOMoveY(transform.position.y - DownDist, 0.15f, false);
        }
    }

}
