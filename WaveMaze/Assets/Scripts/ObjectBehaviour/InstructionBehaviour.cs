using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstructionBehaviour : MonoBehaviour
{
    enum InstructionState
    {
        Idle,
        FadeIn,
        Show,
        FadeOut
    }

    private List<string> _story = new List<string>();
    private string _currentText;
    private InstructionState _currentState;
    private float _timeBetweenTextMax = 1f;
    private float _timeBetweenTextCurrent;
    private float _fadingTimeMax = 1f;
    private float _fadingTimeCurrent;
    private float _timeShowTextLetter = 1f;
    private float _timeShowTextCurrent;


    // Use this for initialization
    void Start()
    {

        _currentState = InstructionState.Idle;
        //TODO: Read Textfile and insert string
        _story = ("You are not alone$There is always a light with you").Split('$').ToList();
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
        _timeBetweenTextCurrent -= Time.deltaTime;
        if (_timeBetweenTextCurrent <= 0f)
        {
            _currentState = InstructionState.FadeIn;
            _timeBetweenTextCurrent = _timeBetweenTextMax;
            if (_story.Count > 0)
            {
                _currentText = "";
            }
        }
    }

    private void fadingIn()
    {
        _fadingTimeCurrent -= Time.deltaTime;
        if (_fadingTimeCurrent <= 0f)
        {
            _currentState = InstructionState.Show;
            _fadingTimeCurrent = _fadingTimeMax;
            //_timeShowTextCurrent = 
        }
    }

    private void showText()
    {
        _timeShowTextCurrent -= Time.deltaTime;
        if (_timeShowTextCurrent <= 0f)
        {
            _currentState = InstructionState.Show;
            _timeShowTextCurrent = float.MaxValue;
        }
    }
    private void fadingOut()
    {
        _fadingTimeCurrent -= Time.deltaTime;
        if (_fadingTimeCurrent <= 0f)
        {
            _currentState = InstructionState.Idle;
            _fadingTimeCurrent = _fadingTimeMax;
        }
    }
}
