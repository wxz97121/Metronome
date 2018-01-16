using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public GameObject Toshow;
    public PlayButton Level1, Level2, Level3;
    public Dropdown P1, P2, P3, P4;
    public void SelectPlayer(int num)
    {
        UpdateControl();
        gameObject.SetActive(false);
        Toshow.SetActive(true);
        if (num==2)
        {
            Level1.ToScene = "demo";
            Level2.ToScene = "2.0fallDemo";
            Level3.ToScene = "2.0iceDemo";
        }
        else if (num==3)
        {
            Level1.ToScene = "3Pdemo";
            Level2.ToScene = "2.0fallDemo3P";
            Level3.ToScene = "2.0iceDemo3P";
        }
        else if (num==4)
        {
            Level1.ToScene = "4Pdemo";
            Level2.ToScene = "2.0fallDemo4P";
            Level3.ToScene = "2.0awayDemo4P";
        }

    }
    void UpdateControl()
    {
        PlayerPrefs.SetInt("Player1_Input", P1.value);
        PlayerPrefs.SetInt("Player2_Input", P2.value);
        PlayerPrefs.SetInt("Player3_Input", P3.value);
        PlayerPrefs.SetInt("Player4_Input", P4.value);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
