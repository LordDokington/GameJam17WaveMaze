using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameBehaviour : MonoBehaviour
{
    public GameObject TriggerArea;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameManager.Instance.IncreaseLevelNumber();
            GameManager.Instance.IncreaseLevelNumber();
            GameManager.Instance.KillPlayer(true, true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Trigger");
            GameManager.Instance.StartOutro();
        }
    }
}
