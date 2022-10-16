using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyScoreManager : MonoBehaviour
{
    public static MyScoreManager instance;
    public List<PlayerController> players;

    private List<int> scores = new List<int>();
    public int PointsForSlaying = 10;
    public int PointsForSuicide = -5;


    public static bool playerOneAlive = false, playerTwoAlive = false;
       



    public GameObject scoreCardPrefab1, scoreCardPrefab2;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scores.Add(0);
        scores.Add(0);
    }



    
    public void AddScoreForPlayerOneDeath()
    {
        scores[1] += PointsForSlaying;
        Debug.Log("Player two scored points for slaying");
    }

    public void AddScoreForPlayerTwoDeath()
    {
        scores[0] += PointsForSlaying;
        Debug.Log("Player one scored points for slaying");
    }



    // Update is called once per frame
    void Update()
    {
        scoreCardPrefab1.GetComponent<ScoreCard>().SetScore(scores[0]);
        scoreCardPrefab2.GetComponent<ScoreCard>().SetScore(scores[1]);
    }

    public enum ScoreType
    {
        Slay,
        Suicide
    }

}
