using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectile;
    public float speed = 1f;
    public float editorRotationSpeed = 120f;

    private TextMesh log;

    private bool doneMoving = false;
    private bool doneShooting = false;

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
            doneMoving = false;
            doneShooting = false;

            ProcessInput(Input.touches[0]);
            if (Input.touchCount > 1)
                ProcessInput(Input.touches[1]);
        }
    }

    void ProcessInput(Touch touch)
    {
        if (touch.position.y > Screen.height / 2)
        {
            TouchPhase phase = touch.phase;
            if (phase == TouchPhase.Began || phase == TouchPhase.Moved || phase == TouchPhase.Stationary)
            {
                if(!doneMoving)
                {
                    rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
                    doneMoving = true;
                }
            }
        }
        else // Shoot
        {
            TouchPhase phase = touch.phase;
            if (phase == TouchPhase.Began)
            {
                if(!doneShooting)
                {
                    ShootProjectile();
                    doneShooting = true;
                }
            }
        }
    }

    void ShootProjectile()
    {
        GameObject instance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity) as GameObject;
        instance.GetComponent<Projectile>().StartMovement(transform.forward);
        projectileSpawnPoint.audio.Play();
    }
}
