using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCommon;

public class GameController : SingletonBind<GameController>
{
    public float killCamSlowMoTime = 3f;
    public float slowTo = 0.1f;


    public void ActivateKillCam()
    {
        StartCoroutine(KillCam(killCamSlowMoTime, slowTo));
    }

    private IEnumerator KillCam(float slowmoTime, float slowAmount)
    {
        Time.timeScale = slowAmount;
        yield return new WaitForSecondsRealtime(slowmoTime);
        Time.timeScale = 1f;
    }
}
