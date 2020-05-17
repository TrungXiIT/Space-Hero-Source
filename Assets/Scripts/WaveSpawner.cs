using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int enemyCount;
        public Transform alien;
        public int alienCount;
        public Transform boss;
        public int bossCount;
        public float rate;
        public float timeBetween2Enemy;
        
    }
    public SpawnState State
    {
        get { return state; }
    }

    public Transform[] spawnPoints;
    [SerializeField]
    private int nextWave = 0;
    public int NextWave
    {
        get { return nextWave + 1; }
    }

    public Wave[] waves;

    private SpawnState state = SpawnState.COUNTING;

    private float searchCoundown;

    public float timeBetweenWaves = 5f;

    private float waveCountdown;

    public float WaveCoundown
    {
        get { return waveCountdown + 1; }
    }

    void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsLive())
            {
                Debug.Log("WAVE COMPLETE");
                WaveComplete();
            }
            else return;
        }
        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }

        Cheat(); 
    }
    void WaveComplete()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("ALL WAVE COMPLETE!! Lopping...");
        }
        else nextWave++;
    }
    bool EnemyIsLive()
    {
        searchCoundown -= Time.deltaTime;
        if(searchCoundown <= 0)
        {
            searchCoundown = 1;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.bossCount; i++)
        {
            SoundManager.soundManager.PlayAudio("Boss");
            yield return new WaitForSeconds(3f);
            SpawnEnemy(_wave.boss);
        }

        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < _wave.enemyCount; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }
        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < _wave.alienCount; i++)
        {
            SpawnEnemy(_wave.alien);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }
    void SpawnEnemy(Transform _enemy)
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(_enemy, sp.position, sp.rotation);

        Debug.Log("Spawning Enemy: " + _enemy.name);
    }
    void Cheat()
    {
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.F12))
        {

            Debug.Log("Next Wave !!");
            if (nextWave < waves.Length)
            {
                 WaveComplete();
            }
        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.F11))
        {

            Debug.Log("Previous Wave !!");
            if (nextWave > 0)
            {
                nextWave -= 2;
                WaveComplete();
            }
        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.F10))
        {

            Debug.Log("LIVEEEEEEEE !!");
            if (GameController.gameController.Lives < 10)
                GameController.gameController.Lives++;
        }
        else if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.F9))
        {
            Debug.Log("DIEEEEEE !!");
            if(GameController.gameController.Lives > 0)
                GameController.gameController.Lives--;
        }
    }
}
