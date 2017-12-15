using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingDown : MonoBehaviour {


    public KeyCode goingDownKey;

    public float forceOfGoingDown;


    Rigidbody2D rigdidbodyOfMy;


    bool InWater;
    public bool isActive = false;


    private void Awake()
    {

        if (!(rigdidbodyOfMy = GetComponent<Rigidbody2D>())) print(1);
        isActive = false;
        InWater = false;


    }


    // Use this for initialization
    void Start () {





		
	}
	
	// Update is called once per frame
	void Update () {




		
	}



    private void FixedUpdate()
    {
        if (Input.GetKey(goingDownKey) && InWater && isActive)
        {


            rigdidbodyOfMy.AddForce(new Vector2(0, -forceOfGoingDown));


        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            InWater = true;
        }




    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            InWater = false;
        }

    }

}
