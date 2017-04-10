using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour
{
    private int direcion = 1;
    public float speed = 1;
    public AudioClip Tap;
    public AudioClip Coll;
    public AudioClip Hit;
    public bool isHammer;
    public Sprite HammerSprite;
    public Sprite normalHammer;
    public bool newWave = false;
    private float nowTime;
    //public float newWave_c;
    public void ChangeHammer()
    {
        if (!isHammer)
        {
            GetComponent<SpriteRenderer>().sprite = HammerSprite;
            isHammer = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = normalHammer;
            isHammer = false;
        }
    }
    // Use this for initialization
    void Start()
    {
        nowTime = 0;
        speed = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMode>().wavespeed;
    }
    /*void HammerTrigged(Collider2D other)
    {
        if (!isHammer) return;
        if ((transform.eulerAngles.z < 60 || transform.eulerAngles.z > 330) && direcion == -1) return;
        if ((transform.eulerAngles.z > 240 && transform.eulerAngles.z < 330) && direcion == 1) return;
        if (other.tag == "Player" && GetComponentInParent<Character2D>().Disable == false)
        {
            other.GetComponent<Character2D>().Damage(-5, transform);
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<AudioSource>().clip = Hit;
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().Play();
            direcion *= -1;
        }
        else if (other.tag == "Weapon")
        {
            GetComponent<AudioSource>().clip = Coll;
            GetComponent<AudioSource>().volume = 0.45f;
            GetComponent<AudioSource>().Play();
            //direcion *= -1;
        }
    }*/
    public void OnTriggerEnter2D(Collider2D other)
    {
        if ((transform.eulerAngles.z < 60 || transform.eulerAngles.z > 330) && direcion == -1) return;
        if ((transform.eulerAngles.z > 240 && transform.eulerAngles.z < 330) && direcion == 1) return;
        if (other.tag == "Player" && GetComponentInParent<Character2D>().Disable == false)
        {
            other.GetComponent<Character2D>().Damage(-5, transform);
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<AudioSource>().clip = Hit;
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().Play();
            direcion *= -1;
        }
        else if (other.tag == "Weapon")
        {
            GetComponent<AudioSource>().clip = Coll;
            GetComponent<AudioSource>().volume = 0.45f;
            GetComponent<AudioSource>().Play();
            if (!isHammer) direcion *= -1;
        }
        //if (GetComponentInParent<Rigidbody2D>().velocity.y==0) direcion *= -1;
        //else kill Monster 
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        nowTime += Time.fixedDeltaTime;
        //print(nowTime);
        if (GetComponentInParent<Character2D>().Disable) return;

        if (transform.eulerAngles.z > 60 && direcion == 1 && transform.eulerAngles.z < 90)
        {
            GetComponent<AudioSource>().clip = Tap;
            GetComponent<AudioSource>().volume = 0.95f;
            GetComponent<AudioSource>().Play();
            //if (newWave) print(nowTime);
            nowTime = 0;
            direcion *= -1;
        }
        else if (transform.eulerAngles.z < 240 && direcion == -1 && transform.eulerAngles.z > 200)
        {
            GetComponent<AudioSource>().clip = Tap;
            GetComponent<AudioSource>().volume = 0.95f;
            GetComponent<AudioSource>().Play();
            //if (newWave) print(nowTime);
            nowTime = 0;
            direcion *= -1;
        }
        if (!GetComponentInParent<Character2D>().wined)
        {
            if (newWave)
            {
                float Rate = 0.9f / speed * 4;
                float b = Mathf.PI * (6) / Rate / Rate;
                float a = -b / Rate;
                //print(Rate);
                //print((nowTime * nowTime * a + nowTime * b));
                while (nowTime > Rate) nowTime -= Rate;
                //print(a);
                //print(newWave_c);
                //print(nowTime);
                //print(0.02f + nowTime * nowTime * a + newWave_c);
                transform.Rotate(new Vector3(0, 0, Time.fixedDeltaTime * direcion * (nowTime * nowTime * a + nowTime * b) * 180 / Mathf.PI));
            }
            else transform.Rotate(new Vector3(0, 0, direcion * speed));
        }
        else
            transform.Rotate(new Vector3(0, 0, direcion * (0.5f + 5 * Mathf.Abs(Mathf.Cos(transform.eulerAngles.z / 360 * 2 * Mathf.PI)))));
    }
}
