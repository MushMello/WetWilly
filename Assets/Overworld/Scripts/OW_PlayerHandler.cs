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
}
