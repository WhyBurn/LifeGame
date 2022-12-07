using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameControllerObject.GetGCO().SetCameraController(this);
    }

    public void FocusCamera(Vector2Int bounds)
    {
        gameObject.transform.position = new Vector3(bounds.x * Data.displayScale / 2, bounds.y * Data.displayScale / 2, transform.position.z);
        GetComponent<Camera>().orthographicSize = Mathf.Max(bounds.x, bounds.y) / 4f;

    }
}
