using UnityEngine;

public class AS_PlayerHandler : TopDownPlayerHandler
{

    private AS_GameHandler gameHandler;


    public void RegisterGameHandler(AS_GameHandler gameHandler)
    {
        this.gameHandler = gameHandler;
    }

    public void Hit()
    {
        if(gameHandler)
        {
            gameHandler.RegisterDamage();
        }
    }

    public void Score(int points)
    {
        if(gameHandler)
        {
            gameHandler.Points += points;
        }
    }
}
