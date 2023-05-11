using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : GameScore
{
    // Start is called before the first frame update
    void Start()
    {
        Text myText = GetComponent<Text>();
        myText.text = "SCORE: " + GameScore.score2.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

}
