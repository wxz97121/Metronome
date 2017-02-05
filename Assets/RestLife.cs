using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RestLife : MonoBehaviour {
    public Sprite[] LifeSprite;
    public Character2D Player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Image>().sprite = LifeSprite[Player.life];
	}
}
