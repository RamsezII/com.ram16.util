using UnityEngine;

public class GetGroundSlope : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("~@ Slope Raycast @~")]
    [SerializeField] LayerMask mask = 1;
    [SerializeField] QueryTriggerInteraction trigger;

    [Space(15)]
    [SerializeField] bool contact, raycast;
    [Range(-1, 1)] [SerializeField] float dot;
    [Range(0, 180)] [SerializeField] float angle;

    //----------------------------------------------------------------------------------------------------------

    [ContextMenu(nameof(OnValidate))]
    private void OnValidate()
    {
        if (raycast)
        {
            RayCast();
            raycast = false;
        }
    }

    //----------------------------------------------------------------------------------------------------------

    [ContextMenu(nameof(RayCast))]
    void RayCast()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + .1f * Vector3.up, Vector3.down);

        if (Physics.Raycast(ray, out hit, .2f, mask, trigger))
        {
            dot = Vector3.Dot(Vector3.up, hit.normal);
            angle = 180 * Mathf.InverseLerp(1, -1, dot);

            contact = true;
        }
        else
        {
            dot = angle = 0;
            contact = false;
        }
    }
#endif
}