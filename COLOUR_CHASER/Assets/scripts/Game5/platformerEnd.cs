using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class platformerEnd : MonoBehaviour
{
    public AudioSource buttonSound;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void BackToGameSelection()
    {
        StartCoroutine(GameSelection());
    }

    public void Retry()
    {
        StartCoroutine(RetryGame());
    }

    public IEnumerator GameSelection()
    {
        yield return new WaitForSeconds(0f);
        buttonSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameSelect");
    }

    public IEnumerator RetryGame()
    {
        yield return new WaitForSeconds(0f);
        buttonSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game5");
    }

}
