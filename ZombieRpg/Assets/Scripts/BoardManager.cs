using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public void SetupScene(GameObject player, GameObject[] environment)
    {
        InstantiatePlayer(player);

        InstantiateEnvironment(environment);
    }

    private void InstantiatePlayer(GameObject player)
    {
        Instantiate(player);
    }

    private void InstantiateEnvironment(GameObject[] environment)
    {
        foreach(GameObject environmentItem in environment)
        {
            Instantiate(environmentItem);
        }
    }
} 