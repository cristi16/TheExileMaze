using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectile;
    public float speed = 1f;
    private TextMesh log;
    private float editorRotationSpeed = 60f;

    void Start()
    {
        log = GameObject.FindGameObjectWithTag("Log").GetComponent<TextMesh>();
        Input.gyro.enabled = true;
    }
    
    void Update()
    {
    #if UNITY_EDITOR
            EditorUpdate();
    #else
            TouchUpdate();
    #endif
    }

    void EditorUpdate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -editorRotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, editorRotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }

    void TouchUpdate()
    {
        Quaternion currentRotation = Input.gyro.attitude;
        transform.rotation = Quaternion.Euler(0f, 360f - (currentRotation.eulerAngles.z - 90f), 0f);
        log.text = transform.rotation.eulerAngles.ToString();

        if(Input.touchCount > 0)
        {
            if(Input.touches[0].position.y > Screen.height / 2)
            {
                TouchPhase phase = Input.touches[0].phase;
                if(phase == TouchPhase.Began || phase == TouchPhase.Moved || phase == TouchPhase.Stationary)
                    rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
            }
            else // Shoot
            {
                TouchPhase phase = Input.touches[0].phase;
                if (phase == TouchPhase.Began)
                {
                    ShootProjectile();
                }
            }
        }
    }

    void ShootProjectile()
    {
        GameObject instance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity) as GameObject;
        instance.GetComponent<Projectile>().StartMovement(transform.forward);
    }
}
