using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class objectMover : MonoBehaviour
{
    public bool moveForward = false;
    public float moveSpeed = 5f;
    private Rigidbody2D platformRB;

    public void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();
        platformRB.velocity = new Vector2(0, moveSpeed);
        Debug.Log("should start the platform");
    }

    public void Update()
    {
        if (moveForward == true)
        {
            platformRB.velocity = new Vector2(0, moveSpeed);

        }

        if (moveForward == false)
        {
            platformRB.velocity = new Vector2(0, -moveSpeed);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Front")
        {
            moveForward = false;
            Debug.Log("hit top trigger");
        }

        if (other.tag == "Back")
        {
            moveForward = true;
            Debug.Log("hit bottom trigger");
        }
    }
}
