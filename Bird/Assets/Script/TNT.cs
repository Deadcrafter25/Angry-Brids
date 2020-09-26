using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TNT : MonoBehaviour
{
    public float fieldofimpact = 1f;
    public float force = 500;
    public LayerMask LayerToHit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bird bird = collision.collider.GetComponent<Bird>();

        float collisionForce = collision.GetImpactForce();

        if (collisionForce >= 800f){
            if (bird != null)
            {
                Destroy(gameObject);
                explode();
                return;
            }

            TNT tnt = collision.collider.GetComponent<TNT>();
            if (tnt != null)
            {
                return;

            }

            if (collision.contacts[0].normal.y < -0.5)
            {
                Destroy(gameObject);
            }
        }
    }

    void explode()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldofimpact, LayerToHit);

        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fieldofimpact);
    }
}
