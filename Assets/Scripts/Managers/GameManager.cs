using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isGameEnded;
    private bool isWin;

    public bool IsGameEnded
    {
        get => isGameEnded;
    }

    private UIManager ui;
    private Manager manager;
    
    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.Instance;
        ui = manager.ui;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameWin()
    {
        isWin = true;
        GameEnded();
    }

    public void GameLose()
    {
        isWin = false;
        GameEnded();
    }

    public void GameEnded()
    {
        isGameEnded = true;
        ui.ShowEndPanel(isWin);
    }

    public void Retry()
    {
        // reset player
        Player.Instance.Reset();
        //reset enemies
        manager.curlvl.Reset();
        //reset bool
        isGameEnded = false;
    }
}
