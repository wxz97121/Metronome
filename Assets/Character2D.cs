using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Character2D : MonoBehaviour
{
    #region Move_Detail
    [HideInInspector]
    public float m_MaxSpeed;
    [HideInInspector]
    public float MoveForce;
    [HideInInspector]
    public float m_RushSpeed;
    [HideInInspector]
    public float m_JumpForce;
    [HideInInspector]
    public float m_JumpForce2;
    [HideInInspector]
    public int GoAwayDist = 5;
    [HideInInspector]
    public bool Disable = false;
    [HideInInspector]
    public float DamageTime = 0;
    private int nowJumpTimes = 0;
    private int maxJumpTimes;
    private AnimationCurve RushCurve;
    public bool newWave = false;
    public bool newMove;
    public float flyForce;
    private bool isJumping = false;
    #endregion
    // The fastest the player can travel in the x axis.

    #region Ground_And_Ceil_Check
    [SerializeField]
    private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .05f; // Radius of the overlap circle to determine if grounded
    [HideInInspector]
    public bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    #endregion

    private bool isFlip = true;
    public int HP = 15;
    //private Animator m_Anim;            // Reference to the player's animator component.

    private Rigidbody2D m_Rigidbody2D;

    //public SpriteRenderer LegRenderer;
    public SpriteRenderer CopterRenderer;
    private int nowSprite = 0;
    public int life = 3;
    [HideInInspector]
    public int AttackDamage = 5;
    [HideInInspector]
    public int UpAttackDamage = 10;
    [HideInInspector]
    public bool wined = false;
    [HideInInspector]
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public Sprite[] Walking;
    public Sprite getDemage;
    private GameMode_base m_Gamemode;
    public Sprite[] WinedSprite;
    public Sprite[] normalMap;
    public Sprite[] WinSprite;
    public bool isFly;
    private float LastRushTime = -100;
    private bool Rushing = false;
    [HideInInspector]
    public float meltSpeed = 1;
    [HideInInspector]
    public bool isStop = false;
    [HideInInspector]
    public bool isDead = false;
    private bool isWin = false;
    //public float StopSecond = 0.5f;
    //public float HardAttackMultiple = 3;
    void Init()//复活和初次出现时更新各种值
    {
        HP = m_Gamemode.HPofLife;
        isDead = false;
        AttackDamage = m_Gamemode.AttackDamage;
        UpAttackDamage = m_Gamemode.UpAttackDamage;
        meltSpeed = m_Gamemode.meltSpeed;
        //transform.localScale = Vector3.one;
        isJumping = false;
        GetComponent<Rigidbody2D>().gravityScale = m_Gamemode.gscale;
        GetComponent<Rigidbody2D>().drag = m_Gamemode.lineardrag;
        ChangeRotateSpeed(m_Gamemode.wavespeed);
        flyForce = m_Gamemode.flyForce;
        m_MaxSpeed = m_Gamemode.maxspeed;
        MoveForce = m_Gamemode.moveforce;
        m_JumpForce = m_Gamemode.jumpforce;
        m_JumpForce2 = m_Gamemode.jumpforce2;
        GoAwayDist = m_Gamemode.goaway;
        maxJumpTimes = m_Gamemode.maxJumpTimes;
        GetComponent<Rigidbody2D>().gravityScale = m_Gamemode.gscale;
        RushCurve = m_Gamemode.RushCurve;
        wined = false;
        Disable = false;
        DamageTime = m_Gamemode.DamageTime;
        //isFly = false;
        isStop = false;
        ChangeRotateSpeed(m_Gamemode.wavespeed);
        if (CopterRenderer != null) CopterRenderer.gameObject.SetActive(false);
        m_Rigidbody2D.velocity = new Vector3(0, 0, 0);
    }
    public IEnumerator Respawn()
    {
        //print("???");
        //if (Disable) yield break;
        isDead = true;
        Rushing = false;
        GetComponent<AudioSource>().Play();
        transform.Rotate(0, 0, 90);
        if (GetComponentInChildren<NewWave>().isHammer)
            GetComponentInChildren<NewWave>().ChangeHammer();
        yield return new WaitForSeconds(3);
        if (m_Gamemode.pause || life <= 0) yield break;
        //life--;
        Init();
        transform.position = m_Gamemode.GetRespawnLocation();
        transform.Rotate(0, 0, -90);
    }
    private void Awake()
    {
        //m_Anim = GetComponent<Animator>();
        m_Gamemode = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMode_base>();
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        Init();
        StartCoroutine(WalkUpdate());
    }

    IEnumerator WalkUpdate()//更新Sprite，从而实现动画
    {
        //GetComponent<Animator>().
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (isWin)
            {
                nowSprite = (nowSprite + 1) % WinSprite.Length;
                GetComponent<SpriteRenderer>().sprite = WinSprite[nowSprite];
            }
            else if (Disable && !isDead)
            {
                GetComponent<SpriteRenderer>().sprite = getDemage;
            }
            else if (wined)
            {
                nowSprite = (nowSprite + 1) % WinedSprite.Length;
                GetComponent<SpriteRenderer>().sprite = WinedSprite[nowSprite];
            }
            else
            {
                nowSprite = (nowSprite + 1) % Walking.Length;
                GetComponent<SpriteRenderer>().sprite = normalMap[nowSprite];
            }

        }
    }
    public void Win()//让GameController告诉角色，他赢了，开始跳舞吧～
    {
        GetComponentInChildren<NewWave>().gameObject.GetComponent<SpriteRenderer>().enabled = false;
        isWin = true;
    }
    //已经废弃的重击和停顿。
    //public IEnumerator Stop()
    //{
    //	isStop = true;
    //	yield return new WaitForSeconds(StopSecond);
    //	isStop = false;
    //}
    //public IEnumerator Hard_Attack()
    //{
    //	GetComponentInChildren<Wave>().multiple = HardAttackMultiple;
    //	yield return new WaitForSeconds(0.15f);
    //	GetComponentInChildren<Wave>().multiple = 1;
    //}

    public IEnumerator Rush(int dir)//冲锋
    {
        if (Rushing || Disable || Time.time - LastRushTime < m_Gamemode.CD) yield break;
        Rushing = true;
        LastRushTime = Time.time;
        while (Time.time - LastRushTime < RushCurve[RushCurve.length - 1].time)
        {
            m_Rigidbody2D.velocity = new Vector3(dir * m_MaxSpeed * RushCurve.Evaluate(Time.time - LastRushTime), 0, 0);
            yield return new WaitForFixedUpdate();
        }
        Rushing = false;
    }

    private void FixedUpdate()//每帧更新落地状态，以及Rush_CD
    {
        //if (Time.time - LastRushTime < m_Gamemode.CD) GetComponent<SpriteRenderer>().color = Color.gray;
        //else GetComponent<SpriteRenderer>().color = Color.white;
        if (m_Rigidbody2D.velocity.y < 0) isJumping = false;
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        if (!isJumping)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject == gameObject) continue;
                if (colliders[i].tag == "Weapon") continue;
                if (colliders[i].isTrigger) continue;
                m_Grounded = true;
                nowJumpTimes = 0;
            }
        }
        //HPUI.GetComponentInChildren<Scrollbar>().size = (float)HP / 15;
        //m_Anim.SetBool("Ground", m_Grounded);
        //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }

    public void Move(float move, bool jump)//移动
    {
        if (Disable || Rushing || isDead || m_Gamemode.pause) return;
        if (wined) move *= -1;
        //m_Anim.SetBool("Crouch", crouch);
        if (m_Grounded || m_Gamemode.airControl)
        {
            //m_Anim.SetFloat("Speed", Mathf.Abs(move));
            if (!newMove)
            {
                if (Mathf.Abs(m_Rigidbody2D.velocity.x) > m_MaxSpeed && (m_Rigidbody2D.velocity.x * move > 0))
                    m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
                else m_Rigidbody2D.AddForce(new Vector3(move, 0, 0) * MoveForce);
            }
            else m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed / 1.5f, m_Rigidbody2D.velocity.y);
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }
        if (isFly && jump)
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, flyForce));
        }
        else if (nowJumpTimes < maxJumpTimes && jump)
        {
            isJumping = true;
            m_Grounded = false;
            if (m_Rigidbody2D.velocity.y < 0) m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
            if (nowJumpTimes == 0) m_Rigidbody2D.velocity += new Vector2(0, m_JumpForce);
            else m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpForce2);
            //m_Anim.SetBool("Ground", false);
            //m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            nowJumpTimes++;
        }

    }
    private void Flip()//根据面向，实现翻转
    {
        m_FacingRight = !m_FacingRight;
        if (isFlip) GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

    public void ChangeRotateSpeed(float Speed)
    {
        GetComponentInChildren<NewWave>().speed = Speed;
    }
    public void incRotateSpeed(int deltaSpeed)
    {
        GetComponentInChildren<NewWave>().speed += deltaSpeed;
    }
    public void Damage(int deltaHP, Transform OtherTrans, bool IgnoreDisable = false)//被打了>_<
    {
        if (m_Gamemode.pause) return;
        //Rushing = false;
        //ChangeRotateSpeed(m_Gamemode.wavespeed);
        if (HP <= 0) return;
        if (IgnoreDisable == false && Disable) return;
        if (deltaHP != 0) ChangeRotateSpeed(m_Gamemode.wavespeed);
        HP += deltaHP;
        Disable = true;
        m_Rigidbody2D.velocity = new Vector2(0, m_Rigidbody2D.velocity.y);
        if (transform.position.x > OtherTrans.position.x) m_Rigidbody2D.AddForce(new Vector2(GoAwayDist, 0));
        else m_Rigidbody2D.AddForce(new Vector2(-GoAwayDist, 0));
        if (HP > 0) StartCoroutine(CancelDisable());
        else { life--; StartCoroutine(Respawn()); }
    }
    IEnumerator CancelDisable()
    {
        yield return new WaitForSeconds(DamageTime);
        Disable = false;
    }
}
