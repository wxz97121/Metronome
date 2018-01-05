using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWave : MonoBehaviour
{
    private int direcion = 1;
    [HideInInspector]
    public float speed = 1;
    public AudioClip Tap;
    public AudioClip Coll;
    public AudioClip Hit;
    public bool isHammer;
    public Sprite HammerSprite;
    public Sprite normalHammer;
    public float multiple = 1;
    private float nowTime;
    //public float newWave_c;
    private Vector2[] BigPoly = { new Vector2(-64, 250), new Vector2(-202, 158), new Vector2(-146, 86), new Vector2(-16, 167) };
    private Vector2[] SmallPoly = { new Vector2(-129, 143), new Vector2(-108, 111), new Vector2(-60, 145), new Vector2(-77, 180) };
    public GameObject Boom;
    [HideInInspector]
    public Transform RotateTransform;
    private void Awake()
    {
        RotateTransform = transform.parent;
    }
    public void ChangeHammer()
    {
        if (!isHammer)
        {
            GetComponent<SpriteRenderer>().sprite = HammerSprite;
            GetComponent<PolygonCollider2D>().SetPath(0, BigPoly);
            isHammer = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = normalHammer;
            GetComponent<PolygonCollider2D>().SetPath(0, SmallPoly);
            isHammer = false;
        }
    }
    // Use this for initialization
    void Start()
    {
        nowTime = 0;
        speed = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMode_base>().wavespeed;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //如果正在向上摆锤，什么都不做
        if (RotateTransform.eulerAngles.z < 90 && direcion == -1) return;
        if (RotateTransform.eulerAngles.z > 270 && direcion == 1) return;
        //如果砸到人
        if (other.tag == "Player" && GetComponentInParent<Character2D>().Disable == false)
        {
            int FaceDir = GetComponentInParent<Character2D>().m_FacingRight ? -1 : 1;
            //如果是跳劈
            if (GetComponentInParent<Rigidbody2D>().velocity.y < 0
                && GetComponentInParent<Character2D>().transform.position.y > other.transform.position.y + 0.15
                && direcion * FaceDir > 0
                && GetComponentInParent<Character2D>().m_Grounded == false)
            {
                Instantiate(Boom, other.transform.position, Quaternion.identity, other.transform);
                other.GetComponent<Character2D>().Damage(-GetComponentInParent<Character2D>().UpAttackDamage, GetComponentInParent<Character2D>().transform);
                //Debug.Break();
                //print("Ahhhh!");

            }
            else other.GetComponent<Character2D>().Damage(-GetComponentInParent<Character2D>().AttackDamage, GetComponentInParent<Character2D>().transform);
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<AudioSource>().clip = Hit;
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().Play();
            direcion *= -1;
        }
        //如果砸到武器
        else if (other.tag == "Weapon")
        {
            //注意到当一个摆锤成功弹开另一个的时候 因为你已经修改了自身的Dir，所以事件只会处理一次
            if (other.GetComponent<NewWave>() != null)
            {
                //Debug.Break();
                print(1);
                if (other.GetComponent<NewWave>().RotateTransform.eulerAngles.z < 90 && other.GetComponent<NewWave>().direcion == -1) return;
                if (other.GetComponent<NewWave>().RotateTransform.eulerAngles.z > 270 && other.GetComponent<NewWave>().direcion == 1) return;
                if (!other.GetComponent<NewWave>().isHammer) other.GetComponent<NewWave>().direcion *= -1;
                print(2);
            }
            GetComponent<AudioSource>().clip = Coll;
            GetComponent<AudioSource>().volume = 0.45f;
            GetComponent<AudioSource>().Play();
            if (!isHammer) direcion *= -1;
            
        }
        //如果砸到怪物
        else if (other.tag == "Enemy")
        {

            other.GetComponent<Enemy>().HasBeenAttack();
            if (!isHammer) direcion *= -1;
        }
        else if (other.tag == "Box")
        {
            float GoAwayDist = 90000;
            if (transform.position.x > other.transform.position.x) other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-GoAwayDist, 0));
            else other.GetComponent<Rigidbody2D>().AddForce(new Vector2(GoAwayDist, 0));
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<AudioSource>().clip = Hit;
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().Play();
            direcion *= -1;
        }
    }
    // direction=1 摆锤向左，Direction=-1 摆锤向右
    void FixedUpdate()
    {
        //print(RotateTransform.eulerAngles.z);
        //print(transform.parent.eulerAngles.z);
        if (GetComponentInParent<Character2D>().isStop) return;
        nowTime += Time.fixedDeltaTime;
        if (GetComponentInParent<Character2D>().Disable) return;
        if (RotateTransform.eulerAngles.z > 90 && direcion == 1 && RotateTransform.eulerAngles.z < 180)
        {
            GetComponent<AudioSource>().clip = Tap;
            GetComponent<AudioSource>().volume = 0.95f;
            GetComponent<AudioSource>().Play();
            nowTime = 0;
            direcion *= -1;
        }
        else if (RotateTransform.eulerAngles.z < 270 && direcion == -1 && RotateTransform.eulerAngles.z > 180)
        {
            GetComponent<AudioSource>().clip = Tap;
            GetComponent<AudioSource>().volume = 0.95f;
            GetComponent<AudioSource>().Play();
            nowTime = 0;
            direcion *= -1;
        }
        if (!GetComponentInParent<Character2D>().wined)
        {
            if (GetComponentInParent<Character2D>().newWave)
            {
                float Rate = 0.9f / speed * 4;
                float b = Mathf.PI * (6) / Rate / Rate;
                float a = -b / Rate; ;
                while (nowTime > Rate) nowTime -= Rate;
                RotateTransform.Rotate(new Vector3(0, 0, multiple * Time.fixedDeltaTime * direcion * (nowTime * nowTime * a + nowTime * b) * 180 / Mathf.PI));
            }
            else RotateTransform.Rotate(new Vector3(0, 0, direcion * speed * multiple));
        }
        else
            RotateTransform.Rotate(new Vector3(0, 0, direcion * (0.5f + 5 * Mathf.Abs(Mathf.Cos(RotateTransform.eulerAngles.z / 360 * 2 * Mathf.PI)))));
    }
}
