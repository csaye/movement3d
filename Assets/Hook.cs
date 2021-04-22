using UnityEngine;

namespace Movement3D
{
    public class Hook : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private float maxHookDist = 32;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform player;

        private SpringJoint springJoint;

        private Vector3 hitPoint;

        private void Update()
        {
            // Toggle hook
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (springJoint != null)
                {
                    // Destroy spring joint and clear line renderer
                    Destroy(springJoint);
                    lineRenderer.positionCount = 0;
                }
                else
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, transform.forward, out hit, maxHookDist, groundMask))
                    {
                        hitPoint = hit.point;

                        // Initialize spring joint
                        springJoint = player.gameObject.AddComponent<SpringJoint>();
                        springJoint.autoConfigureConnectedAnchor = false;
                        springJoint.connectedAnchor = hitPoint;

                        float hookDist = Vector3.Distance(player.position, hitPoint);

                        // springJoint.maxDistance = hookDist * 0.8f;
                        // springJoint.minDistance = hookDist * 0.25f;

                        springJoint.spring = 10;
                        springJoint.damper = 7;
                        springJoint.massScale = 4.5f;

                        // Set up line renderer
                        lineRenderer.positionCount = 2;
                        lineRenderer.SetPosition(0, player.position);
                        lineRenderer.SetPosition(1, hitPoint);
                    }
                }
            }

            // Render hook
            if (lineRenderer.positionCount == 2)
            {
                lineRenderer.SetPosition(0, player.position);
                lineRenderer.SetPosition(1, hitPoint);
            }
        }
    }
}
