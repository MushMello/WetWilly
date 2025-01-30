using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum GameScene
{
    Title = 0,
    ExtinguishFire = 1,
    BuildingFire = 2,
    Asteroids = 3,
    Overworld = 4,
    Pong = 5
};

public class StateHandler : MonoBehaviour
{
    private static StateHandler stateHandler;

    [Header("StateHandler Settings")] 
    [SerializeField] private float announcementDelay = 2f;

    private int points = 0;
    private GameScene currentScene = GameScene.Title;
    private bool announcementRunning = false;

    public static StateHandler GetStateHandler()
    {
        return stateHandler;
    }

    private void Awake()
    {
        if(!stateHandler)
        {
            stateHandler = this;
            SetScoreLabel();
            DontDestroyOnLoad(gameObject);
        }
    }

    private void SetScoreLabel()
    {
        DisplayScoreValue(Points);
    }

    public void DisplayScoreValue(int score)
    {
        GameObject stateCanvas = transform.Find("StateCanvas").gameObject;
        if (stateCanvas)
        {
            GameObject statePanel = stateCanvas.transform.Find("StatePanel").gameObject;
            if (statePanel)
            {
                GameObject scoreLblObj = statePanel.transform.Find("ScoreLabel").gameObject;
                if (scoreLblObj)
                {
                    Text scoreLabel = scoreLblObj.GetComponent<Text>();
                    if (scoreLabel)
                    {
                        scoreLabel.text = string.Format("Score: {0:D}", score);
                    }
                }
            }
        }
    }

    public int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
            SetScoreLabel();
        }
    }

    public void WarpToScene(GameScene targetScene, bool allowPause)
    {
        currentScene = targetScene;
        SettingsHandler settings = SettingsHandler.GetSettingsHandler();
        if(settings)
        {
            settings.CanPause = allowPause;
        }
        SceneManager.LoadSceneAsync((int)targetScene);
    }

    public GameScene CurrentScene
    {
        get
        {
            return currentScene;
        }
    }

    private GameObject StatePanel
    {
        get
        {
            GameObject stateCanvas = transform.Find("StateCanvas").gameObject;
            if (stateCanvas)
            {
                GameObject statePanel = stateCanvas.transform.Find("StatePanel").gameObject;
                return statePanel;
            }
            return null;
        }
    }

    private void ToggleWinLabel()
    {
        GameObject statePanel = StatePanel;
        if (statePanel)
        {
            GameObject winLblObj = statePanel.transform.Find("WinLabel").gameObject;
            if (winLblObj)
            {
                winLblObj.SetActive(!winLblObj.activeSelf);
            }
        }
    }

    private void ToggleLoseLabel()
    {
        GameObject statePanel = StatePanel;
        if (statePanel)
        {
            GameObject loseLblObj = statePanel.transform.Find("LoseLabel").gameObject;
            if (loseLblObj)
            {
                loseLblObj.SetActive(!loseLblObj.activeSelf);
            }
        }
    }

    private IEnumerator GetAnnouncementRoutine(bool won, GameScene targetScene, bool allowPause)
    {
        announcementRunning = true;
        if(won)
        {
            ToggleWinLabel();
        }
        else
        {
            ToggleLoseLabel();
        }
        yield return new WaitForSeconds(announcementDelay);
        if (won)
        {
            ToggleWinLabel();
        }
        else
        {
            ToggleLoseLabel();
        }
        WarpToScene(targetScene, allowPause);
        announcementRunning = false;
    }

    public void DisplayAnnouncementAndWarp(bool wonGame, GameScene targetScene, bool allowPause)
    {
        if(!announcementRunning)
        {
            StartCoroutine(GetAnnouncementRoutine(wonGame, targetScene, allowPause));
        }
    }
}
