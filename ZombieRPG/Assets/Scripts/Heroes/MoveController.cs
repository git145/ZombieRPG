﻿using UnityEngine;

// File: MoveController.cs
// Author: Richard Kneale
// Date created: 13th October 2018
// Date modified: 13th October 2018
// Description: Manages player movement

public class MoveController : MonoBehaviour {
    public bool CanMove {
        get;
        set;
    }

    // The speed that the player will move at
    [SerializeField]
    private float speed = 2f;

    // The Rigidbody component on the object
    private Rigidbody2D objectRigidbody;

    private Vector2 target;

    // Use this for initialization
    void Start () {
        // Reference the Rigidbody component on the player
        objectRigidbody = GetComponent<Rigidbody2D>();

        target = objectRigidbody.position;

        CanMove = true;
    }
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    // Moves the player
    private void Move()
    {
        if (CanMove)
        {
            // Determine whether the user has pressed the horizontal or vertical movement buttons
            float _movementX = Input.GetAxisRaw("Horizontal");
            float _movementY = Input.GetAxisRaw("Vertical");

            if ((_movementX != 0) && (_movementY == 0))
            {
                target += new Vector2 (0.48f * _movementX, 0);
                CanMove = false;
            }
            else if ((_movementX == 0) && (_movementY != 0))
            {
                target += new Vector2(0, 0.48f * _movementY);
                CanMove = false;
            }
        }

        Vector2 objectRigidbodyposition = objectRigidbody.position;

        objectRigidbody.position = Vector3.MoveTowards(objectRigidbodyposition, target, Time.deltaTime * speed);

        if (objectRigidbodyposition == target)
        {
            CanMove = true;
        }
    }
}
