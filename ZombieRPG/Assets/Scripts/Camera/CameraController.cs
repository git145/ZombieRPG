using UnityEngine;
using UnityEditor;

public class CameraController : MonoBehaviour {
    // The transform the camera will be following
    [SerializeField]
    private GameObject target;

    // The speed with which the camera will be following the target
    [SerializeField]
    private float smoothing = 10;

    // The offset from the target
    [SerializeField]
    private Vector2 offset2D = new Vector2(0f, 0f);

    private float offsetZ = GameController.tileSize * -1;

    // Use this for initialization
    private void Start()
    {
        if (target != null)
        {
            SetTarget(target, offset2D);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        // If a target has been assigned
        if (target != null)
        {
            MoveCamera();
        }
    }

    // Assign a target to the camera
    public void SetTarget(GameObject gameObject, Vector2 offset)
    {
        // Set the target and the offset of the camera from the target
        target = gameObject;
        offset2D = offset;
    }

    private void MoveCamera()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 cameraPositionNew = new Vector3(target.transform.position.x + offset2D.x, target.transform.position.y + offset2D.y, offsetZ);

        // Interpolate towards the new position for the camera
        if (smoothing > 0f)
        {
            // Smoothly interpolate between the current position and target position of the camera
            transform.position = Vector3.Lerp(transform.position, cameraPositionNew, Time.deltaTime * smoothing);
        }
        else
        {
            // Move the camera to the next position
            transform.position = cameraPositionNew;
        }
    }
}
