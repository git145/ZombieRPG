using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;

    void Awake()
    {
        if (GameManager.gameManager == null)
        {
            Instantiate(gameManager);
        }
    }
}