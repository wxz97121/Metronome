using System.Collections;
using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;

public class LR_Platform : MonoBehaviour
{
    public float MoveSpeed;
    private float Left, Right;
    private Vector2 nowSpeed;
    List<Rigidbody2D> RigidOnPlatform;
    private void Awake()
    {
        Left = GameObject.Find("Left").transform.position.x;
        Right = GameObject.Find("Right").transform.position.x;
        nowSpeed = new Vector3(MoveSpeed, 0, 0);
        RigidOnPlatform = new List<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > Right)
        {
            nowSpeed = new Vector3(-MoveSpeed, 0);
            foreach (var m_Rigid in RigidOnPlatform)
            {
                //print(m_Rigid);
                m_Rigid.velocity = new Vector2(-MoveSpeed, m_Rigid.velocity.y);
            }
        }
        if (transform.position.x < Left)
        {
            nowSpeed = new Vector3(MoveSpeed, 0);
            foreach (var m_Rigid in RigidOnPlatform)
                m_Rigid.velocity = new Vector2(MoveSpeed, m_Rigid.velocity.y);
        }
    }
    private void FixedUpdate()
    {
        Vector2 nowPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 TargetPos = nowPos + nowSpeed * Time.fixedDeltaTime;
        GetComponent<Rigidbody2D>().MovePosition(TargetPos);
    }
    private void LateUpdate()
    {
        //GetComponent<Rigidbody2D>().velocity = nowSpeed;
    }
    IEnumerator DisAppear()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
        yield return new WaitForSeconds(0.35f);
        GetComponent<Collider2D>().isTrigger = true;
        yield return new WaitForSeconds(0.15f);

        yield return new WaitForSeconds(1.5f);
        GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.15f);
        GetComponent<Collider2D>().isTrigger = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.tag == "Player")
        //collision.gameObject.transform.SetParent(transform, true);
        RigidOnPlatform.Add(collision.rigidbody);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        RigidOnPlatform.Remove(collision.rigidbody);
        //if (collision.gameObject.tag == "Player")
        //    collision.gameObject.transform.parent = null;
    }
}
