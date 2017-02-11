using UnityEngine;
using System.Collections;

public class HelpBack : MonoBehaviour {
    public GameObject Main;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Main.SetActive(true);
            gameObject.SetActive(false);
        }
	}
}
