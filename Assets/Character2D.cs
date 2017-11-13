using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Character2D : MonoBehaviour
{
	public float m_MaxSpeed = 800;
	public float MoveForce = 10f;
	public float m_RushSpeed;

	// The fastest the player can travel in the x axis.
	private float m_JumpForce = 400f;
	private float m_JumpForce2 = 400f;
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
	public SpriteRenderer CopterRenderer;
	private int nowSprite = 0;
	public int life = 3;
	public bool wined = false;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	public Sprite[] Walking;
	public Sprite getDemage;
	public GameMode m_Gamemode;
	public Sprite[] WinedSprite;
	public Sprite[] normalMap;
	public bool isFly;
	public float flyForce;
	public bool isStop = false;
	public float StopSecond = 0.5f;
	public float HardAttackMultiple = 3;
	public bool newWave = false;
	public bool newMove;
	private bool Rushing = false;
	private int nowJumpTimes = 0;
	private int maxJumpTimes;
	public AnimationCurve RushCurve;



	public IEnumerator Respawn()
	{
		Rushing = false;
		GetComponent<AudioSource>().Play();
		transform.Rotate(0, 0, 90);
		HP = 15;
		if (GetComponentInChildren<Wave>().isHammer)
			GetComponentInChildren<Wave>().ChangeHammer();
		yield return new WaitForSeconds(3);
		transform.Rotate(0, 0, -90);
		flyForce = m_Gamemode.flyForce;
		m_MaxSpeed = m_Gamemode.maxspeed;
		MoveForce = m_Gamemode.moveforce;
		m_JumpForce = m_Gamemode.jumpforce;
		m_JumpForce2 = m_Gamemode.jumpforce2;
		GoAwayDist = m_Gamemode.goaway;
		maxJumpTimes = m_Gamemode.maxJumpTimes;
		GetComponent<Rigidbody2D>().gravityScale = m_Gamemode.gscale;
		wined = false;
		ChangeRotateSpeed(m_Gamemode.wavespeed);
		Disable = false;
		isFly = false;
		isStop = false;
		if (CopterRenderer != null) CopterRenderer.gameObject.SetActive(false);
		m_Rigidbody2D.velocity = new Vector3(0, 0, 0);
		transform.position = new Vector3(Random.Range(m_Gamemode.RespawnLeft, m_Gamemode.RespawnRight), m_Gamemode.Respawnheight, 0);
	}
	private void Awake()
	{
		flyForce = m_Gamemode.flyForce;
		m_MaxSpeed = m_Gamemode.maxspeed;
		MoveForce = m_Gamemode.moveforce;
		m_JumpForce = m_Gamemode.jumpforce;
		m_JumpForce2 = m_Gamemode.jumpforce2;
		GoAwayDist = m_Gamemode.goaway;
		maxJumpTimes = m_Gamemode.maxJumpTimes;
		RushCurve = m_Gamemode.RushCurve;
		GetComponent<Rigidbody2D>().gravityScale = m_Gamemode.gscale;
		GetComponent<Rigidbody2D>().drag = m_Gamemode.lineardrag;
		ChangeRotateSpeed(m_Gamemode.wavespeed);
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
			if (Disable && HP != 15)
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
	public IEnumerator Stop()
	{
		isStop = true;
		yield return new WaitForSeconds(StopSecond);
		isStop = false;
	}
	public IEnumerator Hard_Attack()
	{
		GetComponentInChildren<Wave>().multiple = HardAttackMultiple;
		yield return new WaitForSeconds(0.15f);
		GetComponentInChildren<Wave>().multiple = 1;
	}
	private float LastRushTime=-100;
	public IEnumerator Rush(int dir)
	{
		if (Rushing || Disable || Time.time-LastRushTime<m_Gamemode.CD) yield break;
		Rushing = true;
		LastRushTime = Time.time;
		while (Time.time - LastRushTime < RushCurve[RushCurve.length - 1].time)
		{
			m_Rigidbody2D.velocity = new Vector3(dir * m_RushSpeed * RushCurve.Evaluate(Time.time - LastRushTime), 0, 0);
			yield return new WaitForFixedUpdate();
		}
		Rushing = false;
	}

	private void FixedUpdate()
	{
		if (Time.time - LastRushTime < m_Gamemode.CD) GetComponent<SpriteRenderer>().color = Color.gray;
		else GetComponent<SpriteRenderer>().color = Color.white;
		m_Grounded = false;
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		if (m_Rigidbody2D.velocity.y < 5f)
		{
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
				{
					m_Grounded = true;
					nowJumpTimes = 0;
					//print("haha");
				}
			}
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
		if (Disable || Rushing) return;
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
			// Add a vertical force to the player.
			m_Grounded = false;
			if (nowJumpTimes == 0) m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, m_JumpForce, 0);
			else m_Rigidbody2D.velocity = new Vector3(m_Rigidbody2D.velocity.x, m_JumpForce2, 0);
			//m_Anim.SetBool("Ground", false);
			//m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			nowJumpTimes++;
			//print(nowJumpTimes);
			//print(maxJumpTimes);
		}

	}


	private void Flip()
	{
		m_FacingRight = !m_FacingRight;
		if (isFlip) LegRenderer.flipX = !LegRenderer.flipX;
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
		//Rushing = false;
		if (Disable || Rushing) return;
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
		yield return new WaitForSeconds(m_Gamemode.DamageTime);
		Disable = false;
	}
}
