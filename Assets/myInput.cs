using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class myInput : MonoBehaviour {
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode Jump;
    public int Type;
    // Use this for initialization
    private Character2D m_Character;
    private bool m_Jump;
    private int direction = 1;
    private void Awake()
    {
        m_Character = GetComponent<Character2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            if (Type == 3) m_Jump = Input.GetKey(Jump);
            else m_Jump = Input.GetKeyDown(Jump);
        }
    }


    private void FixedUpdate()
    {
        direction = 0;
        if (Input.GetKey(moveLeft)) direction = -1;
        if (Input.GetKey(moveRight)) direction = 1;
        // Pass all parameters to the character control script.
        m_Character.Move(direction, m_Jump);
        m_Jump = false;
    }
}
