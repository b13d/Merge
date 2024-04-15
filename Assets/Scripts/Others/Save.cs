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

        // var elementLevel = GameManager.instance.ElementsManager.ElementsLevels;
        var newShopData = YandexGame.savesData.shopData;
        
        // for (int i = 0; i < elementLevel.Length; i++)
        // {
        //     newShopData.price[i] = elementLevel[i].price;
        //     newShopData.income[i] = elementLevel[i].income;
        // }

        int index = 0;
        
        foreach (var income in newShopData.income)
        {
            Debug.Log($"income after save - {income} - levelProduct - {index}");

            index++;
        }
        
        YandexGame.savesData.shopData = newShopData;
        
        YandexGame.SaveProgress();
    }
}
