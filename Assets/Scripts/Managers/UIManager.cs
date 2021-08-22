using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int curLevel = 1;
    public Animator vingetteEfffect;
    public GameObject SettingPanel;
    public GameObject LevelPanel;
    public RawImage SoundToggler;
    // 1 on 0 off
    public List<Sprite> SoundSprites;

    private GameObject EndPanel;
    private GameObject winPanel;
    private GameObject losePanel;
    private GameObject quitPanel;
    private Manager manager;
    private GameManager gm;
    private bool isWin;
    private bool isSound = true;

    // Start is called before the first frame update
    void Start()
    {
        manager = Manager.Instance;
        if(manager == null)
            return;
        winPanel = manager.WinPanel;
        losePanel = manager.LosePanel;
        quitPanel = manager.QuitPanel;
        EndPanel = manager.EndPanel;
        gm = manager.manager;
    }

    public void ShowSetting()
    {
        SettingPanel.SetActive(true);
    }

    public void SoundToggle()
    {
        isSound = !isSound;
        if (isSound)
            SoundToggler.texture = SoundSprites[1].texture;
        else
            SoundToggler.texture = SoundSprites[0].texture; 
    }
    
    public void HideSetting()
    {
        SettingPanel.GetComponent<Animator>().Play("ClosePanel");
    }

    public void ShowLevels()
    {
        LevelPanel.SetActive(true);
    }

    public void HideLevels()
    {
        LevelPanel.GetComponent<Animator>().Play("ClosePanel");
    }

    public void LoadLevel(int no)
    {
        curLevel = no;
        vingetteEfffect.Play("Vignette");
    }

    public void ShowEndPanel(bool isWin)
    {
        EndPanel.SetActive(true);
        this.isWin = isWin;
        if (isWin)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void FirstQuit()
    {
        quitPanel.SetActive(true);
    }

    public void NextLevel()
    {
        manager.LoadNextLevel();
    }

    public void Retry()
    {
        EndPanel.SetActive(false);
        losePanel.GetComponent<Animator>().Play("ClosePanel");
        gm.Retry();
    }

    public void HideQuitPanel()
    {
        ShowEndPanel(isWin);
    }
}
