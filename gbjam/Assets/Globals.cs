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
    public HealthBar healthBar;
    public DungeonManager dungeonManager;
    public ScreenTransition screenTransition;

    private int currentGP = 0;
    public int CurrentGP
    {
        get
        {
            return currentGP;
        }
        set
        {
            currentGP = value;

            int displayedGP = Mathf.Min(currentGP, 99);

            string currentGPString = displayedGP.ToString();
            currentGPString = currentGPString.PadLeft(2, '0');

            currentGPText.text = currentGPString;

        }
    }

    private int currentFloor = 1;
    public int CurrentFloor
    {
        get
        {
            return currentFloor;
        }
        set
        {
            currentFloor = value;

            int displayedFloor = Mathf.Min(currentFloor, 99);

            string currentFloorString = displayedFloor.ToString();
            currentFloorString = currentFloorString.PadLeft(2, '0');

            currentFloorText.text = currentFloorString;

        }
    }

    public Text currentGPText;
    public Text currentFloorText;

    public override void Start()
    {
        base.Start();

        if (this == null) { return; }

        if (playIntro && firstPanel != null)
        {
            //play intro   
            acceptPlayerGameInput = false;
            //GUIManager.Instance.FadeScreen(1.0f, 1.0f, 0.0f);
            firstPanel.introParent.SetActive(true);
            firstPanel.DisplayPanel();
        }
        else
        {
            StartGame();
        }
    }

    public void IntroFinished()
    {
        if (playIntro)
        {
            playIntro = false;            
            StartGame();
        }
    }

    public void StartGame()
    {
        StartCoroutine(DoStartGame());
        dungeonManager.ResetDungeon();
    }

    private IEnumerator DoStartGame()
    {
        //disable player object
        Globals.Instance.Pause(true);
        Globals.Instance.acceptPlayerGameInput = false;

        //TODO: do a screen transition
        yield return StartCoroutine(screenTransition.TransitionCoverScreen(1.0f));

        try
        {
            firstPanel.introParent.SetActive(false);
        }
        catch { }

        yield return null;

        yield return StartCoroutine(screenTransition.TransitionUncoverScreen(1.0f));

        Globals.Instance.Pause(false);
        Globals.Instance.acceptPlayerGameInput = true;

        //TODO: do screen transition in
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
