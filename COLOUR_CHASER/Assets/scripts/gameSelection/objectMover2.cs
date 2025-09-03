using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectMover2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D objectRB;

    private void Start()
    {
        objectRB = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        objectRB.velocity = new Vector2(moveSpeed, 0);
    }
}
