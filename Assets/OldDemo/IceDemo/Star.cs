using UnityEngine;
using System.Collections;
using DG.Tweening;
public class Star : MonoBehaviour
{
    public float Duration = 5;
    private bool used = false;
    public float scaleSize = 2;
    public float scaleSpeed = 2;
    public float scaleMoveforce = 2;
    public float scalemeltSpeed = 2;
    public float scaleJumpforce = 2;
    public float scaleJumpforce2 = 2;
    private GameMode_base m_Gamemode;
    // Use this for initialization
    private void Awake()
    {
        m_Gamemode = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMode_base>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;
        if (other.gameObject.tag == "Player")
        {
            used = true;
            Character2D m_Player = other.GetComponent<Character2D>();
            m_Player.GetComponent<SpriteRenderer>().color = Color.red;
            if (m_Player!=null)
            {
                m_Player.transform.localScale *= scaleSize;
                m_Player.m_MaxSpeed *= scaleSpeed;
                m_Player.meltSpeed *= scalemeltSpeed;
                m_Player.MoveForce *= scaleMoveforce;
                m_Player.m_JumpForce *= scaleJumpforce;
                m_Player.m_JumpForce2 *= scaleJumpforce2; 
                Destroy(GetComponent<SpriteRenderer>());
                StartCoroutine(reset(other.GetComponent<Character2D>()));
            }

        }
    }
    // Update is called once per frame
    public IEnumerator reset(Character2D m_Player)
    {
        yield return new WaitForSeconds(Duration);
        m_Player.GetComponent<SpriteRenderer>().color = Color.white;
        m_Player.transform.localScale /= scaleSize;
        m_Player.m_MaxSpeed = m_Gamemode.maxspeed;
        m_Player.meltSpeed = m_Gamemode.meltSpeed;
        m_Player.MoveForce = m_Gamemode.moveforce;
        m_Player.m_JumpForce = m_Gamemode.jumpforce;
        m_Player.m_JumpForce2 = m_Gamemode.jumpforce2;
        Destroy(gameObject);
    }

}
