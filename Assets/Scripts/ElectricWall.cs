using UnityEngine;
using System.Collections;

public class ElectricWall : MonoBehaviour
{

    public Transform bounds1;
    public Transform bounds2;

    public Vector3 wallOrientation;
    private GameObject player;
    private Vector3 minBound;
    private Vector3 maxBound;
    private float minValue;
    private float maxValue;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wallOrientation = (bounds1.position - bounds2.position).normalized;
        wallOrientation = new Vector3(Mathf.Round(wallOrientation.x), Mathf.Round(wallOrientation.y), Mathf.Round(wallOrientation.z));
        wallOrientation = new Vector3(Mathf.Abs(wallOrientation.x), Mathf.Abs(wallOrientation.y), Mathf.Abs(wallOrientation.z));
        SetupBounds();
    }

    void Update()
    {
        float playerPosition = GetPlayerPositionComponent();
        playerPosition = Mathf.Clamp(playerPosition, minValue, maxValue);
        float offset = playerPosition - minValue;
        transform.position = Vector3.Lerp(minBound, maxBound, offset / (maxValue - minValue));
    }

    void SetupBounds()
    {
        if (wallOrientation.x == 1f)
        {
            minBound = (bounds1.position.x < bounds2.position.x) ? bounds1.position : bounds2.position;
            maxBound = (bounds1.position.x > bounds2.position.x) ? bounds1.position : bounds2.position;
            minValue = minBound.x;
            maxValue = maxBound.x;
        }
        else if (wallOrientation.y == 1f)
        {
            minBound = (bounds1.position.y < bounds2.position.y) ? bounds1.position : bounds2.position;
            maxBound = (bounds1.position.y > bounds2.position.y) ? bounds1.position : bounds2.position;
            minValue = minBound.y;
            maxValue = maxBound.y;
        }
        else if (wallOrientation.z == 1f)
        {
            minBound = (bounds1.position.z < bounds2.position.z) ? bounds1.position : bounds2.position;
            maxBound = (bounds1.position.z > bounds2.position.z) ? bounds1.position : bounds2.position;
            minValue = minBound.z;
            maxValue = maxBound.z;
        }
    }

    float GetPlayerPositionComponent()
    {
        if (wallOrientation.x == 1f)
            return player.transform.position.x;
        else if (wallOrientation.y == 1f)
            return player.transform.position.y;
        else if (wallOrientation.z == 1f)
            return player.transform.position.z;
        else
            return 0f;
    }
}
