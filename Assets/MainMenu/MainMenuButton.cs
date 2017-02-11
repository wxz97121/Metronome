using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {
    public Sprite Normal;
    public Sprite Over;
    public GameObject Maker;
    public GameObject Main;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = Over;
    }
    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = Normal;
    }
    void OnMouseDown()
    {
        if (gameObject.name == "maker1")
        {
            Main.SetActive(false);
            Maker.SetActive(true);
        }
        else SceneManager.LoadScene("Demo");
    }
}
