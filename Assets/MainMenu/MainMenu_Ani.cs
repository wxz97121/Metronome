using UnityEngine;
using System.Collections;

public class MainMenu_Ani : MonoBehaviour
{
    public Sprite[] AniSprite;
    private float LastTime = -1;
    private int nowSprite = 0;
    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        float nowTime = Time.time;
        if (nowTime > LastTime + 0.25f)
        {
            LastTime = nowTime;
            nowSprite = (nowSprite + 1) % AniSprite.Length;
            GetComponent<SpriteRenderer>().sprite = AniSprite[nowSprite];
        }
    }



}
