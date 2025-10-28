using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SignatureArea : MonoBehaviour
{
    private BoxCollider2D box;

    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    public bool IsInside(Vector2 pos)
    {
        return box.OverlapPoint(pos);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider2D>().size);
    }
#endif
}