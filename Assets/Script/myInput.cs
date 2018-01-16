using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class myInput : MonoBehaviour
{
    public enum InputType
    {
        Keyboard_WAD,
        Keyboard_Arrow,
        Joystick1,
        Joystick2,
        Debug_1,
        Debug_2
    }
    public InputType m_Input;

    private KeyCode moveLeft=KeyCode.Joystick8Button9;
    private KeyCode moveRight= KeyCode.Joystick8Button9;
    private string JoyStickMove= "NullHorizon";
    private KeyCode Jump= KeyCode.Joystick8Button9;
    private KeyCode JoyStickJump= KeyCode.Joystick8Button9;



    private Character2D m_Character;
    private bool m_Jump;
    private float direction = 1;
    void ProcessKey()
    {
        if (m_Input == InputType.Keyboard_WAD || m_Input == InputType.Debug_1)
        {
            moveRight = KeyCode.D;
            moveLeft = KeyCode.A;
            Jump = KeyCode.W;
        }
        if (m_Input == InputType.Keyboard_Arrow || m_Input == InputType.Debug_2)
        {
            moveRight = KeyCode.RightArrow;
            moveLeft = KeyCode.LeftArrow;
            Jump = KeyCode.UpArrow;
        }
        if (m_Input == InputType.Joystick1 || m_Input == InputType.Debug_1)
        {
            JoyStickMove = "Joy1Horizon";
            JoyStickJump = KeyCode.Joystick1Button0;
        }
        if (m_Input == InputType.Joystick2 || m_Input == InputType.Debug_2)
        {
            JoyStickMove = "Joy2Horizon";
            JoyStickJump = KeyCode.Joystick2Button0;
        }
    }
    private void Awake()
    {
        //对于Player1 它在PlayerPrefs里有一个Key叫 Player1_Input。
        //这个值可能是 1 2 3 4，分别对应 WAD、方向键、手柄一、手柄二
        if (PlayerPrefs.HasKey(gameObject.name + "_Input"))
        {
            int v = PlayerPrefs.GetInt(gameObject.name + "_Input");
            if (v == 0) m_Input = InputType.Keyboard_WAD;
            if (v == 1) m_Input = InputType.Keyboard_Arrow;
            if (v == 2) m_Input = InputType.Joystick1;
            if (v == 3) m_Input = InputType.Joystick2;
        }
        ProcessKey();
        m_Character = GetComponent<Character2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            if (m_Character.isFly) m_Jump = Input.GetKey(Jump);
            else m_Jump = Input.GetKeyDown(Jump) | Input.GetKeyDown(JoyStickJump);
        }
    }


    private void FixedUpdate()
    {
        direction = 0;
        if (JoyStickMove.Length != 0) direction = Input.GetAxis(JoyStickMove);
        if (Input.GetKey(moveLeft)) direction = -1;
        if (Input.GetKey(moveRight)) direction = 1;
        m_Character.Move(direction, m_Jump);
        m_Jump = false;
    }
}
