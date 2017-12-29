using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RestLife : MonoBehaviour
{
    public Sprite[] LifeSprite;
    private Character2D m_Character;
    public string Target;
    private Image ImageUI;
    private Scrollbar ScrollbarUI;
    private GameMode_base m_Gamemode;
    private void Awake()
    {
        m_Character = GameObject.Find(Target).GetComponent<Character2D>();
        ImageUI = transform.Find("Image").GetComponent<Image>();
        ScrollbarUI = transform.Find("Scrollbar").GetComponent<Scrollbar>();
        m_Gamemode = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameMode_base>();
    }
    void Update()
    {
        if (m_Character.life<LifeSprite.Length && m_Character.life>=0)
            ImageUI.sprite = LifeSprite[m_Character.life];
        ScrollbarUI.size = (float)m_Character.HP / m_Gamemode.HPofLife;
        if (m_Character.isDead) ScrollbarUI.size = 1;

    }
}
