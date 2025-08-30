using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{
    public float respawnTime = 5f; // time before box reappears
    private SpriteRenderer sr;
    private Collider2D col;

    private string[] abilities = { "Tire", "Oil", "Speed", "Shockwave" };

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        CarController car = other.GetComponent<CarController>();
        if (car != null)
        {
            // Give random ability
            string randomAbility = abilities[Random.Range(0, abilities.Length)];
            car.GiveAbility(randomAbility);

            // Disable box
            sr.enabled = false;
            col.enabled = false;

            // Respawn later
            Invoke(nameof(RespawnBox), respawnTime);
        }
    }

    void RespawnBox()
    {
        sr.enabled = true;
        col.enabled = true;
    }
}
