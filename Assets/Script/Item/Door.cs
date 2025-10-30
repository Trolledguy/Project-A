using System.Collections;
using UnityEngine;

public class Door : PlayerRelate
{
    public bool isOpen = false;
    public bool isReverse;
    private bool isMoving;

    public void StartMove() { StartCoroutine(MoveObject()); }

    public override IEnumerator MoveObject()
    {
        if (isMoving) { yield return null; }
        float duration = 1f; // how long the move takes (seconds)
        float elapsed = 0f;

        Vector3 startPos = transform.position;
        Vector3 endPos;

        // Decide direction based on flags
        if (!isReverse)
        {
            endPos = isOpen ? startPos - transform.right : startPos + transform.right;
        }
        else
        {
            endPos = isOpen ? startPos + transform.right : startPos - transform.right;
        }

        // Smooth movement over time
        while (elapsed < duration)
        {
            isMoving = true;
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null; // wait for next frame
        }

        // snap to final position and toggle open state
        isMoving = false;
        transform.position = endPos;
        isOpen = !isOpen;
        
    }
}