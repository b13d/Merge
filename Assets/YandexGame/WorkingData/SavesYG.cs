using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ShopData
{
    public List<int> price = new List<int>();
    public List<int> bonusElement = new List<int>();
}

namespace YG
{
    [Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        // Тестовые сохранения для демо сцены
        // // Можно удалить этот код, но тогда удалите и демо (папка Example)
        // public int money = 1;                       // Можно задать полям значения по умолчанию
        // public string newPlayerName = "Hello!";
        // public bool[] openLevels = new bool[3];

        // Ваши сохранения

        // public List<GameObject> _elementsList = new List<GameObject>(9) {null,null,null,null,null,null,null,null,null };
        
        public List<int> _elementsList = new List<int>(9) {999,999,999,999,999,999,999,999,999 };
        
        // public Dictionary<int, GameObject> elements = new Dictionary<int, GameObject>(9);
        
        public int money;

        public int levelPlayer = 0;

        public int countExp = 0;

        public float maxValueSlider = 15;

        public int countActiveBeds = 6;

        public int recordMoney;

        public int oldRecordMoney;

        public ShopData shopData = new ShopData();

        public int lastNewElementLevel = 0;

        public int lastAchievementID;
        
        // public int incomeMoney;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {

            if (shopData.price.Count == 0)
            {
                for (int i = 0; i < _elementsList.Count; i++)
                {
                    shopData.price.Add(0);
                    shopData.bonusElement.Add(0);
                }
            }
            // for (int i = 0; i < _elementsList.Count; i++)
            // {
            //     elements.Add(i, _elementsList[i]);
            // }
            
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            // openLevels[1] = true;
        }
    }
}