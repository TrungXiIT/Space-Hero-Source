using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnMouseOver()
    {
        SoundManager.soundManager.PlayAudio("ButtonHover");
    }
    public void Resume()
    {
        GameController.gameController._Resume();
    }
}
