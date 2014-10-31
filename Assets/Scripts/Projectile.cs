using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 3f;
    public float maxLifeTime = 10f;
    Vector3 direction;
    bool moving = false;
    float lifetime = 0f;

    IEnumerator Move()
    {
        while(moving)
        {
            rigidbody.MovePosition(transform.position + direction * Time.deltaTime * speed);
            lifetime += Time.deltaTime;
            if (lifetime > maxLifeTime)
                GameObject.Destroy(gameObject);
            yield return null;
        }
    }

    public void StartMovement(Vector3 forward)
    {
        direction = forward;
        moving = true;
        StartCoroutine(Move());
    }

    void OnCollisionEnter(Collision col)
    {
        var hit = col.gameObject.GetComponent<EnvironmentObject>();
        if(hit != null)
        {
            hit.PlayProjectileHitSound(col.contacts[0].point);
        }
        moving = false;
        GameObject.Destroy(gameObject);
    }
}
