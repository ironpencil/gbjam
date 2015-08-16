using UnityEngine;
using System.Collections;

public class UseOtherSortingLayer : MonoBehaviour {

    public SpriteRenderer thisSprite;
    public SpriteRenderer otherSprite;
    public int relativeSortAdjust = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        thisSprite.sortingOrder = otherSprite.sortingOrder + relativeSortAdjust;

    }
}
