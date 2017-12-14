using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Water : MonoBehaviour {
    public float LowTime, HighTime, ConvertTime;
    public float minYScale, MaxYScale;
    // Use this for initialization
    void Start () {
        var tmpScale = transform.localScale;
        tmpScale.y = minYScale;
        transform.localScale = tmpScale;
        StartCoroutine(Tidal());
	}
	IEnumerator Tidal()
    {
        while (true)
        {
            yield return new WaitForSeconds(LowTime);

            transform.DOScaleY(MaxYScale, ConvertTime);
            yield return new WaitForSeconds(ConvertTime);

            yield return new WaitForSeconds(HighTime);

            transform.DOScaleY(minYScale, ConvertTime);
            yield return new WaitForSeconds(ConvertTime);
            
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
