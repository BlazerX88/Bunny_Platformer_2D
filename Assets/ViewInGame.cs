using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ViewInGame : MonoBehaviour
{
   public TMP_Text coinsLabel;
   public TMP_Text ScoreLabel;
   public TMP_Text HighScoreText;
    // Update is called once per frame

    private static ViewInGame sharedInstance;

    public static ViewInGame GetInstance()
    {
        return sharedInstance;
    }
   
    private void Awake()
    {
        sharedInstance = this;
    }
    private void Start()
    {
        
    }

    public void showHighestScore()
    {
        HighScoreText.text = PlayerController.GetInstance().GetMaxScore().ToString();
    }
    void Update()
    {
       
        if (GameManager.GetInstance().currentgamestate == GameState.InGame)
        {
            coinsLabel.text = GameManager.GetInstance().GetCollectedCoins().ToString();
            ScoreLabel.text = PlayerController.GetInstance().GetDistance().ToString();
        }
    }

    public void UpdateCoins()
    {
        coinsLabel.text = GameManager.GetInstance().GetCollectedCoins().ToString();
    }
}
