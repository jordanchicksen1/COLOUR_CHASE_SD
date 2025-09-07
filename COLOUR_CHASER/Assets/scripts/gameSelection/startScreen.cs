using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScreen : MonoBehaviour
{
    public Camera sceneCam;

    public Color first;
    public Color second;
    public Color third;
    public Color fourth; 
    public Color fifth;

    public GameObject startButton;
    public GameObject sdsPresentsText;
    public GameObject title;
    public GameObject movingStuff;

    public AudioSource clickSFX;

    public void Start()
    {
        StartCoroutine(StartColours());
    }

    public void StartButton()
    {
        StartCoroutine(PressedButton());
     
    }


    public IEnumerator StartColours()
    {
        yield return new WaitForSeconds(0.5f);
        sdsPresentsText.SetActive(true);
        yield return new WaitForSeconds(2f);
        sdsPresentsText.SetActive(false);
        yield return new WaitForSeconds(0f);
        title.SetActive(true);
        //sceneCam.backgroundColor = fifth;
        yield return new WaitForSeconds(0.5f);
        sceneCam.backgroundColor = first;
        yield return new WaitForSeconds(0.5f);
        sceneCam.backgroundColor = second;
        yield return new WaitForSeconds(0.5f);
        sceneCam.backgroundColor = third;
        yield return new WaitForSeconds(0.5f);
        sceneCam.backgroundColor = fourth;
        yield return new WaitForSeconds(0.5f);
        sceneCam.backgroundColor = fifth;
        movingStuff.SetActive(true);
        yield return new WaitForSeconds(1f);
        startButton.SetActive(true);

    }

    public IEnumerator PressedButton()
    {
        yield return new WaitForSeconds(0f);
        clickSFX.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameSelect");
    }
}
