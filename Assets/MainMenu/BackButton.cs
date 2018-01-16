using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : PlayButton {
    public GameObject now;
    public GameObject Main;
    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        now.SetActive(false);
        Main.SetActive(true);
    }

}
