using UnityEngine;

// BF_GameHandler
// handles the overarching game logic in Building Fire

public class BF_GameHandler : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private int startingHealth = 3;

    private bool isRunning = true;
    private int points = 0;
    private int health;

    private void Start()
    {
        health = startingHealth;
    }

    public bool Running
    {
        get
        {
            return isRunning;
        }
        set
        {
            isRunning = value;
        }
    }

    public int Points
    {
        get
        {
            return points;
        }
        set
        {
            points = value;
        }
    }

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(health <= 0)
            {
                Running = false;
            }
        }
    }
}
