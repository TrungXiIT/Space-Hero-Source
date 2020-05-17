using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    Text livesText;

    void Start()
    {
        livesText = GetComponent<Text>();
    }

    void Update()
    {
        livesText.text = "LIVES: " + GameController.gameController.Lives.ToString();        
    }
}
