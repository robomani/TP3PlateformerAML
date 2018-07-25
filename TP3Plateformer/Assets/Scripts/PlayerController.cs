using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{

    
    public float m_Speed = 10f;
    public float m_JumpForce = 50f;
    public Action m_OnLifeLost;

    private bool m_CanJump;
    [SerializeField]
    private bool m_IsRanma;
    private bool m_FemaleRanma;
    private bool m_Big = true;

    private Rigidbody2D m_RB;
    private Vector2 m_MoveDir = new Vector2();
    private Animator m_Animator;
    private SpriteRenderer m_Renderer;

    private float m_Invincibility = 0f;

    private void Awake()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_Renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            //m_MoveDir = transform.right;
            m_RB.AddForce(transform.right * m_Speed);

            m_Animator.SetTrigger("Right");             
            

            m_Renderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            //m_MoveDir = -transform.right;
            m_RB.AddForce(-transform.right * m_Speed);
            m_Animator.SetTrigger("Left");
            
            m_Renderer.flipX = true;
        }
        else
        {
            m_MoveDir = Vector2.zero;
            m_Animator.SetTrigger("Idle");
        }

        if (Input.GetKeyDown(KeyCode.W) && m_CanJump)
        {
            m_RB.AddForce(transform.up * m_JumpForce);
            m_CanJump = false;
        }
        m_Invincibility -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        //m_MoveDir *= m_Speed;
        m_MoveDir.y = m_RB.velocity.y;
        //m_RB.velocity = m_MoveDir;
    }

    private void OnCollisionEnter2D(Collision2D i_Collision)
    {
        Debug.Log(i_Collision.gameObject.tag);
        if (i_Collision.gameObject.tag == "Ground")
        {
            m_CanJump = true;
        }

        
    }

    private void OnCollisionEnter(Collision i_Collision)
    {
        if (m_Invincibility <= 0 && (i_Collision.gameObject.tag == "Enemy" || i_Collision.gameObject.tag == "Boss"))
        {
            if (m_Big)
            {
                Debug.Log("Small");
                m_Big = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
                m_JumpForce *= 2f;
                m_Invincibility = 5f;
            }
            else
            {
                m_OnLifeLost();
                Die();
            }
        }
         
    }

    private void OnTriggerEnter2D(Collider2D i_Collision)
    {
        if (m_Invincibility <= 0 && i_Collision.gameObject.tag == "Fire")
        {
            Debug.Log("Damage");
            if (m_Big)
            {
                Debug.Log("Small");
                m_Big = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
                m_JumpForce *= 2f;
                m_Invincibility = 5f;
            }
            else
            {
                m_OnLifeLost();
                Die();
            }
        }

        if (i_Collision.gameObject.tag == "Death")
        {
            m_OnLifeLost();
            Die();    
        }

        if (m_IsRanma)
        {
            if (m_FemaleRanma && i_Collision.gameObject.tag == "HotWater")
            {
                m_Animator.SetTrigger("Male");
                m_FemaleRanma = false;
            }
            else if (!m_FemaleRanma && i_Collision.gameObject.tag == "ColdWater")
            {
                m_Animator.SetTrigger("Female");
                m_FemaleRanma = true;
            }
        }
    }

    private void Die()
    {
        if (m_IsRanma)
        {
            //m_FemaleRanma = false;
        }
        m_Big = true;
        transform.localScale = new Vector3(2f, 2f, 2f);
        LevelManager.Instance.m_Lives--;
        LevelManager.Instance.ChangeLevel("Lose", 0.1f);
    }

    
}
