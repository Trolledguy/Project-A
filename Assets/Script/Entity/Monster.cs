using UnityEngine;

public abstract class Monster : Entity , ISprite2D
{
    protected SpriteLoader spLoader;
    
    public SpriteRenderer sprite
    {
        get
        {
            if (spLoader != null && spLoader.GetComponent<SpriteLoader>())
            {
                return spLoader.sRenderer;
            }
            else
            {
                Debug.LogWarning("Sprite Loader Fail");
                return null;
            }
        }
        
        set
        {
            if (value != null)
            {
                spLoader.sRenderer = value;
            }
        }
    }



    
    protected abstract void OnPlayerHit();
    protected override void Initialized()
    {
        base.Initialized();
        spLoader = GetComponentInChildren<SpriteLoader>();
    }
}
