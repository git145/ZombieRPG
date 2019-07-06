using System.Collections;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;

    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    private float inverseMoveTime;

    RaycastHit2D detectHit;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        rigidBody = GetComponent<Rigidbody2D>();

        inverseMoveTime = 1f / moveTime;
    }

    protected bool Move(float directionX, float directionY)
    {
        bool isHit = false;

        Vector2 positionStart = transform.position;

        Vector2 positionEnd = positionStart + new Vector2(directionX, directionY);

        SetDetectHit(positionStart, positionEnd);

        if (detectHit.transform == null)
        {
            StartCoroutine(SmoothMovement(positionEnd));

            isHit = true;
        }

        return isHit;
    }

    private void SetDetectHit(Vector2 positionStart, Vector2 positionEnd)
    {
        boxCollider.enabled = false;

        detectHit = Physics2D.Linecast(positionStart, positionEnd, blockingLayer);

        boxCollider.enabled = true;
    }

    protected IEnumerator SmoothMovement(Vector3 positionEnd)
    {
        float squareOfRemainingDistance = GetSquareOfRemainingDistance(positionEnd);

        while (squareOfRemainingDistance > float.Epsilon)
        {
            Vector3 newPostion = Vector3.MoveTowards(rigidBody.position, positionEnd, inverseMoveTime * Time.deltaTime);

            rigidBody.MovePosition(newPostion);

            squareOfRemainingDistance = GetSquareOfRemainingDistance(positionEnd);

            yield return null;
        }
    }

    private float GetSquareOfRemainingDistance(Vector3 positionEnd)
    {
        return (transform.position - positionEnd).sqrMagnitude;
    }

    protected virtual void AttemptMove<T>(int directionX, int directionY)
        where T : Component
    {
        bool canMove = Move(directionX, directionY);

        if (!(detectHit.transform == null))
        {
            T componentHit = detectHit.transform.GetComponent<T>();

            if (!canMove && componentHit != null)
            {
                OnCantMove(componentHit);
            }
        }
    }

    protected virtual void OnCantMove<T>(T component)
        where T : Component
    {
    }
}