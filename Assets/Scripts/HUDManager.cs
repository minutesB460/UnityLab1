using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private Vector3[] scoreTextPosition = {
        new Vector3(-250, 180, 0),
        new Vector3(-800, 0, 0)
        };
    private Vector3[] restartButtonPosition = {
        new Vector3(321, 187, 0),
        new Vector3(-800, -150, 0)
    };

    public GameObject scoreText;
    public Transform restartButton;

    public GameObject scoreTextPanel;

    public GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        // hide gameover panel
        gameOverPanel.SetActive(false);
        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.localPosition = restartButtonPosition[0];

    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        scoreTextPanel.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.localPosition = restartButtonPosition[1];
        
    }
}
