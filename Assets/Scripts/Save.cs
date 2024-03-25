using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Save : MonoBehaviour
{
    public void SaveProgressPlayer()
    {
        YandexGame.savesData.money = GameManager.instance.AmountOfMoney;
        GameManager.instance.SavePositionElement();

        if (GameManager.instance.GetCoinManager.GetMoney > YandexGame.savesData.recordMoney)
        {
            YandexGame.savesData.recordMoney = GameManager.instance.GetCoinManager.GetMoney;
            
            YandexGame.NewLeaderboardScores("recordMoney", GameManager.instance.GetCoinManager.GetMoney);
        }
        
        YandexGame.SaveProgress();
    }
}
