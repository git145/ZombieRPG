using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager = null;

    private BoardManager boardManager;

    public GameObject[] environment;

    public GameObject player;

    public void Awake()
    {
        EnforceSingleton();

        DontDestroyOnLoad(gameObject);

        boardManager = GetComponent<BoardManager>();

        InitGame();
    }

    private void EnforceSingleton()
    {
        if (gameManager == null)
        {
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
        }
    }

    public void InitGame()
    {
        boardManager.SetupScene(player, environment);
    }
}
