using UnityEngine;

public class DeleteBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Destroy(col.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
