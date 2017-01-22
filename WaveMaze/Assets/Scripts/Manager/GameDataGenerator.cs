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
                Storage.Save<GameData>(m_GameData, Defines.c_GameDataFile);
            }
        }

        public static GameData Load()
        {
            return Storage.Load<GameData>(Defines.c_GameDataFile);
        }
    }
}
