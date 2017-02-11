using UnityEngine;
using System.Collections;

public class HelpButton : MonoBehaviour {
    public GameObject HelpObject;
    public GameObject Main;
    // Use this for initialization

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.grey;
        
    }
    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Main.SetActive(false);
        HelpObject.SetActive(true);
    }
}
