using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    enum EState
    {
        Peace,Attack,Angry
    }
    private EState m_State;
    public float MoveSpeed=50;
    public float JumpForce = 5000;
    public float MaxDist = 400;
    public float CheckDist = 200;
    private Vector3 InitPos;
    private int Dir;
    private bool m_Grounded;
    public Transform Ground_check;
    private void Awake()
    {
        InitPos = transform.position;
        Dir = 1;
    }
    public void HasBeenAttack()
    {
        //Debug.Log(collision.gameObject.tag);
        //if (collision.gameObject.tag == "Weapon") 
        Destroy(gameObject);
    }
    // Use this for initialization
    void Start () {
		
	}
	// Update is called once per frame
	void Update () {
        if (!m_Grounded) return;
        if (m_State == EState.Peace)
        {
            if (transform.position.x < InitPos.x - MaxDist) Dir = 1;
            if (transform.position.x > InitPos.x + MaxDist) Dir = -1;
            GetComponent<Rigidbody2D>().velocity = new Vector3(MoveSpeed * Dir, GetComponent<Rigidbody2D>().velocity.y, 0);
        }
        else
        {
            if (transform.position.x < InitPos.x + MaxDist && transform.position.x > InitPos.x - MaxDist)
                GetComponent<Rigidbody2D>().velocity = new Vector3(MoveSpeed * Dir, GetComponent<Rigidbody2D>().velocity.y, 0);
            if (GetComponent<Rigidbody2D>().velocity.y<=1)
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
        }
    }
    private void FixedUpdate()
    {
        m_Grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(Ground_check.position,4f);
        for (int i = 0; i < colliders.Length; i++)
        {
            //Debug.Log(colliders[i].gameObject);
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }
        m_State = EState.Peace;
        colliders = Physics2D.OverlapCircleAll(transform.position, CheckDist);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Player")
            {
                m_State = EState.Angry;
                Dir = -1*(int) Mathf.Sign(transform.position.x - colliders[i].transform.position.x);
            }
           
        }
    }
}
