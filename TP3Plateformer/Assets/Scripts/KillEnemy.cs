using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D i_Collision)
    {
        if (i_Collision.GetComponent<EnemyController>() && i_Collision.tag != "Fire")
        {
            gameObject.GetComponentInParent<Rigidbody2D>().AddForce(Vector2.up * 4f);
            i_Collision.GetComponent<EnemyController>().DamageEnemy();
        }
    }
}
