using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    public Image Background;
    public GameObject SkipText;
    public Text TextStoryLine;
    private List<string> _story = new List<string>();
    private InstructionState _currentState;
    private float _timeBetweenTextMax = 1f;
    private float _timeBetweenTextCurrent;
    private float _fadingTimeMax = 1f;
    private float _fadingTimeCurrent;
    private float _timeShowTextLetter = 0.1f;
    private float _timeShowTextCurrent;
    private bool _isSkipTextShown;

	void Awake()
	{

	}

    // Use this for initialization
    void Start()
    {
        GameManager.Instance.FindPlayer();
        GameManager.Instance.SetInstrObj = gameObject;
        _currentState = InstructionState.Idle;
        _timeBetweenTextCurrent = _timeBetweenTextMax;
        _fadingTimeCurrent = _fadingTimeMax;        
        _story = new List<string> { "You are not alone", "You are not alone", "You are not alone" };//GameManager.Instance.GetGameData.m_InstructionList;
		HideOther();
	}

	void HideOther()
	{
		GameManager.Instance.Player1.SetActive (false);
        if(GameManager.Instance.Player2 != null)
            GameManager.Instance.Player2.SetActive(false);
        GameManager.Instance.ShouldCameraDarknessBeOn(false);
	}

	void ShowOther()
	{
        GameManager.Instance.Player1.SetActive(true);
        if (GameManager.Instance.Player2 != null)
            GameManager.Instance.Player2.SetActive(true);
        GameManager.Instance.ShouldCameraDarknessBeOn(true);
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

        if (GameManager.Instance.FinishPreloading)
        {
            if (!_isSkipTextShown)
            {
                _isSkipTextShown = true;
                SkipText.SetActive(true);
            }
            if(_isSkipTextShown && Input.GetKeyUp(KeyCode.Space))
                endInstructions();
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
                TextStoryLine.text = _story.FirstOrDefault();
                _story.RemoveAt(0);
            }
            else
            {
                //TODO: end instructions endInstructions();
            }
        }
    }

    private void fadingIn()
    {
        TextStoryLine.color = new Color(TextStoryLine.color.r, TextStoryLine.color.g, TextStoryLine.color.b, Mathf.Clamp((1f -_fadingTimeCurrent / _fadingTimeMax), 0f, 1f));
        _fadingTimeCurrent -= Time.deltaTime;

        if (_fadingTimeCurrent <= 0f)
        {
            _currentState = InstructionState.Show;
            _fadingTimeCurrent = _fadingTimeMax;
            _timeShowTextCurrent = TextStoryLine.text.Length * _timeShowTextLetter;
        }
    }

    private void showText()
    {
        _timeShowTextCurrent -= Time.deltaTime;
        if (_timeShowTextCurrent <= 0f)
        {
            _currentState = InstructionState.FadeOut;
            _timeShowTextCurrent = float.MaxValue;
        }
    }
    private void fadingOut()
    {
        TextStoryLine.color = new Color(TextStoryLine.color.r, TextStoryLine.color.g, TextStoryLine.color.b, Mathf.Clamp((_fadingTimeCurrent / _fadingTimeMax), 0f, 1f));

        _fadingTimeCurrent -= Time.deltaTime;
        if (_fadingTimeCurrent <= 0f)
        {
            _currentState = InstructionState.Idle;
            _fadingTimeCurrent = _fadingTimeMax;
        }
    }

    private void endInstructions()
    {
		ShowOther();
        gameObject.SetActive(false);
    }

    public void Init(bool IsOutro)
    {
        if(IsOutro)
        {
            _story = new List<string> { "You are not alone", "You are not alone", "You are not alone" };//GameManager.Instance.GetGameData.m_InstructionList;
            Background.sprite = Resources.Load<Sprite>("Assets/Resources/Textures/Turner.png");
        }
        else
        {
            _story = new List<string> { "You are not alone", "You are not alone", "You are not alone" };//GameManager.Instance.GetGameData.m_InstructionList;
            Background.sprite = Resources.Load<Sprite>("");
        }
        _currentState = InstructionState.Idle;
        _timeBetweenTextCurrent = _timeBetweenTextMax;
        _fadingTimeCurrent = _fadingTimeMax;
        HideOther();
    }
}
