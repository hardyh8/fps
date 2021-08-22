using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    

    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject QuitPanel;
    public GameObject EndPanel;
    public List<Level> Levels;
    
    [HideInInspector] public InputManager input;
    [HideInInspector] public GameManager manager;
    [HideInInspector] public UIManager ui;
    [HideInInspector] public Level curlvl;
    
    private GameObject CurLevel;
    private int curLevelno = 1;
    
    #region Static Instance

    private static Manager instance;

    public static Manager Instance
    {
        get => instance;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        input = gameObject.AddComponent<InputManager>();
        manager = gameObject.AddComponent<GameManager>();
        ui = gameObject.GetComponent<UIManager>();
    }
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        curLevelno = ui.curLevel;
        LoadLevel();
    }

    private void LoadLevel()
    {
        if(CurLevel != null)
        {
            Destroy(CurLevel.gameObject);
            CurLevel = null;
            curlvl = null;
        }
        CurLevel = Instantiate(Levels[curLevelno - 1].gameObject, transform);
        curlvl = CurLevel.GetComponent<Level>();
    }

    public void LoadNextLevel()
    {
        if(CurLevel != null)
        {
            Destroy(CurLevel.gameObject);
            CurLevel = null;
            curlvl = null;
        }
        
        if(curLevelno == Levels.Count)
            return;
        
        CurLevel = Instantiate(Levels[curLevelno].gameObject, transform);
        curlvl = CurLevel.GetComponent<Level>();
        curLevelno++;
    }
}
