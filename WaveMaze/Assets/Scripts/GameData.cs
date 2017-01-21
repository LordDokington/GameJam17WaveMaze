using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;

[System.Serializable]
public class GameData{
	public List<string> m_InstructionList;
    public string Instruction1; 
	public List<LevelData> m_LevelDataList;

	public GameData()
	{
 
    }     
}

[System.Serializable]
public class LevelData{
    public int m_LevelNumber;
    public int m_EnemyCount;
    public bool HasEntry;
    public bool HasEnd;
    public Vector2 SpornPlayer1;
    public Vector2 SpornPlayer2;
}
