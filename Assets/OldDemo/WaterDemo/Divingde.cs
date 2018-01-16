using UnityEngine;
using System.Collections;

public class Divingde : MonoBehaviour
{

    public float Duration = 5;
    private bool used = false;
    // Use this for initialization
    void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;
        if (other.gameObject.tag == "Player")
        {
            used = true;
            other.GetComponent<SpriteRenderer>().color = Color.blue;
            other.GetComponent<GoingDown>().isActive = true;
            Destroy(GetComponent<SpriteRenderer>());
            StartCoroutine(reset(other.GetComponent<Character2D>()));
        }
    }
    // Update is called once per frame
    public IEnumerator reset(Character2D m_Player)
    {
        yield return new WaitForSeconds(Duration);
        m_Player.GetComponent<SpriteRenderer>().color = Color.white;
        m_Player.GetComponent<GoingDown>().isActive = false;
        Destroy(gameObject);
    }

}
