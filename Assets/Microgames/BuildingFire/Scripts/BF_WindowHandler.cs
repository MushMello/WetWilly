using System.Collections;
using UnityEngine;

// BF_WindowHandler
// handles the fires that spawn from windows in Building Fire

public class BF_WindowHandler : MonoBehaviour
{
    [Header("Window Settings")]
    [SerializeField] private float spawnDelay = 3; // refers to the spawn rate of falling objects in seconds
    [SerializeField] private GameObject fireObject; // the object to fall from the window
    [Header("References")]
    [SerializeField] private BF_GameHandler gameHandler;

    private Transform[] spawnTransforms;
    private Coroutine spawnRoutine;
    

    private void Start()
    {
        spawnTransforms = new Transform[transform.childCount];
        int i = 0;
        foreach(Transform eachTransform in transform)
        {
            spawnTransforms[i++] = eachTransform;
        }
        spawnRoutine = StartCoroutine(GetSpawnRoutine());
    }

    private void SpawnFire()
    {
        GameObject newFire = Instantiate(fireObject, spawnTransforms[Random.Range(0, spawnTransforms.Length)]);
        newFire.transform.localPosition = Vector3.zero;
    }

    private IEnumerator GetSpawnRoutine()
    {
        while(gameHandler && gameHandler.Running)
        {
            SpawnFire();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public float SpawnDelay
    {
        get
        {
            return spawnDelay;
        }
        set
        {
            spawnDelay = value;
        }
    }

    public void EndSpawns()
    {
        if(spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }
    }
}
