using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ParticleSystem p1Scored;
    public ParticleSystem p2Scored;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("GoalP1"))
        {
            p2Scored.Play();
        }

        if (other.gameObject.CompareTag("GoalP2"))
        {
            p1Scored.Play();
        }
    }
}
