using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreSystem.highScoreKey, 0);
        highScoreText.text = $"High Score: {highScore}";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}