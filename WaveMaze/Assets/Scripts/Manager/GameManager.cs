using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    private bool _isInstructionMode;
    private GameObject _instructionsGO;

    public bool GetInstructionMode
    {
        get { return _isInstructionMode; }
    }

    // Use this for initialization
    void Start()
    {
       var instrGO = Resources.Load("Prefabs/InstructionObject") as GameObject;
        _instructionsGO = Instantiate(instrGO);
        _isInstructionMode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_isInstructionMode && Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(_instructionsGO);
            _isInstructionMode = false;
        }
    }
}
