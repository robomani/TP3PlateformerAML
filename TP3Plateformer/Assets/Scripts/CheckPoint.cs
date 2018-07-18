using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool m_IsActive;

    private void Start()
    {
        m_IsActive = true;
    }

    private void OnTriggerEnter2D(Collider2D i_Collision)
    {
        if (m_IsActive && i_Collision.gameObject.tag == "Player")
        {
            LevelManager.Instance.m_SpawnPos = transform.position;
            gameObject.GetComponent<SpriteRenderer>().material.color = Color.black;
            m_IsActive = false;
        }
    }
}
