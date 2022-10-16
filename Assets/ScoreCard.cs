using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCard : MonoBehaviour
{

    public Text Score;
    // Start is called before the first frame update
public void SetScore(int score)
    {
        Score.text = score.ToString();
    }
}
