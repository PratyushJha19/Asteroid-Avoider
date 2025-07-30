using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] private int scoreIncrement;
    public float score;
    public int intScore;
    public const string highScoreKey = "HighScore";
    private bool shouldCount = true;

    void Update()
    {
        if (!shouldCount) { return; }

        score += Time.deltaTime * scoreIncrement;
        intScore = (int)score;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);

        if (intScore > currentHighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, intScore);
        }
    }
}