using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 jumpForce;
    public Vector2 jumpForceUp;
    public float minForceX;
    public float maxForceX;
    public float minForceY;
    public float maxForceY;

    [HideInInspector]
    public int lastPlatformId;

    bool m_didJump;
    bool m_powerSetted;

    Rigidbody2D m_rb;
    Animator m_anim;
    float m_curPowerBarVal = 0;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>(); 
        m_anim = GetComponent<Animator>();
    }

    private void Update()
    {
        SetPower();

        if(Input.GetMouseButtonDown(0))
        {
            SetPower(true);
        }

        if(Input.GetMouseButtonUp(0))
        {
            SetPower(false);
        }
    }

    void SetPower()
    {
        if(m_powerSetted && !m_didJump)
        {
            jumpForce.x += jumpForceUp.x * Time.deltaTime;
            jumpForce.y += jumpForceUp.y * Time.deltaTime;

            jumpForce.x = Mathf.Clamp(jumpForce.x, minForceX, maxForceX);
            jumpForce.y = Mathf.Clamp(jumpForce.y, minForceY, maxForceY);

            m_curPowerBarVal += GameManager.Ins.powerBarUp * Time.deltaTime;

            GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);
        }
    }

    public void SetPower(bool isHoldingMouse)
    {
        m_powerSetted = isHoldingMouse; 

        if(!m_powerSetted && !m_didJump)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!m_rb || jumpForce.x <= 0 || jumpForce.y <= 0) return;

        m_rb.velocity = jumpForce;

        m_didJump = true;

        if (m_anim) m_anim.SetBool("did_jump", true);

        AudioController.Ins.PlaySound(AudioController.Ins.jump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(TagConsts.GROUND))
        {
            Platform p = collision.transform.root.GetComponent<Platform>(); 

            if(m_didJump)
            {
                m_didJump = false;

                if (m_anim) m_anim.SetBool("did_jump", false);

                if(m_rb) m_rb.velocity = Vector2.zero;

                jumpForce = Vector2.zero;

                m_curPowerBarVal = 0;

                GameGUIManager.Ins.UpdatePowerBar(m_curPowerBarVal, 1);
            }

            if(p && p.id != lastPlatformId)
            {
                GameManager.Ins.CreatePlatformAndLerp(transform.position.x);
                lastPlatformId = p.id;

                GameManager.Ins.AddScore();
            }
        }

        if(collision.CompareTag(TagConsts.DEAD_ZONE))
        {
            GameGUIManager.Ins.ShowGameoverDialog();
            
            Destroy(gameObject);

            AudioController.Ins.PlaySound(AudioController.Ins.gameover);
        }
    }
}
