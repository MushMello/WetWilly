using System.Collections;
using UnityEngine;

public class OW_WarpTrigger : MonoBehaviour
{
    [Header("Warp Settings")]
    [SerializeField] private bool disableOnUse = true;
    [SerializeField] private string warpName;
    [Header("References")]
    [SerializeField] private GameScene targetScene;


    private bool warpEnabled;

    protected virtual void Start()
    {
        OverworldHandler oh = OverworldHandler.GetOverworldHandler();
        if(oh)
        {
            if(oh.HasWarpState(warpName))
            {
                warpEnabled = oh.GetWarpState(warpName);
            }
            else
            {
                warpEnabled = true;
                oh.SetWarpState(warpName, warpEnabled);
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        OW_PlayerHandler targetPlayer = collision.GetComponent<OW_PlayerHandler>();
        StateHandler state = StateHandler.GetStateHandler();
        if (targetPlayer && state && warpEnabled)
        {
            if (disableOnUse)
            {
                WarpEnabled = false;
            }
            OverworldHandler oh = OverworldHandler.GetOverworldHandler();
            if(oh)
            {
                oh.StoredPlayerPosition = targetPlayer.Position;
            }
            state.WarpToScene(targetScene, true);
        }
    }

    public bool WarpEnabled
    {
        get
        {
            return warpEnabled;
        }
        set
        {
            warpEnabled = value;
            OverworldHandler oh = OverworldHandler.GetOverworldHandler();
            if(oh)
            {
                oh.SetWarpState(warpName, warpEnabled);
            }
        }
    }
}
