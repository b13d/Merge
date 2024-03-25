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

        if (YandexGame.savesData.oldRecordMoney < YandexGame.savesData.recordMoney)
        {
            YandexGame.savesData.oldRecordMoney = YandexGame.savesData.recordMoney;
            YandexGame.NewLeaderboardScores("recordMoney", YandexGame.savesData.recordMoney);
        }
        
        YandexGame.SaveProgress();
    }
}
