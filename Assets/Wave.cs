using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private int direcion = 1;
    [HideInInspector]
    public float speed = 1;
    public AudioClip Tap;
    public AudioClip Coll;
    public AudioClip Hit;
    public bool isHammer;
    public float multiple = 1;
    private float nowTime = 0;
    //public float Wave_c;
    //private Vector2[] BigPoly = { new Vector2(-64, 250), new Vector2(-202, 158), new Vector2(-146, 86), new Vector2(-16, 167) };
    //private Vector2[] SmallPoly = { new Vector2(-129, 143), new Vector2(-108, 111), new Vector2(-60, 145), new Vector2(-77, 180) };
    public GameObject Boom;
    [HideInInspector]
    public Transform StickTransform;
    private Character2D m_Character;
    private GameMode_base m_Gamemode;
    private void Awake()
    {
        m_Character = GetComponentInParent<Character2D>();
        m_Gamemode = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMode_base>();
        StickTransform = transform.parent;
        Init();
    }
    public void Init()
    {
        if (isHammer) ChangeHammer();
        StickTransform.eulerAngles.Set(0, 0, 0);
    }
    public void ChangeHammer()
    {
        if (!isHammer)
        {
            transform.localScale *= 2f;
            isHammer = true;
        }
        else
        {
            transform.localScale /= 2f;
            isHammer = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //如果正在向上摆锤，什么都不做
        if (StickTransform.eulerAngles.z < 90 && direcion == -1) return;
        if (StickTransform.eulerAngles.z > 270 && direcion == 1) return;
        //如果砸到人
        if (other.tag == "Player" && m_Character.Disable == false)
        {
            //var m_Dist = Physics2D.Distance(GetComponent<Collider2D>(), other);
            //print(m_Dist.pointA);
            //print(m_Dist.pointB);
            //print(m_Dist.distance);
            //print((m_Dist.pointB - m_Dist.pointA) * m_Dist.distance);
            //Debug.Break();
            int FaceDir = m_Character.m_FacingRight ? -1 : 1;
            Character2D otherChar = other.GetComponent<Character2D>();
            //如果是跳劈
            if (m_Character.GetComponent<Rigidbody2D>().velocity.y < 0
                && m_Character.transform.position.y > other.transform.position.y + 0.15
                && direcion * FaceDir > 0
                && m_Character.m_Grounded == false)
            {
                Instantiate(Boom, other.transform.position, Quaternion.identity, other.transform);
                otherChar.Damage(-m_Character.UpAttackDamage, m_Character.transform);
                //Debug.Break();
                //print("Ahhhh!");

            }
            else otherChar.Damage(-m_Character.AttackDamage, m_Character.transform);
            m_Character.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<AudioSource>().clip = Hit;
            GetComponent<AudioSource>().volume = 1;
            GetComponent<AudioSource>().Play();
            direcion *= -1;
        }
        //如果砸到武器
        else if (other.tag == "Weapon")
        {
            var OtherWave = other.GetComponent<Wave>();
            //注意到当一个摆锤成功弹开另一个的时候 因为你已经修改了自身的Dir，所以事件只会处理一次
            if (other.GetComponent<Wave>() != null)
            {
                //Debug.Break();
                //print(1);
                if (OtherWave.StickTransform.eulerAngles.z < 90 && other.GetComponent<Wave>().direcion == -1) return;
                if (OtherWave.StickTransform.eulerAngles.z > 270 && other.GetComponent<Wave>().direcion == 1) return;
                if (!OtherWave.isHammer) other.GetComponent<Wave>().direcion *= -1;
                //print(2);
            }
            GetComponent<AudioSource>().clip = Coll;
            GetComponent<AudioSource>().volume = 0.45f;
            GetComponent<AudioSource>().Play();
            if (!isHammer) direcion *= -1;

        }
        if (other.GetComponent<BeAttack>() != null)
        {
            other.GetComponent<BeAttack>().BeAttackEvent.Invoke();
            if (other.GetComponent<BeAttack>().Rebound)
            {
                GetComponent<AudioSource>().clip = Hit;
                GetComponent<AudioSource>().volume = 1;
                GetComponent<AudioSource>().Play();
                direcion *= -1;
            }
        }
    }
    // direction=1 摆锤向左，Direction=-1 摆锤向右
    void FixedUpdate()
    {
        //print(RotateTransform.eulerAngles.z);
        //print(transform.parent.eulerAngles.z);
        if (m_Character.isStop) return;
        nowTime += Time.fixedDeltaTime;
        if (m_Character.Disable) return;
        if (StickTransform.eulerAngles.z > 90 && direcion == 1 && StickTransform.eulerAngles.z < 180)
        {
            GetComponent<AudioSource>().clip = Tap;
            GetComponent<AudioSource>().volume = 0.95f;
            GetComponent<AudioSource>().Play();
            nowTime = 0;
            direcion *= -1;
        }
        else if (StickTransform.eulerAngles.z < 270 && direcion == -1 && StickTransform.eulerAngles.z > 180)
        {
            GetComponent<AudioSource>().clip = Tap;
            GetComponent<AudioSource>().volume = 0.95f;
            GetComponent<AudioSource>().Play();
            nowTime = 0;
            direcion *= -1;
        }
        if (!m_Character.wined)
        {
            //基于二次函数的摆动
            float Rate = 0.9f / speed * 4;
            float b = Mathf.PI * (6) / Rate / Rate;
            float a = -b / Rate; ;
            while (nowTime > Rate) nowTime -= Rate;
            StickTransform.Rotate(new Vector3(0, 0, multiple * Time.fixedDeltaTime * direcion * (nowTime * nowTime * a + nowTime * b) * 180 / Mathf.PI));
            //原来的匀速摆动
            //else StickTransform.Rotate(new Vector3(0, 0, direcion * speed * multiple));
        }
        else
            StickTransform.Rotate(new Vector3(0, 0, direcion * (0.5f + 5 * Mathf.Abs(Mathf.Cos(StickTransform.eulerAngles.z / 360 * 2 * Mathf.PI)))));
    }
}
