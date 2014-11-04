using UnityEngine;
using System.Collections;

public class FitToScreen : MonoBehaviour
{
    void Awake()
    {
        AdjustScale();
    }

    public void AdjustScale()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;
        float orthoSize = transform.root.GetComponent<Camera>().orthographicSize;
        transform.localScale = new Vector3(orthoSize * 2 * aspectRatio, orthoSize * 2, transform.localScale.z);
    }
}