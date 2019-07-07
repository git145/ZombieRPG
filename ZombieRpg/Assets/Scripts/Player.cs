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
        if (GameManager.instance.playerCanMove && !isMoving)
        {
            int directionX = (int)(Input.GetAxisRaw("Horizontal"));
            int directionY = (int)(Input.GetAxisRaw("Vertical"));

            if (directionX != 0)
            {
                directionY = 0;
            }

            if (directionX != 0 || directionY != 0)
            {
                Move(directionX, directionY);
            }
        }
        else if (isMoving && (transform.position == positionEndGlobal)) {
            isMoving = false;
        }
    }

    private void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
