using UnityEngine;

public class GameController : MonoBehaviour
{
    int count = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SpawnObject(GameObject prefab) {
        Instantiate(prefab, new Vector3(-7.93f, 4.24f, 0), Quaternion.identity);
    }

    public void BallDestroyed(GameObject prefab) {
        if (count >= 3) {
            SpawnObject(prefab);
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
