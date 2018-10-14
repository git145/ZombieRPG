using UnityEngine;

public class GameController : MonoBehaviour {
    // Configuration variables
    public static float tileSize = 0.48f;

    void Start () {
        // Add objects to scene
        Instantiate(Resources.Load("Maps/Camp_001"));

        GameObject player = (GameObject)Instantiate(Resources.Load("Heroes/Hero_001"));

        GameObject camera = (GameObject)Instantiate(Resources.Load("Cameras/Camera_001"));
        CameraController cameraController = camera.GetComponent(typeof(CameraController)) as CameraController;
        cameraController.SetTarget(player, new Vector2(0f, 0f));
    }
}
