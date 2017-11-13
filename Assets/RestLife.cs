using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RestLife : MonoBehaviour {
    public Sprite[] LifeSprite;
    public Character2D Player;
	void Update () {
        GetComponent<Image>().sprite = LifeSprite[Player.life];
	}
}
