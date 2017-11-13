using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class myInput : MonoBehaviour {
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode Jump;
    public KeyCode Stop;
    public KeyCode RushLeft;
	public KeyCode RushRight;
    public KeyCode HardAttack;
    public int Type;
    // Use this for initialization
    private Character2D m_Character;

	[SerializeField]
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
        //if (Input.GetKey(Stop)) m_Character.StartCoroutine(m_Character.Stop());
        if (Input.GetKey(RushLeft)) m_Character.StartCoroutine(m_Character.Rush(-1));
		if (Input.GetKey(RushRight)) m_Character.StartCoroutine(m_Character.Rush(1));
		//if (Input.GetKey(HardAttack)) m_Character.StartCoroutine(m_Character.Hard_Attack());
        m_Jump = false;
    }
}
