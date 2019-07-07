using System.Collections;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = 0.1f;

    public GameObject sprite;

    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidBody;

    private float inverseMoveTime;

    private RaycastHit2D detectHit;

    protected Vector3 positionEndGlobal;

    protected bool isMoving;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

        rigidBody = GetComponent<Rigidbody2D>();

        inverseMoveTime = 1f / moveTime;
    }

    protected bool Move(int directionX, int directionY)
    {
        bool isHit = false;

        Vector3 positionStart = transform.position;

        positionEndGlobal = positionStart + new Vector3(directionX, directionY, 0);

        SetDetectHit(positionStart, positionEndGlobal);

        if (detectHit.transform == null)
        {
            StartCoroutine(SmoothMovement(positionEndGlobal));

            isMoving = true;

            isHit = true;
        }

        return isHit;
    }

    private void SetDetectHit(Vector3 positionStart, Vector3 positionEnd)
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