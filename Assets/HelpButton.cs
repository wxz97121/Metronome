using UnityEngine;
using System.Collections;

public class HelpButton : MonoBehaviour {
    public GameObject HelpObject;
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
        HelpObject.SetActive(true);
    }
}
