using System.Collections;
using System.Collections.Generic;
using PostProcessing;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            Destroy(gameObject);
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<EnemyDefault>().DamageEnemy();
            }
        }
        
        transform.Translate(Vector2.right * (speed * Time.deltaTime));
    }
}
