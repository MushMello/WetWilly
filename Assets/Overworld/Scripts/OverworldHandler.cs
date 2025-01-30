using System.Collections.Generic;
using UnityEngine;

public class OverworldHandler : MonoBehaviour
{
    private static OverworldHandler overworldHandler;

    private Dictionary<string, bool> warpDictionary = new Dictionary<string, bool>();

    [Header("Instance Settings")]
    [SerializeField] private OW_PlayerHandler player;
    
    
    private Vector2 storedPlayerPosition = Vector2.zero;

    private void Awake()
    {
        if(!overworldHandler)
        {
            DontDestroyOnLoad(gameObject);
            overworldHandler = this;
        }
        else
        {
            if(player)
            {
                player.transform.localPosition = new Vector3(storedPlayerPosition.x, storedPlayerPosition.y, 0);
            }
        }
    }

    public static OverworldHandler GetOverworldHandler()
    {
        return overworldHandler;
    }

    public bool GetWarpState(string warpName)
    {
        if(warpDictionary.ContainsKey(warpName))
        {
            return warpDictionary[warpName];
        }
        return false;
    }

    public bool HasWarpState(string warpName)
    {
        return warpDictionary.ContainsKey(warpName);
    }

    public void SetWarpState(string warpName, bool warpState)
    {
        warpDictionary[warpName] = warpState;
    }

    public Vector2 StoredPlayerPosition
    {
        get
        {
            return storedPlayerPosition;
        }
        set
        {
            storedPlayerPosition = value;
        }
    }
}
