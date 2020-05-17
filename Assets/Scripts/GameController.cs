using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject playerInfo;

    public Transform player;
    public Transform respawnPlayerPositon;

    public int maxLives;
    public int Lives = 3;
    public int Scores = 0;

    public static GameController gameController;

    bool isFirstOpenGame = true;

    void Awake()
    {
        IsGameStartForTheFirstTime();
    }
    void Start()
    {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        playerInfo.SetActive(true);

        Lives = maxLives;
        gameController = GetComponent<GameController>();
        
        if(respawnPlayerPositon == null)
            respawnPlayerPositon = GameObject.FindGameObjectWithTag("RespawningPositon").GetComponent<Transform>();
  
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
            if (pausePanel.activeSelf)
                Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }
    void IsGameStartForTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("IsGameStartForTheFirstTime"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("IsGameStartForTheFirstTime", 0);
        }
    }

    public void _RestartGame()
    {
        SoundManager.soundManager.PlayAudio("ButtonPress");
        SceneManager.LoadScene("GamePlay");
    }
    public void _StartGame()
    {
        SoundManager.soundManager.PlayAudio("ButtonPress");
        SceneManager.LoadScene("GamePlay");
    }
    public void _Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void _Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void _Resume()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void _Quit()
    {
        Application.Quit();
    }
    public void _PlayerDead(GameObject player)
    {
        if (Lives > 0)
        {
            StartCoroutine(_RespawnPlayer(3.5f));
            Lives -= 1;
        }
        else _GameOver();

        Destroy(player);
    }
    public IEnumerator _RespawnPlayer(float spawnDelay)
    {
        yield return new WaitForSeconds(2);
        SoundManager.soundManager.PlayAudio("Respawning");
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(player, respawnPlayerPositon.position, respawnPlayerPositon.rotation);
    }
    public void _GameOver()
    {
        if (PlayerPrefs.GetInt("HighSocre") < Scores)
            PlayerPrefs.SetInt("HighScore", Scores);

        playerInfo.SetActive(false);
        SoundManager.soundManager.PlayAudio("GameOver");
        gameOverPanel.SetActive(true);
    }
}
