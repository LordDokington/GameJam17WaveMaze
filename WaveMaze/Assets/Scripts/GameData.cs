using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

[System.Serializable]
public class GameData{
	public List<string> m_InstructionList; 
	public List<LevelData> m_LevelDataList;

	public GameData()
	{
 
    }     
}

[System.Serializable]
public class LevelData{
	public Color m_EntryColor;
    public int m_EnemyCount;
}
