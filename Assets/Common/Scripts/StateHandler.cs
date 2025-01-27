using UnityEngine;
using UnityEngine.SceneManagement;

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
            DontDestroyOnLoad(gameObject);
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
