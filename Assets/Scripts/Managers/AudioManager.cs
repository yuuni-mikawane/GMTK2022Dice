using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using GameCommon;
using DG.Tweening;

public class AudioManager : SingletonBind<AudioManager>
{
    public AudioSource bgm;
    public AudioClip[] songs;

    private bool isQuieting = false;

    private GameController gameController;

    private void Start()
    {
        gameController = GameController.Instance;
    }

    public void PlayRandomBGM()
    {
        bgm.clip = songs[Random.Range(0, songs.Length)];
        bgm.Play();
    }

    private void Update()
    {
        if (gameController.currentState == GameState.Playing && !bgm.isPlaying)
        {
            PlayRandomBGM();
        }

        if (gameController.currentState == GameState.GameOver)
        {
            if (!isQuieting)
            {
                bgm.DOFade(0, 1f);
                bgm.loop = false;
            }
        }
    }
}
