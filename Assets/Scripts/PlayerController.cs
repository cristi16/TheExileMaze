using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform projectileSpawnPoint;
    public GameObject projectile;
    public float speed = 1f;
    public float editorRotationSpeed = 120f;
    public float footstepsVolume = 1f;
    [HideInInspector]
    public bool ignoreInput = false;
    private TextMesh log;

    private bool doneMoving = false;
    private bool doneShooting = false;

    void Start()
    {
        //log = GameObject.FindGameObjectWithTag("Log").GetComponent<TextMesh>();
        Input.gyro.enabled = true;
    }
    
    void Update()
    {
        if (ignoreInput) return;

    #if UNITY_EDITOR
            EditorUpdate();
    #else
            TouchUpdate();
    #endif

        //log.text = Input.gyro.userAcceleration.ToString();
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
            if (audio.isPlaying == false)
                audio.Play();
        }
        else
        {
            if (audio.isPlaying)
                audio.Stop();
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
                    audio.volume = footstepsVolume;
                    StopCoroutine(FadeOut(0.3f));
                    if (audio.isPlaying == false)   
                        audio.Play();
                    doneMoving = true;
                }
            }
            else
            {
                StartCoroutine(FadeOut(0.3f));
                if (audio.isPlaying)
                    audio.Stop();
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

    IEnumerator FadeOut(float time)
    {
        float timer = 0f;
        while(timer < time)
        {
            timer += Time.deltaTime;
            audio.volume = Mathf.Lerp(audio.volume, 0f, timer / time);
            yield return null;
        }
    }

    void ShootProjectile()
    {
        GameObject instance = Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity) as GameObject;
        instance.GetComponent<Projectile>().StartMovement(transform.forward);
        projectileSpawnPoint.audio.Play();
    }
}
