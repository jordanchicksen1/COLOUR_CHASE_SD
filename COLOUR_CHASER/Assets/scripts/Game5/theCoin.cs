using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theCoin : MonoBehaviour
{
    public GameObject coinSound;
    public GameObject coinParticle;
    public GameObject coin;
    public GameObject coinText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            StartCoroutine(GotTheCoin());

            // Add +1 coin to global counter
            GameData.coinScore++;
        }
    }

    private IEnumerator GotTheCoin()
    {
        Destroy(coin);
        coinParticle.SetActive(true);
        coinSound.SetActive(true);
        coinText.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        Destroy(this.gameObject);
    }
}
