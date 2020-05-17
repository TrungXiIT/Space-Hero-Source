using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    Text highScore;

    void Start()
    {
        highScore = GetComponent<Text>();
    }

    void Update()
    {
        highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore");
    }
}
