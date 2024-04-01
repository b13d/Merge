using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Achievements : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gameObjectAchievements;
    [SerializeField] private List<Image> _imagesAchievements;
    
    void Start()
    {
        Initial();
    }

    void Initial()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _gameObjectAchievements.Add(transform.GetChild(i).gameObject);
        }


        foreach (var achievement in _gameObjectAchievements)
        {
            _imagesAchievements.Add(achievement.GetComponent<Image>());
        }
        
        if (YandexGame.savesData.lastAchievementID != 0)
        {
            AlreadyCompletedAchievements(YandexGame.savesData.lastAchievementID);
        }

        // for (int i = 0; i < _imagesAchievements.Count; i++)
        // {
        //     _imagesAchievements[i].color = Color.white;
        // }
    }

    public void AlreadyCompletedAchievements(int id)
    {
        for (int i = 1; i <= id; i++)
        {
            _imagesAchievements[i].color = Color.white;

            if (_gameObjectAchievements[i].transform.childCount > 0)
            {
                Destroy(_gameObjectAchievements[i].transform.GetChild(0).gameObject);
            }
        }
    }

    public void ActiveAchievements(int idElement)
    {
        Initial();
        
        _imagesAchievements[idElement].color = Color.white;
        Destroy(_gameObjectAchievements[idElement].transform.GetChild(0).gameObject);

        YandexGame.savesData.lastAchievementID = idElement;
        
        YandexGame.SaveProgress();
    }
}
