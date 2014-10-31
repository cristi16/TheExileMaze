using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public float speed = 3f;
    Vector3 direction;

    IEnumerator Move()
    {
        while(true)
        {
            rigidbody.MovePosition(transform.position + direction * Time.deltaTime * speed);
            yield return null;
        }
    }

    public void StartMovement(Vector3 forward)
    {
        direction = forward;
        StartCoroutine(Move());
    }

    void OnCollisionEnter(Collision col)
    {
        var hit = col.gameObject.GetComponent<EnvironmentObject>();
        if(hit != null)
        {
            hit.PlayProjectileHitSound(col.contacts[0].point);
        }
    }
}
