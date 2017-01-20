using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public bool FinishLoading;

    public override void AwakeSingleton()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start()
    {
        FinishLoading = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
