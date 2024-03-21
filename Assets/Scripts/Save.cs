using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Save : MonoBehaviour
{
    public void SaveProgressPlayer()
    {
        YandexGame.savesData.money = GameManager.instance.AmountOfMoney;
        
        YandexGame.SaveProgress();
    }
}
