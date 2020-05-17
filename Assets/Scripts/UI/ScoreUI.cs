using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Text scoresText;

    void Start()
    {
        scoresText = GetComponent<Text>();
    }

    void Update()
    {
        scoresText.text = "SCORES: " + GameController.gameController.Scores.ToString();
    }
}
