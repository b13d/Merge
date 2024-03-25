using System.Collections.Generic;
using UnityEngine;

namespace YG
{
    [System.Serializable]
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

        public List<GameObject> _elementsList = new List<GameObject>(9) {null,null,null,null,null,null,null,null,null };
        
        public Dictionary<int, GameObject> elements = new Dictionary<int, GameObject>(9);
        
        public int money;

        public int levelPlayer = 0;

        public int countExp = 0;

        public float maxValueSlider = 2;

        public int countActiveBeds = 3;

        // public int incomeMoney;

        // Поля (сохранения) можно удалять и создавать новые. При обновлении игры сохранения ломаться не должны


        // Вы можете выполнить какие то действия при загрузке сохранений
        public SavesYG()
        {
            for (int i = 0; i < _elementsList.Count; i++)
            {
                elements.Add(i, _elementsList[i]);
            }
            
            // Допустим, задать значения по умолчанию для отдельных элементов массива

            // openLevels[1] = true;
        }
    }
}