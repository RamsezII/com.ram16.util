using UnityEngine;

public class Turner : MonoBehaviour
{
    public Vector3 angularVelocity = new Vector3(0,180,0);

    private void Update()
    {
        transform.Rotate(Time.deltaTime * angularVelocity, Space.World);
    }
}