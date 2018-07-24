using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    private Rigidbody2D m_Body;
    private int m_Direction = 1;

    private void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }
     
    private void OnCollisionEnter2D(Collision2D i_Collision)
    {
        m_Direction *= -1;
    }
}
