using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    public float scrollSpeed = 1f;

    private Material mat;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        Vector2 offset = mat.GetTextureOffset("_BaseMap");
        offset.x += scrollSpeed / 10 * Time.deltaTime;
        mat.SetTextureOffset("_BaseMap", offset);
    }
}
