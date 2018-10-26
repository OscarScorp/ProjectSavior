using UnityEngine;

[ExecuteInEditMode] //To also run code in Unity's editor.
public class IsometricSpriteRenderer : MonoBehaviour
{
    void Update()
    {
        GetComponent<Renderer>().sortingOrder = (int)(transform.position.y * -10);
    }
}