using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private BoardManager boardManager;

    public GameObject[] environment;

    public GameObject player;

    public bool playerCanMove = true;

    public void Awake()
    {
        EnforceSingleton();

        DontDestroyOnLoad(gameObject);

        boardManager = GetComponent<BoardManager>();

        InitGame();
    }

    private void EnforceSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void InitGame()
    {
        boardManager.SetupScene(player, environment);
    }
}
