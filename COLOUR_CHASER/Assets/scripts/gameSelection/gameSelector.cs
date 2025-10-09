using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class gameSelector : MonoBehaviour
{
    public GameObject gamesGroup;
    public GameObject gamesGroup2;
    public GameObject game1Page;
    public GameObject game2Page;
    public GameObject game3Page;
    public GameObject game4Page;
    public GameObject game5Page;
    public GameObject game6Page;
    public GameObject game7Page;

    //button selection stuff
    public GameObject defaultGamesGroupButton;
    public GameObject defaultGamesGroup2Button;
    public GameObject defaultGame1Button;
    public GameObject defaultGame2Button;
    public GameObject defaultGame3Button;
    public GameObject defaultGame4Button;
    public GameObject defaultGame5Button;
    public GameObject defaultGame6Button;
    public GameObject defaultGame7Button;

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
        gamesGroup2.SetActive(false);
        game1Page.SetActive(true);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame1Button);
    }
    public void Game2() 
    {
        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(true);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame2Button);
    }
    public void Game3() 
    {
        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(true);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame3Button);
    }
    public void Game4() 
    {
        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(true);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame4Button);
    }
    public void Game5() 
    {
        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(true);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame5Button);
    }

    public void Game6()
    {
        gamesGroup2.SetActive(false);
        gamesGroup2.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(true);
        game7Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGame6Button);
    }

    public void Game7()
    {
        gamesGroup2.SetActive(false);
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(true);
        clickSFX.Play();

        SetSelected(defaultGame7Button);
    }

    public void ExitPage()
    {
        gamesGroup.SetActive(true);
        gamesGroup2.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();
        
        SetSelected(defaultGamesGroupButton);
    }

    public void ExitPage2()
    {
        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(true);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();

        SetSelected(defaultGamesGroup2Button);
    }

    public void nextGame1()
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(true);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
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
        game6Page.SetActive(false);
        game7Page.SetActive(false);
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
        game6Page.SetActive(false);
        game7Page.SetActive(false);
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
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();   
        SetSelected(defaultGame5Button);
    }
    public void nextGame5() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(true);
        game7Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame6Button);
    }

    public void nextGame6()
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(true);
        clickSFX.Play();
        SetSelected(defaultGame7Button);
    }

    public void nextGame7()
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(true);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
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
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(true);
        clickSFX.Play();
        SetSelected(defaultGame7Button);
    }
    public void prevGame2() 
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(true);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
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
        game6Page.SetActive(false);
        game7Page.SetActive(false);
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
        game6Page.SetActive(false);
        game7Page.SetActive(false);
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
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame4Button);
    }

    public void prevGame6()
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(true);
        game6Page.SetActive(false);
        game7Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame5Button);
    }

    public void prevGame7()
    {
        gamesGroup.SetActive(false);
        game1Page.SetActive(false);
        game2Page.SetActive(false);
        game3Page.SetActive(false);
        game4Page.SetActive(false);
        game5Page.SetActive(false);
        game6Page.SetActive(true);
        game7Page.SetActive(false);
        clickSFX.Play();
        SetSelected(defaultGame6Button);
    }

    public void nextGameGroup1()
    {
        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(true);

        SetSelected(defaultGamesGroup2Button);
    }

    public void previousGameGroup1()
    {
        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(true);

        SetSelected(defaultGamesGroup2Button);
    }

    public void nextGameGroup2()
    {
        gamesGroup.SetActive(true);
        gamesGroup2.SetActive(false);

        SetSelected(defaultGamesGroupButton);
    }

    public void previousGameGroup2()
    {
        gamesGroup.SetActive(true);
        gamesGroup2.SetActive(false);

        SetSelected(defaultGamesGroupButton);
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

    public void PlayGame6()
    {
        SceneManager.LoadScene("Game 6");
    }

    public void PlayGame7()
    {
        SceneManager.LoadScene("Game7");
    }
}
