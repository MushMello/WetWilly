using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButtonHandler : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private int sceneBuildNumber;

    private Scene scene;
    private Button attachedButton;

    private void Start()
    {
        attachedButton = GetComponent<Button>();
        if (attachedButton)
        {
            attachedButton.onClick.AddListener(() => {
                SettingsHandler settings = SettingsHandler.GetSettingsHandler();
                if(settings)
                {
                    settings.CanPause = true;
                }
                SceneManager.LoadSceneAsync(sceneBuildNumber);
                });
        }
    }
}
