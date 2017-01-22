using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class InstructionBehaviour : MonoBehaviour
{
    enum InstructionState
    {
        Idle,
        FadeIn,
        Show,
        FadeOut
    }

    private List<string> _introStrings = new List<string>();
    private List<string> _outroStrings = new List<string>();
    public GameObject[] SpawnPoints;
    public Image Background;
    public Text TextStoryLine;
    public AudioSource Audiosrc;
    private List<string> _story = new List<string>();
    private InstructionState _currentState;
    private bool _endGameAfterText;
    private float _timeBetweenTextMax = 1f;
    private float _timeBetweenTextCurrent;
    private float _fadingTimeMax = 1f;
    private float _fadingTimeCurrent;
    private float _timeShowTextLetter = 0.1f;
    private float _timeShowTextCurrent;

	void Awake()
	{

	}

    // Use this for initialization
    void Start()
    {
        GameManager.Instance.FindPlayer();
        GameManager.Instance.SetInstrObj = gameObject;
        LoadInstructionStrings();
        Init(false);
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
		if (Input.GetKeyUp( KeyCode.Space) )
		{
			endInstructions ();
		}

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
                TextStoryLine.text = _story.FirstOrDefault();
                _story.RemoveAt(0);
            }
            else
            {
               endInstructions();
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
        Audiosrc.Stop();
        if (!_endGameAfterText)
        {
            GameManager.Instance.StartBGM();
            GameManager.Instance.CurrentState = GameManager.GameState.Game;
            GameManager.Instance.SetSpawnPoints(SpawnPoints);
            GameObject.Find("Description").GetComponent<DescriptionBehaviour>().SetLightStatus(true);
            ShowOther();
            gameObject.SetActive(false);


        }
        else
        {
            SceneManager.LoadScene("Credits"); 
        }
    }

    public void Init(bool IsOutro)
    {
        if(IsOutro)
        {
            _story = _outroStrings;
            Background.sprite = Resources.Load<Sprite>("Sprites/outro_bg");
            _endGameAfterText = true;
        }
        else
        {
            _story = _introStrings; 
            Background.sprite = Resources.Load<Sprite>("Sprites/intro_bg");
            _endGameAfterText = false;
        }
        GameManager.Instance.CurrentState = GameManager.GameState.Introduction;
        TextStoryLine.text = "";
        Audiosrc.Play();
        _currentState = InstructionState.Idle;
        _timeBetweenTextCurrent = _timeBetweenTextMax;
        _fadingTimeCurrent = _fadingTimeMax;
        HideOther();
    }

    private void LoadInstructionStrings()
    {
        _introStrings.Add("Einst hülltet ihr das Land in Licht und Wärme.");
        _introStrings.Add("Die Sonne glitzerte und das Grün der Bäume strahlte um die Wette");
        _introStrings.Add("Doch dann kam das Dunkel");
        _introStrings.Add("Es raubte die Sicht und seit dem Tag herrscht Kälte");
        _introStrings.Add("Und verbannte euch in die Verliese unter dem Berg...");
        _outroStrings.Add("Ihr habt es geschafft!");
        _outroStrings.Add("Ihr seid den Bluthunden des Dunkels tatsächlich entwischt!");
        _outroStrings.Add("Erleuchtet und das Land soll nie wieder in Dunkelheit versinken!");
    }
}
