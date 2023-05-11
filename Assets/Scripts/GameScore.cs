﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    Text scoreTextUI;
    int score;
    public static int score2;

    public int Score
    {
        get
        {
            return this.score;
        }

        set
        {
            this.score = value;
            UpdateScoreTextUI();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // get the Text UI component of this gameObject
        scoreTextUI = GetComponent<Text>();
    }

    // Update is called once per frame
    // function to update the score Text UI
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:0000000}", score);
        scoreTextUI.text = scoreStr;
        
        score2 = int.Parse(scoreStr);
        // Debug.Log("Score2 = " + score2);
    }
}
