using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D m_Body;
    private int m_Direction = 1;
    [SerializeField]
    private float m_Speed = 2f;

    public bool m_Boss;

    private bool m_Small;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        /*
        if (m_Body.velocity.x <= 0.5f)
        {
            m_Direction *= -1;
        }
        */
        if (m_Body.velocity.x <= 1.5f)
        {
            m_Body.AddForce(Vector2.one * m_Speed * m_Direction);
        }
    }

    private void OnCollisionStay2D(Collision2D i_Collision)
    {
        if (m_Body.velocity.x <= 1)
        {
            m_Direction *= -1;
        }
    }

    public void DamageEnemy()
    {
        if (m_Boss && !m_Small)
        {
            m_Small = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            if (m_Boss)
            {
                LevelManager.Instance.m_Win = true;
                LevelManager.Instance.ChangeLevel("Lose");
            }
            else
            {
                Destroy(gameObject);
            }
           
        }
    }
}
