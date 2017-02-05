using UnityEngine;
using System.Collections;

public class MainMenu_Ani : MonoBehaviour {
    public Sprite[] AniSprite;
	// Use this for initialization
	void Start () {
        StartCoroutine(UpdateAni());
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator UpdateAni()
    {
        int nowSprite = 0;
        while(true)
        {
            yield return new WaitForSeconds(0.25f);
            nowSprite = (nowSprite + 1) % AniSprite.Length;
            GetComponent<SpriteRenderer>().sprite = AniSprite[nowSprite];
        }
    }


}
