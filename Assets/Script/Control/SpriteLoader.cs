using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class SpriteLoader : MonoBehaviour
{
    private Identity entity;
    public SpriteRenderer sRenderer;
    public Animator anim;

    void Start()
    {
        SetUp();
    }

    void FixedUpdate()
    {
        Animate();
    }

    private void Animate()
    {
        transform.LookAt(Camera.main.transform);
        AnimateControl(entity.GetPlayerPosition());
    }

    public void AnimateControl(Vector2 _position)
    {
        anim.SetFloat("Pos X", _position.x);
        anim.SetFloat("Pos Z", _position.y);
    }

    private void SetUp()
    {
        entity = GetComponentInParent<Identity>();
        sRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
}
