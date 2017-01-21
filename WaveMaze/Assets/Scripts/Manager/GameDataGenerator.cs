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
            setLevel1();
            setLevel2();
        }

        void setLevel1() {
            LevelData aLevel1 = new LevelData();
            aLevel1.m_LevelNumber = 1;
            aLevel1.m_EnemyCount = 0;
            aLevel1.HasEntry = false;
            aLevel1.HasEnd = false;
            aLevel1.SpornPlayer1 = new Vector2(100,100);
            aLevel1.SpornPlayer2 = new Vector2(100,100);
            aLevel1.EntryArray = new Vector3(0,0,0);
            aLevel1.ExitArray = new Vector3(10,300,10);
            m_GameData.m_LevelDataList.Add(aLevel1);
        }

        void setLevel2()
        {
            LevelData aLevel2 = new LevelData();
            aLevel2.m_LevelNumber = 2;
            aLevel2.m_EnemyCount = 0;
            aLevel2.HasEntry = true;
            aLevel2.HasEnd = false;
            aLevel2.SpornPlayer1 = new Vector2(100, 100);
            aLevel2.SpornPlayer2 = new Vector2(100, 100);
            aLevel2.EntryArray = new Vector3(10,300,10);
            aLevel2.ExitArray = new Vector3(10,300,10);
            m_GameData.m_LevelDataList.Add(aLevel2);
        }
    }
}
