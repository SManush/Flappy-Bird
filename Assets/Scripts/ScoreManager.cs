using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;

    void Start()
    {
        UpdateScore(1);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = score.ToString();
    }
}
