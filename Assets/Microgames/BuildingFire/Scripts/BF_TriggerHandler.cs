using UnityEngine;


// BF_TriggerHandler
// handles the trigger in Building Fire 

public class BF_TriggerHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BF_GameHandler gameHandler;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        BF_FireHandler targetFire = collider.gameObject.GetComponent<BF_FireHandler>();
        if (gameHandler && targetFire)
        {
            gameHandler.Health--;
            targetFire.StartScattering();
        }
    }
}
