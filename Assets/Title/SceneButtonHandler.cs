using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneButtonHandler : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private SceneAsset targetScene;

    private Button attachedButton;

    private void Start()
    {
        if (targetScene != null)
        {
            attachedButton = GetComponent<Button>();
            if(attachedButton)
            {
                attachedButton.onClick.AddListener(() => {
                    SceneManager.LoadSceneAsync(targetScene.name);
                });
            }
        }
    }
}
