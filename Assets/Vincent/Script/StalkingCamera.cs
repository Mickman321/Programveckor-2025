using UnityEngine;

public class StalkingCamera : MonoBehaviour
{
    public Transform target; // The object the camera follows
    public Vector3 offset; // Offset from the target
    public float sensitivity = 100f; // Mouse sensitivity
    public float zoomSpeed = 4f; // Speed of zooming
    public float minZoom = 5f; // Minimum zoom distance
    public float maxZoom = 20f; // Maximum zoom distance
    public float pitch = 2f; // Up/down angle of the camera

    private float currentZoom = 10f; // Current zoom level
    private float yaw = 0f; // Horizontal rotation

    void Update()
    {
        // Mouse scroll for zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentZoom -= scroll * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Mouse movement for rotation
        if (Input.GetMouseButton(1)) // Right mouse button to rotate
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        // Adjust camera position
        transform.position = target.position - offset * currentZoom;

        // Adjust camera rotation
        transform.LookAt(target.position + Vector3.up * pitch);
        transform.RotateAround(target.position, Vector3.up, yaw);
    }
}
