using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGroundBehaviour : MonoBehaviour
{
    public SpriteRenderer GroundSpriteR;
    private Sprite[] _groundImgs = new Sprite[10];
    private int _indexOfImgs;
    private int _dmgCounter;
    private bool _isPlayer1OnGround;
    private bool _isPlayer2OnGround;

    // Use this for initialization
    void Start()
    {
        _groundImgs = Resources.LoadAll<Sprite>("GroundSprites");
        Debug.Log(_groundImgs.Length);
        _indexOfImgs = 0;
        GroundSpriteR.sprite = _groundImgs[_indexOfImgs];
    }

    // Update is called once per frame
    void Update()
    {

        if (_isPlayer1OnGround && _isPlayer2OnGround)
        {
            ++_dmgCounter;
        }
        if(_dmgCounter % 20 == 0 && _dmgCounter > 0)
        {
            ++_indexOfImgs;
            if (_indexOfImgs < _groundImgs.Length)
            {

                GroundSpriteR.sprite = _groundImgs[_indexOfImgs];
                if(_indexOfImgs == _groundImgs.Length-1)
                {
                    GameManager.Instance.KillPlayer(true, true);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter");
        if (other.gameObject.name == GameManager.Instance.Player1.name)
            _isPlayer1OnGround = true;
        if (other.gameObject.name == GameManager.Instance.Player2.name)
            _isPlayer2OnGround = true;
    }

    
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exits");
        if (other.gameObject.name == GameManager.Instance.Player1.name)
            _isPlayer1OnGround = false;
        if (other.gameObject.name == GameManager.Instance.Player2.name)
            _isPlayer2OnGround = false;
    }
    
}
