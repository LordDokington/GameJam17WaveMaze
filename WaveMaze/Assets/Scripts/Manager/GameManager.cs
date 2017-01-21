using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Manager;

public class GameManager : SingletonBehaviour<GameManager>
{
    public enum GameState
    {
        Menu,
        Introduction,
        Game,
        Credits
    }

    public GameState CurrentState;
    public bool FinishPreloading;
    private GameData m_GameData;

    public GameData GetGameData
    {
        get
        {
            return m_GameData;
        }
    }

    public override void AwakeSingleton()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
#if UNITY_EDITOR
        GameDataGenerator aGameDataGenerator = new GameDataGenerator();
        aGameDataGenerator.Save();
#endif
        FinishPreloading = false;

        m_GameData = GameDataGenerator.Load();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
