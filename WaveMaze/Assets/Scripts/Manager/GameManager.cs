using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.Manager;
using System.IO;

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

    private GameObject _instructionObject;
    private GameObject _player1;
    private GameObject _player2;
    private GameData m_GameData;

    bool SpawnPlayerAfterGameInit = false;

    public GameData GetGameData
    {
        get
        {
            return m_GameData;
        }
    }

    public GameObject Player1
    {
        get { return _player1 == null ? null: _player1; }
    }

    public GameObject Player2
    {
        get { return _player2 == null ? null : _player2; }
    }

    public GameObject SetInstrObj
    {
        set { _instructionObject = value; }
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
        aGameDataGenerator.Save( !File.Exists(Defines.c_GameDataFile) );
#endif
        FinishPreloading = false;

        m_GameData = GameDataGenerator.Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnPlayerAfterGameInit) {
            Player1.SetActive(true);
            Player1.GetComponent<PlayerController>().spornPlayerOne();
            SpawnPlayerAfterGameInit = false;
        }
    }


    public void FindPlayer()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        _player1 = players[0];
        if(players.Length > 1)
            _player2 = players[1];
    }

    public void StartOutro()
    {
        _instructionObject.SetActive(true);
        _instructionObject.GetComponent<InstructionBehaviour>().Init(true);
    }

    public void ShouldCameraDarknessBeOn(bool state)
    {
        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        mainCamera.GetComponent<WaveMaze.DarknessEffect>().enabled = state;
    }
}
