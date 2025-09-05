// using UnityEngine;

// public class Bullet : MonoBehaviour
// {
//     private Rigidbody rb;
//     private Transform player;

//     [System.Obsolete]
//     public void Init(Vector3 velocity, Transform playerTarget)
// {
//     rb = GetComponent<Rigidbody>();
//     rb.useGravity = false;
//     rb.velocity = velocity;
//     player = playerTarget;
// }

//     void OnCollisionEnter(Collision collision)
//     {
//         if (collision.gameObject.CompareTag("Player"))
//         {
//             PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
//             if (playerHealth != null)
//             {
//                 playerHealth.TakeDamage(1);
//             }
//         }
//         Destroy(gameObject);
//     }
// }
using UnityEngine;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    public GameObject bulletHolePrefab;

    private Rigidbody rb;
    private Transform player;
    private static Queue<GameObject> bulletHoles = new Queue<GameObject>();
    private static int maxBulletHoles = 10;

    [System.Obsolete]
    public void Init(Vector3 velocity, Transform playerTarget)
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.velocity = velocity;
        player = playerTarget;
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 hitPoint = contact.point;
        Vector3 hitNormal = contact.normal;

        if (bulletHolePrefab != null)
        {
            Quaternion rot = Quaternion.LookRotation(-hitNormal);
            rot *= Quaternion.Euler(0, 0, Random.Range(0, 360));

            GameObject hole = Instantiate(bulletHolePrefab, hitPoint + hitNormal * 0.001f, rot);
            hole.transform.SetParent(collision.transform);
            bulletHoles.Enqueue(hole);
            if (bulletHoles.Count > maxBulletHoles)
            {
                GameObject oldest = bulletHoles.Dequeue();
                if (oldest != null)
                    Destroy(oldest);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
            }
        }
        Destroy(gameObject);
    }
}