using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowEndingPanelsEffect : GameEffect {

    public Text congratsText;

    public Image statsPanel;
    public Text statsText;

    public float delay = 3.0f;


    public override void ActivateEffect(GameObject activator, float value, Collision2D coll, Collider2D other)
    {
        if (delay > 0.0f)
        {
            StartCoroutine(WaitThenShow(delay));
        }
        else
        {
            ShowPanels();
        }
    }

    private void ShowPanels()
    {
        congratsText.gameObject.SetActive(true);

        int gold = Mathf.Min(Globals.Instance.CurrentGP, 9999);
        int slimes = Mathf.Min(Globals.Instance.SlimesKilled, 9999);

        string statsString = "GOLD: " + gold + "\r\n" + "SLIMES: " + slimes;
        statsText.text = statsString;
        statsPanel.gameObject.SetActive(true);
    }

    private IEnumerator WaitThenShow(float delay)
    {

        float showTime = Time.realtimeSinceStartup + delay;

        while (showTime > Time.realtimeSinceStartup)
        {
            yield return null;
        }

        ShowPanels();

    }
}
