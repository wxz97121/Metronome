using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Character2D : MonoBehaviour
{
    public float m_MaxSpeed = 800;
    public float MoveForce = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField]
    private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField]
    private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
    [SerializeField]
    private bool isFlip = true;
    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    public GameObject HPUI;
    public int HP = 15;
    private Animator m_Anim;            // Reference to the player's animator component.
    public int GoAwayDist = 5;
    private Rigidbody2D m_Rigidbody2D;
    public bool Disable = false;
    public SpriteRenderer LegRenderer;
    private int nowSprite = 0;
    public int life = 3;
    public bool wined = false;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    public Sprite[] Walking;
    public Sprite getDemage;
    public GameMode m_Gamemode;
    public Sprite[] WinedSprite;
    public Sprite[] normalMap;

    public IEnumerator Respawn()
    {
        GetComponent<AudioSource>().Play();
        transform.Rotate(0, 0, 90);
        HP = 15;
        if (GetComponentInChildren<Wave>().isHammer)
            GetComponentInChildren<Wave>().ChangeHammer();
        yield return new WaitForSeconds(3);
        transform.Rotate(0, 0, -90);

        m_MaxSpeed = m_Gamemode.maxspeed;
        MoveForce = m_Gamemode.moveforce;
        m_JumpForce = m_Gamemode.jumpforce;
        GoAwayDist = m_Gamemode.goaway;
        GetComponent<Rigidbody2D>().gravityScale = m_Gamemode.gscale;

        wined = false;
        ChangeRotateSpeed(3);
        Disable = false;
        m_Rigidbody2D.velocity = new Vector3(0, 0, 0);
        transform.position = new Vector3(Random.Range(-2000, 1300), 900, 0);
    }
    private void Awake()
    {
        m_MaxSpeed = m_Gamemode.maxspeed;
        MoveForce = m_Gamemode.moveforce;
        m_JumpForce = m_Gamemode.jumpforce;
        GoAwayDist = m_Gamemode.goaway;
        GetComponent<Rigidbody2D>().gravityScale = m_Gamemode.gscale;
        GetComponent<Rigidbody2D>().drag = m_Gamemode.lineardrag;

        Disable = false;
        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");
        m_Anim = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(WalkUpdate());
    }
    IEnumerator WalkUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (m_Gamemode.pause) yield break;
            if (Disable && HP!=15)
            {
                GetComponent<SpriteRenderer>().sprite = getDemage;
                LegRenderer.sprite = null;
                //GetComponentInChildren<Wave>().gameObject.GetComponent<SpriteRenderer>().sprite= null;
            }
            else if (wined)
            {
                nowSprite = (nowSprite + 1) % WinedSprite.Length;
                LegRenderer.sprite = null;
                GetComponent<SpriteRenderer>().sprite = WinedSprite[nowSprite];
            }
            else
            {
                nowSprite = (nowSprite + 1) % Walking.Length;
                LegRenderer.sprite = Walking[nowSprite];
                GetComponent<SpriteRenderer>().sprite = normalMap[nowSprite];
            }
        }
    }

    private void FixedUpdate()
    {
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        /*if (m_Rigidbody2D.velocity.y > 0)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<CircleCollider2D>().isTrigger = true;
        }
        else
        {
            GetComponent<BoxCollider2D>().isTrigger = false;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }*/
        //HPUI.GetComponentInChildren<Text>().text = "HP : " + HP.ToString();
        HPUI.GetComponentInChildren<Scrollbar>().size = (float)HP / 15;
        //if (!m_Gamemode.pause && (!wined || (wined && Disable) )) GetComponent<SpriteRenderer>().sprite = (Disable ? getDemage : normalMap);
        //m_Anim.SetBool("Ground", m_Grounded);
        //m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }


    public void Move(float move, bool jump)
    {
        if (Disable) return;
        if (wined) move *= -1;
        //m_Anim.SetBool("Crouch", crouch);
        // if (m_Grounded)
        {
            //m_Anim.SetFloat("Speed", Mathf.Abs(move));
            
            if (Mathf.Abs(m_Rigidbody2D.velocity.x)>m_MaxSpeed && (m_Rigidbody2D.velocity.x*move>0)) m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
            else m_Rigidbody2D.AddForce(new Vector3(move, 0, 0) * MoveForce);
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }

        //if (m_Grounded && jump && m_Anim.GetBool("Ground"))
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            //m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
        }
    }


    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        if (isFlip) GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }
    public void ChangeRotateSpeed(float Speed)
    {
        GetComponentInChildren<Wave>().speed = Speed;
    }
    public void incRotateSpeed(int deltaSpeed)
    {
        GetComponentInChildren<Wave>().speed += deltaSpeed;
    }
    public void Damage(int deltaHP, Transform OtherTrans)
    {
        if (Disable) return;
        HP += deltaHP;
        Disable = true;
        /*Vector3 myDir=(transform.position - OtherTrans.position).normalized*GoAwayDist;
        if (deltaHP < 0) m_Rigidbody2D.AddForce(new Vector2(myDir.x, myDir.y));*/
        //transform.position += myDir;
        if (transform.position.x > OtherTrans.position.x) m_Rigidbody2D.AddForce(new Vector2(GoAwayDist, 0));
        else m_Rigidbody2D.AddForce(new Vector2(-GoAwayDist, 0));
        if (HP != 0) StartCoroutine(CancelDisable());
    }
    IEnumerator CancelDisable()
    {
        yield return new WaitForSeconds(0.75f);
        Disable = false;
    }
}
