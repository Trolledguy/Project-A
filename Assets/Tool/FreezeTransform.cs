using UnityEditor;
using UnityEngine;

public class FreezeTransform : MonoBehaviour
{
    [MenuItem("Tools/Freeze Transform (like Maya)")]
    static void FreezeSelected()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            var parent = new GameObject("TempParent").transform;
            parent.position = obj.transform.position;
            parent.rotation = obj.transform.rotation;
            parent.localScale = obj.transform.localScale;

            obj.transform.SetParent(parent);
            obj.transform.localScale = Vector3.one;
            obj.transform.SetParent(null);
            Object.DestroyImmediate(parent.gameObject);
        }
    }
}
