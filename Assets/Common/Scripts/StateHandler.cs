using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum GameScene
{
    Title = 0,
    ExtinguishFire = 1,
    BuildingFire = 2,
    Asteroids = 3
};

public class StateHandler : MonoBehaviour
{
    private static StateHandler stateHandler;


    private int points = 0;
    private GameScene currentScene = GameScene.Title;

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
}
