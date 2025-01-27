using UnityEngine;
using UnityEngine.UI;

public class SceneButtonHandler : MonoBehaviour
{
    [Header("Button Settings")]
    [SerializeField] private GameScene targetScene;
    [SerializeField] private bool allowPauseAfterLoad = true;

    private Button attachedButton;
    

    protected virtual void Start()
    {
        attachedButton = GetComponent<Button>();
        if (attachedButton)
        {
            attachedButton.onClick.AddListener(() => {
                StateHandler state = StateHandler.GetStateHandler();
                if(state)
                {
                    state.WarpToScene(targetScene, allowPauseAfterLoad);
                }
            });
        }
    }
}
