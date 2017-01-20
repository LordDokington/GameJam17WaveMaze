using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject _instructionsGO;
    // Use this for initialization
    void Start()
    {
       var instrGO = Resources.Load("Prefabs/InstructionObject") as GameObject;
        _instructionsGO = Instantiate(instrGO);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(_instructionsGO);
        }
    }
}
