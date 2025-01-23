using UnityEngine;
using UnityEngine.InputSystem;

// BF_PlayerHandler
// handles the player movement in Building Fire

public class BF_PlayerHandler : SidePlayerHandler
{
    [Header("References")]
    [SerializeField] private BF_GameHandler gameHandler;


    protected override void Update()
    {
        if(gameHandler && !gameHandler.Running)
        {
            Locked = true;
        }

        base.Update();
    }

    public void RegisterSave()
    {
        if(gameHandler)
        {
            gameHandler.Points++;
        }
    }
}
