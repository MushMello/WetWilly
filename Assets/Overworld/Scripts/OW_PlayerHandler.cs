using UnityEngine;

public class OW_PlayerHandler : TopDownPlayerHandler
{
    [Header("Sprite Controls")]
    [SerializeField] private bool facingRight = true;
    [SerializeField] private bool flipSprite = true;
    
    private SpriteRenderer spriteRenderer;
    

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        OverworldHandler oh = OverworldHandler.GetOverworldHandler();
        if(oh)
        {
            if(oh.StoredPlayerPosition != Vector2.zero)
            {
                transform.position = oh.StoredPlayerPosition;
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if(spriteRenderer)
        {
            float xScalar = GetInputVector().x;
            spriteRenderer.flipX = flipSprite && ((facingRight && xScalar < 0) || (!facingRight && xScalar > 0));
        }
    }

    public Vector2 Position
    {
        get
        {
            return new Vector2(transform.position.x, transform.position.y);
        }
    }
}
