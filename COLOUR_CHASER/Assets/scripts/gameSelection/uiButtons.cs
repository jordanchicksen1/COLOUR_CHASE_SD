using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiButtons : MonoBehaviour
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
    
    public void RetryGame1()
    {
        StartCoroutine(RedoGame1());
    }

    public void RetryGame2()
    {
        StartCoroutine(RedoGame2());
    }

    public void RetryGame4()
    {
        StartCoroutine(RedoGame4());
    }

    public void RetryGame6()
    {
        StartCoroutine(RedoGame6());
    }
    public void RetryGame7()
    {
        StartCoroutine(RedoGame7());
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
        string GameScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(GameScene);
    }

    public IEnumerator RedoGame1()
    {
        yield return new WaitForSeconds(0f);
        buttonSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game1");
    }

    public IEnumerator RedoGame2()
    {
        yield return new WaitForSeconds(0f);
        buttonSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game2");
    }

    public IEnumerator RedoGame4()
    {
        yield return new WaitForSeconds(0f);
        buttonSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game4");
    }

    public IEnumerator RedoGame7()
    {
        yield return new WaitForSeconds(0f);
        buttonSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game7");
    }

    public IEnumerator RedoGame6()
    {
        yield return new WaitForSeconds(0f);
        buttonSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game 6");
    }
}
