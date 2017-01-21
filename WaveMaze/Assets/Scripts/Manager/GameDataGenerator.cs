using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    class GameDataGenerator
    {
        GameData m_GameData;

        public GameDataGenerator()
        {
           
        }

        public void Save(bool TheOverride = true)
        {
            if ( TheOverride )
            {
                m_GameData = new GameData();
                WriteInstructions();
                WriteLevelData();
                Storage.Save<GameData>(m_GameData, Defines.c_GameDataFile);
            }
        }

        public static GameData Load()
        {
            return Storage.Load<GameData>(Defines.c_GameDataFile);
        }

        void WriteInstructions()
        {
            m_GameData.m_InstructionList = new List<string>{ "Test" };
        }

        void WriteLevelData()
        {
                m_GameData.m_LevelDataList = new List<LevelData>();
                LevelData aLevel1 = new LevelData();
                aLevel1.m_EntryColor = Color.red;
                aLevel1.m_EnemyCount = 1;
                m_GameData.m_LevelDataList.Add(aLevel1);
        }
    }
}
