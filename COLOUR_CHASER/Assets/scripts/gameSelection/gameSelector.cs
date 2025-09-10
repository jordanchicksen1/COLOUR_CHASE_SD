using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class gameSelector : MonoBehaviour
{
    public GameObject gamesGroup;
    public GameObject game1Page;
    public GameObject game2Page;
    public GameObject game3Page;
    public GameObject game4Page;
    public GameObject game5Page;

    //button selection stuff
    public GameObject defaultGamesGroupButton;
    public GameObject defaultGame1Button;
    public GameObject defaultGame2Button;
    public GameObject defaultGame3Button;
    public GameObject defaultGame4Button;
    public GameObject defaultGame5Button;

    public AudioSource clickSFX;

    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void SetSelected(GameObject newSelected)
    {
        EventSystem.current.SetSelectedGameObject(null); // clear old selection
        EventSystem.current.SetSelectedGameObject(newSelected); // set new one
    }
    public void Game1()
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(true);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame1Button);
    }
    public void Game2() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(true);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame2Button);
    }
    public void Game3() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(true);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame3Button);
    }
    public void Game4() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(true);
        game5Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame4Button);
    }
    public void Game5() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(true);
        clickSFX.Play();

        SetSelected(defaultGame5Button);
    }

    public void ExitPage()
    {
        gamesGroup.SetActive(true);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();
        
        SetSelected(defaultGamesGroupButton);
    }

    public void nextGame1()
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(true);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame2Button);
    }
    public void nextGame2() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(true);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame3Button);
    }
    public void nextGame3() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(true);
        game5Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame4Button);
    }
    public void nextGame4() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(true);
        clickSFX.Play();   
        SetSelected(defaultGame5Button);
    }
    public void nextGame5() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(true);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame1Button);
    }

    public void prevGame1()
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(true);
        clickSFX.Play();
        SetSelected(defaultGame5Button);
    }
    public void prevGame2() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(true);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame1Button);
    }
    public void prevGame3() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(true);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame2Button);
    }
    public void prevGame4() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(true);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame3Button);
    }
    public void prevGame5() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(true);
        game5Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame4Button);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame1()
    {
        SceneManager.LoadScene("Game1");
    }
    public void PlayGame2()
    {
        SceneManager.LoadScene("Game5");
    }

    public void PlayGame3()
    {
        SceneManager.LoadScene("Game2");
    }

    public void PlayGame4()
    {
        SceneManager.LoadScene("Game4");
    }

    public void PlayGame5()
    {
        SceneManager.LoadScene("Game3");
    }
}
