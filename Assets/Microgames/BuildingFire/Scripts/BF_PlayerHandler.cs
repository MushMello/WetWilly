using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// BF_PlayerHandler
// handles the player movement in Building Fire

public class BF_PlayerHandler : SidePlayerHandler
{
    [Header("BuildingFire Player Controls")]
    [SerializeField] private float waterDelaySeconds = 1f;

    [Header("BuildingFire References")]
    [SerializeField] private BF_GameHandler gameHandler;
    [SerializeField] private InputActionReference fireAction;
    [SerializeField] private GameObject waterObject;

    private bool hasUnpressedFire = true;

    private float waterDelay = 0f;
    private Coroutine waterDelayRoutine;

    protected override void Start()
    {
        base.Start();
        waterDelayRoutine = StartCoroutine(GetWaterTimer());
    }

    protected override void Update()
    {
        if(gameHandler)
        {
            if(!gameHandler.Running)
            {
                Locked = true;
            }
            
            if(!Locked && fireAction)
            {
                if(fireAction.action.WasPressedThisFrame())
                {
                    if(hasUnpressedFire)
                    {
                        if(waterDelay == 0f)
                        {
                            SpawnWater();
                            waterDelay = waterDelaySeconds;
                        }
                        hasUnpressedFire = false;
                    }
                }
                else
                {
                    hasUnpressedFire = true;
                }
                
            }
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

    private void SpawnWater()
    {
        GameObject waterClone = Instantiate(waterObject, transform);
        waterClone.transform.localPosition = Vector3.zero;
        BF_WaterHandler newWater = waterClone.GetComponent<BF_WaterHandler>();
        if(newWater)
        {
            newWater.RegisterSender(this);
        }
    }

    private IEnumerator GetWaterTimer()
    {
        while(true)
        {
            float seconds = Mathf.Min(1, waterDelay); // wait for 1 second, or less time if we have a smaller delay
            yield return new WaitForSeconds(seconds);
            waterDelay = Mathf.Max(0, waterDelay - seconds);
        }
    }

    private void OnDestroy()
    {
        if(waterDelayRoutine != null)
        {
            StopCoroutine(waterDelayRoutine);
        }
    }
}
