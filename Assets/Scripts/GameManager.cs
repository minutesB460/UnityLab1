using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager: Singleton<GameManager>
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;

    private int score = 0;

    public IntVariable gameScore;




    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;

         // subscribe to scene manager scene change
        SceneManager.activeSceneChanged += SceneSetup;

            // use it as per normal

        // reset score
        gameScore.Value = 0;


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        // reset score
        score = 0;
        gameScore.Value = 0; //
        SetScore(score);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
        
        RestoreAllQuestionBoxes();
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        SetScore(score);
        // increase score by 1
        gameScore.ApplyChange(1);

        // invoke score change event with current score to update HUD
        scoreChange.Invoke(gameScore.Value);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(score);
    }

    [System.Obsolete]
    public void RestoreAllQuestionBoxes()
{
    QuestionBox[] boxes = FindObjectsOfType<QuestionBox>(); 
    foreach (QuestionBox box in boxes)
    {
        box.RestoreBox();
    }
}

}