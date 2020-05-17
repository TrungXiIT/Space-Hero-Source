using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    [SerializeField] Animator waveAnimator;

    [SerializeField] WaveSpawner spawner;

    [SerializeField] Text waveCountdown;

    [SerializeField] Text waveConutText;

    WaveSpawner.SpawnState previousState;
    void Start()
    {
        if(spawner == null)
        {
            Debug.LogError("No spawner referenced");
            this.enabled = false;
        }
        if (waveAnimator == null)
        {
            Debug.LogError("No waveAnimator referenced");
            this.enabled = false;
        }
        if (waveCountdown == null)
        {
            Debug.LogError("No waveCountdown referenced");
            this.enabled = false;
        }
        if (waveConutText == null)
        {
            Debug.LogError("No waveConutText referenced");
            this.enabled = false;
        }
    }
    void Update()
    {
        switch (spawner.State)
        {
            case WaveSpawner.SpawnState.COUNTING:
                UpdateCountingUI();
                break;
            case WaveSpawner.SpawnState.SPAWNING:
                UpdateSpawningUI();
                break;
        }
        previousState = spawner.State;
    }
    void UpdateCountingUI()
    {
        if (previousState != WaveSpawner.SpawnState.COUNTING)
        {
            waveAnimator.SetBool("WaveIncoming", false);
            waveAnimator.SetBool("WaveCountdown", true);
            //Debug.Log("COUNTING");
        }
        waveCountdown.text = ((int)spawner.WaveCoundown).ToString();
    }
    void UpdateSpawningUI()
    {
        if (previousState != WaveSpawner.SpawnState.SPAWNING)
        {
            waveAnimator.SetBool("WaveIncoming", true);
            waveAnimator.SetBool("WaveCountdown", false);

            waveConutText.text = spawner.NextWave.ToString();
            //Debug.Log("SPAWNING");
        }
    }
}
