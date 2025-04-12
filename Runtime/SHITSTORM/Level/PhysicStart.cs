using UnityEngine;

public class PhysicStart : MonoBehaviour
{
    private void Awake()
    {
        foreach (Rigidbody rgb in GetComponentsInChildren<Rigidbody>())
            rgb.Sleep();

        Destroy(this);
    }
}