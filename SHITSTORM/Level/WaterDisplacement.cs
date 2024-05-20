using UnityEngine;

public class WaterDisplacement : MonoBehaviour
{
    Material mat;
    public string id1;
    public string id2;
    public Vector2 vlc1 = new Vector2(2, 0);
    public Vector2 vlc2 = new Vector2(0, 1);

    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        if (id1 != "")
            mat.SetTextureOffset(id1, Time.time * vlc1);

        if (id2 != "")
            mat.SetTextureOffset(id2, Time.time * vlc2);
    }
}