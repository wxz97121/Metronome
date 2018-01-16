using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayButton : MonoBehaviour {
    public string ToScene;
    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
    }
    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.clear;
    }
    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        SceneManager.LoadScene(ToScene);
    }
}
