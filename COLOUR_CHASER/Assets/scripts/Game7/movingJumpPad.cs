using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingJumpPad : MonoBehaviour
{
    public bool hasHitBox = false;
    public bool movingRight = false;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    void Start()
    {
       movingRight = true;
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movingRight == true)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }

        if (movingRight == false)
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "MoveBox" && movingRight == true && hasHitBox == false)
        {
            movingRight = false;
            hasHitBox = true;
            StartCoroutine(HitBox());
            Debug.Log("hit movebox");
        }

        if (other.tag == "MoveBox" && movingRight == false && hasHitBox == false)
        {
            movingRight = true;
            hasHitBox = true;
            StartCoroutine(HitBox());
            Debug.Log("hit movebox again");
        }
    }

    public IEnumerator HitBox()
    {
        yield return new WaitForSeconds(0.5f);
        hasHitBox = false;
    }
}
