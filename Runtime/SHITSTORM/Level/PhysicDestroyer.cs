using UnityEngine;

public class PhysicDestroyer : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Rigidbody rgb = other.attachedRigidbody;

        if (rgb)
            Destroy(rgb.gameObject);
    }
}