using UnityEngine;
using System.Collections;

public class DeathColl : MonoBehaviour {
    public int Force;
    public int Damage=5;
    public bool IgnoreDisbale=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && other.GetComponent<Character2D>().HP != 0)
        {
            other.GetComponent<Character2D>().Damage(-Damage, transform, IgnoreDisbale);
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Force));
        }
        else if (other.tag == "Box") Destroy(other.gameObject);
    }
}
