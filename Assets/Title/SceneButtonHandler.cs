using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButtonHandler : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private int sceneBuildNumber;
    [SerializeField] private bool allowPauseAfterLoad = true;

    private Scene scene;
    private Button attachedButton;
    

    protected virtual void Start()
    {
        attachedButton = GetComponent<Button>();
        if (attachedButton)
        {
            attachedButton.onClick.AddListener(() => {
                SceneManager.LoadSceneAsync(sceneBuildNumber);
                SettingsHandler settings = SettingsHandler.GetSettingsHandler();
                if(settings)
                {
                    settings.CanPause = allowPauseAfterLoad;
                }
            });
        }
    }
}
