using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class GameData
    {
        public int score;
        public int birds;
        public int lastLevelIndex = 1;
        private int _defaultBirdAmount;

        public GameData(int birds)
        {
            _defaultBirdAmount = birds;
            this.birds = birds;
        }

        public void OnRestartLevel()
        {
            birds = _defaultBirdAmount;
            score = 0;
        }

        public static void SaveGame(GameData data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/gamedata.dat");
            bf.Serialize(file, data);
            file.Close();
        }


        public static GameData LoadGameData()
        {
            if (File.Exists(Application.persistentDataPath + "/gamedata.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/gamedata.dat", FileMode.Open);
                GameData data = (GameData)bf.Deserialize(file);
                file.Close();
                return data;
            }
            return null;
        }
    }
}