using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionBehaviour : MonoBehaviour
{
    enum InstructionState
    {
        Idle,
        FadeIn,
        Show,
        FadeOut
    }

    private string _story;
    private InstructionState _currentState;
    private float _timeBetweenTextMax = 1f;
    private float _timeBetweenTextCurrent;
    private float _fadingTimeMax = 1f;
    private float _fadingTimeCurrent;
    //private float _timeShowTextMax = 1f;
    private float _timeShowTextCurrent;


    // Use this for initialization
    void Start()
    {

        _currentState = InstructionState.Idle;
        //TODO: Read Textfile and insert string
        _story = "You are not alone$There is always a light with you";
    }

    // Update is called once per frame
    void Update()
    {
        switch(_currentState)
        {
            case InstructionState.Idle:
                idling();
                break;
            case InstructionState.FadeIn:
                fadingIn();
                break;
            case InstructionState.Show:
                showText();
                break;
            case InstructionState.FadeOut:
                fadingOut();
                break;
        }
    }

    private void idling()
    {

    }

    private void fadingIn()
    {

    }

    private void showText()
    {

    }
    private void fadingOut()
    {

    }
}
