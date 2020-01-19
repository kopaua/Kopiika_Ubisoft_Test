using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 offset;
    private Transform target;
    private float zoomSpeed =50;
    private const float minZoom = -200, maxZoom = -20;
    // Start is called before the first frame update
    void Awake()
    {
        offset = transform.position;
    }

    void Update()
    {
        float zoom = offset.z;
        zoom +=Input.mouseScrollDelta.y * Time.deltaTime * zoomSpeed;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        offset = new Vector3(offset.x, offset.y, zoom);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
            transform.position = target.position + offset;
    }   

    public void SetPlayer(Transform playerTransform)
    {
        target = playerTransform;
    }
}
