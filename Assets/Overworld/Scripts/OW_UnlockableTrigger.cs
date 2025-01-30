using UnityEngine;

public class OW_UnlockableTrigger : OW_WarpTrigger
{
    [Header("Unlock Settings")]
    [SerializeField] private string[] dependentWarpIDs;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(Unlocked)
        {
            base.OnTriggerEnter2D(collision);
        }
    }

    private bool Unlocked
    {
        get
        {
            bool result = false;
            OverworldHandler oh = OverworldHandler.GetOverworldHandler();
            if(oh)
            {
                foreach (string eachString in dependentWarpIDs)
                {
                    result = result || !oh.HasWarpState(eachString) || oh.GetWarpState(eachString);
                }
                result = !result;
            }
            return result;
        }
    }
}
