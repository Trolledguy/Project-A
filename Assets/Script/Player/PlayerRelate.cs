using System.Collections;
using UnityEngine;

public abstract class PlayerRelate : Identity
{
    public virtual IEnumerator MoveObject() { yield return null; }
}
