using System.Collections;
using UnityEngine;

public class BF_WaterHandler : MonoBehaviour
{
    private BF_PlayerHandler sender;
    private Coroutine despawnRoutine;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BF_FireHandler>())
        {
            if(sender)
            {
                sender.RegisterSave();
            }
            Destroy(collision.gameObject);
            if(despawnRoutine != null)
            {
                StopCoroutine(despawnRoutine);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator GetDisposalRoutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    public void RegisterSender(BF_PlayerHandler sender)
    {
        this.sender = sender;
        despawnRoutine = StartCoroutine(GetDisposalRoutine(10));
    }
}
