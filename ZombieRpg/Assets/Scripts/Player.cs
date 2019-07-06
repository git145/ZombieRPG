using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MovingObject
{
    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        int directionX = (int)(Input.GetAxisRaw("Horizontal"));
        int directionY = (int)(Input.GetAxisRaw("Vertical"));

        if (directionX != 0)
        {
            directionY = 0;
        }

        if (directionX != 0 || directionY != 0)
        {
            Move((float)(directionX * 0.9), (float)(directionY * 0.9));
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
