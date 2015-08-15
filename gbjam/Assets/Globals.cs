using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Globals : Singleton<Globals>
{    

    public bool paused = false;
    public bool acceptPlayerGameInput = true;

    public bool playIntro = true;
    public IntroPanel firstPanel;

    public WeaponChargeBar weaponChargeBar;

    public override void Start()
    {
        base.Start();

        if (this == null) { return; }

        if (playIntro && firstPanel != null)
        {
            //play intro   
            acceptPlayerGameInput = false;
            //GUIManager.Instance.FadeScreen(1.0f, 1.0f, 0.0f);
            firstPanel.DisplayPanel();
        }
        else
        {
            //StartCoroutine(DoNewLevelSetup(currentLevel));
        }
    }

    public void IntroFinished()
    {
        if (playIntro)
        {
            playIntro = false;
            //acceptPlayerGameInput = true;
            //StartCoroutine(DoNewLevelSetup(currentLevel));
        }
    }

    public void Pause(bool pause)
    {
        if (isQuitting) { return; }

        paused = pause;

        if (paused)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }

    [ContextMenu("Toggle Pause")]
    public void TogglePause()
    {
        Pause(!paused);
    }

    public bool isQuitting = false;

    public void DoQuit()
    {
        Pause(false);
        isQuitting = true;
        StartCoroutine(WaitAndQuit(1.0f));
    }

    private IEnumerator WaitAndQuit(float time)
    {
        yield return new WaitForSeconds(time);

        Application.Quit();
    }
}
