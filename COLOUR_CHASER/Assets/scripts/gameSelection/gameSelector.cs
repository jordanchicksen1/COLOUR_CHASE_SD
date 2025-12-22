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
    public GameObject game8Page;
    public GameObject game9Page;
    public GameObject game10Page;
    public GameObject chooseRacePage;

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
    public GameObject defaultGame8Button;
    public GameObject defaultGame9Button;
    public GameObject defaultGame10Button;
    public GameObject defaultChooseRaceButton;

    public AudioSource clickSFX;

    GameObject[] gamePages;
    GameObject[] defaultGameButtons;
    int currentGameIndex = -1;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameData.coinScore = 0;

        gamePages = new GameObject[]
        {
            game1Page, game2Page, game3Page, game4Page, game5Page,
            game6Page, game7Page, game8Page, game9Page, game10Page
        };

        defaultGameButtons = new GameObject[]
        {
            defaultGame1Button, defaultGame2Button, defaultGame3Button,
            defaultGame4Button, defaultGame5Button, defaultGame6Button,
            defaultGame7Button, defaultGame8Button, defaultGame9Button,
            defaultGame10Button
        };

        ShowGamesGroup1();
    }

    /* ----------------- CORE HELPERS ----------------- */

    void DisableAllPages()
    {
        foreach (var page in gamePages)
            page.SetActive(false);

        chooseRacePage.SetActive(false);
    }

    void EnableAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
            child.gameObject.SetActive(true);
    }

    void SetSelected(GameObject obj)
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(obj);
    }

    void ShowGamesGroup1()
    {
        currentGameIndex = -1;
        DisableAllPages();
        gamesGroup.SetActive(true);
        gamesGroup2.SetActive(false);
        EnableAllChildren(gamesGroup);
        SetSelected(defaultGamesGroupButton);
        //clickSFX.Play();
    }

    void ShowGamesGroup2()
    {
        currentGameIndex = -1;
        DisableAllPages();
        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(true);
        EnableAllChildren(gamesGroup2);
        SetSelected(defaultGamesGroup2Button);
        //clickSFX.Play();
    }

    void ShowGameByIndex(int index)
    {
        currentGameIndex = index;

        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(false);
        DisableAllPages();

        gamePages[index].SetActive(true);
        clickSFX.Play();
        SetSelected(defaultGameButtons[index]);
    }

    /* ----------------- GAME PAGES ----------------- */

    public void Game1() => ShowGameByIndex(0);
    public void Game2() => ShowGameByIndex(1);
    public void Game3() => ShowGameByIndex(2);
    public void Game4() => ShowGameByIndex(3);
    public void Game5() => ShowGameByIndex(4);
    public void Game6() => ShowGameByIndex(5);
    public void Game7() => ShowGameByIndex(6);
    public void Game8() => ShowGameByIndex(7);
    public void Game9() => ShowGameByIndex(8);
    public void Game10() => ShowGameByIndex(9);

    public void ChooseRace()
    {
        currentGameIndex = -1;
        gamesGroup.SetActive(false);
        gamesGroup2.SetActive(false);
        DisableAllPages();
        chooseRacePage.SetActive(true);
        clickSFX.Play();
        SetSelected(defaultChooseRaceButton);
    }

    /* ----------------- NEXT / PREVIOUS GAME PAGE ----------------- */

    public void NextGame()
    {
        if (currentGameIndex < 0) return;
        int next = (currentGameIndex + 1) % gamePages.Length;
        ShowGameByIndex(next);
    }

    public void PreviousGame()
    {
        if (currentGameIndex < 0) return;
        int prev = (currentGameIndex - 1 + gamePages.Length) % gamePages.Length;
        ShowGameByIndex(prev);
    }

    /* ----------------- EXIT / NAV ----------------- */

    public void ExitPage()
    {
        clickSFX.Play();
        ShowGamesGroup1();
    }

    public void ExitPage2()
    {
        clickSFX.Play();
        ShowGamesGroup2();
    }

    public void nextGameGroup1()
    {
        ShowGamesGroup2();
        clickSFX.Play();
    }
    public void previousGameGroup1()
    {
        ShowGamesGroup2();
        clickSFX.Play();
    }
    public void nextGameGroup2()
    {
        ShowGamesGroup1();
        clickSFX.Play();
    }
    public void previousGameGroup2()
    {
        ShowGamesGroup1();
        clickSFX.Play();
    }


    /* ----------------- SCENE LOAD ----------------- */

    public void QuitGame() => Application.Quit();

    public void PlayGame1() => SceneManager.LoadScene("Game1");
    public void PlayGame2() => SceneManager.LoadScene("Game5");
    public void PlayGame3() => SceneManager.LoadScene("Game2");
    public void PlayGame4() => SceneManager.LoadScene("Game4");
    public void PlayGame5() => SceneManager.LoadScene("Game3");
    public void PlayGame6() => SceneManager.LoadScene("Game 6");
    public void PlayGame7() => SceneManager.LoadScene("Game7");
    public void PlayGame8() => SceneManager.LoadScene("Game8");
    public void PlayGame9() => SceneManager.LoadScene("Game9");

    public void PlayGame10()
    {
        game10Page.SetActive(false);
        chooseRacePage.SetActive(true);
        SetSelected(defaultChooseRaceButton);
        clickSFX.Play();
    }

    public void PlayRace1() => SceneManager.LoadScene("Game10.1");
    public void PlayRace2() => SceneManager.LoadScene("Game10.2");
    public void PlayRace3() => SceneManager.LoadScene("Game10.3");
}
