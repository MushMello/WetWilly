using UnityEngine;

// BF_FireHandler
// handles the fires that fall from the building in Building Fire

public class BF_FireHandler : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        BF_PlayerHandler targetPlayer = collider.gameObject.GetComponent<BF_PlayerHandler>();
        if(targetPlayer)
        {
            targetPlayer.RegisterSave();
            Destroy(gameObject);
        }
    }
}
