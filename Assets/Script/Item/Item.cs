using UnityEngine;

public abstract class Item : Identity , ISprite2D , IInteractable
{
    public string itemID;

    protected SpriteLoader spLoader;
    public Sprite itemSprite;
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

    void Start()
    {
        base.Initialized();
    }

    //ความสามารถ Item
    public abstract void ObjectInteract(Player player);

    public virtual void Holding(Transform holdPos, float _time)
    {
        eRigi.isKinematic = true;
        transform.position = Vector3.Lerp(transform.position, holdPos.position, _time);
        icollider.isTrigger = true;
        transform.SetParent(holdPos);
    }

    public void Drop()
    {
        eRigi.isKinematic = false;
        icollider.isTrigger = false;
        transform.SetParent(null);
    }

}
