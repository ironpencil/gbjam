using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponChargeBar : MonoBehaviour {

    public Image fillBar;

    public float imageMaxHeight = 100.0f;    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetChargeValue(float percent)
    {        
        int imageHeight = (int) (imageMaxHeight * percent);


        fillBar.rectTransform.sizeDelta = new Vector2(fillBar.rectTransform.sizeDelta.x, imageHeight);


    }
}
