using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField]
    private float m_TurnSpeed = 10f;
    [SerializeField]
    private float m_LunchForce = 250f;

    private bool m_PlayerLocked;
    private PointEffector2D m_Effector;
    private Rigidbody2D m_LockedBody;

    private void Awake()
    {
        m_Effector = GetComponent<PointEffector2D>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * (m_TurnSpeed * Time.deltaTime)); 

        if (m_PlayerLocked && m_LockedBody != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                
                m_Effector.enabled = false;
                m_LockedBody.constraints = RigidbodyConstraints2D.None;
                m_LockedBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                m_LockedBody.AddForce(transform.up * m_LunchForce);
                StartCoroutine(RestartEffector());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D i_Collision)
    {
        if (i_Collision.tag == "Player" && !m_PlayerLocked)
        {
            m_LockedBody = i_Collision.GetComponent<Rigidbody2D>();
            float PosX = i_Collision.transform.position.x;
            float PosY = i_Collision.transform.position.y;
            if ((PosX >= transform.position.x - 1.5 || PosX <= transform.position.x + 1.5) && (PosY >= transform.position.y - 1.5 || PosY <= transform.position.y + 1.5))
            {
                m_LockedBody.velocity = Vector3.zero;
                m_LockedBody.constraints = RigidbodyConstraints2D.FreezeAll;
                i_Collision.transform.position = transform.position;
                m_PlayerLocked = true;
            }
        }
    }

    private IEnumerator RestartEffector()
    {
        yield return new WaitForSeconds(2);
        m_PlayerLocked = false;
        m_Effector.enabled = true;
        m_LockedBody = null;
    }
}
