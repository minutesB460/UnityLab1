using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainMenu: MonoBehaviour
{
    public GameObject highscoreText;
    public IntVariable gameScore;

    void Start()
    {
        SetHighscore();
    }

    public void GoToLoadScene() {
        SceneManager.LoadSceneAsync("Loading",LoadSceneMode.Single);
    }

    void SetHighscore() {
    highscoreText.GetComponent<TextMeshProUGUI>().text = "TOP- "+gameScore.previousHighestValue.ToString("D6");
    }

    public void ResetHighScore() {
        //want to reset highscore but not leave the button in the state of "pressed" after highscore is reset
        
        GameObject eventSystem = GameObject.Find("EventSystem");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        gameScore.ResetHighestValue();
        SetHighscore();
    }
}