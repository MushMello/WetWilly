using UnityEngine;


// BF_TriggerHandler
// handles the trigger in Building Fire 

public class BF_TriggerHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private BF_GameHandler gameHandler;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(gameHandler && collider.gameObject.GetComponent<BF_FireHandler>())
        {
            gameHandler.Health--;
            Destroy(collider);
        }
    }
}
