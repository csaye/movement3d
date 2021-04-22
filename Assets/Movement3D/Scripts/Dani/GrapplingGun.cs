using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private LayerMask grappleableMask;
    [SerializeField] private Transform gunTipTransform, camTransform, playerTransform;

    private Vector3 grapplePoint, currentGrapplePos;
    private float maxGrappleDist = 100;
    private SpringJoint joint;

    private void Update()
    {
        // If left click, start grapple
        if (Input.GetMouseButtonDown(0)) StartGrapple();
        // If left click up, stop grapple
        else if (Input.GetMouseButtonUp(0)) StopGrapple();
    }

    private void LateUpdate()
    {
        // Draw rope
        DrawRope();    
    }

    // Starts a grapple
    private void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, maxGrappleDist, grappleableMask))
        {
            // Initialize spring joint
            grapplePoint = hit.point;
            joint = playerTransform.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(playerTransform.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            joint.spring = 4.5f;
            joint.damper = 7;
            joint.massScale = 4.5f;

            // Initialize line renderer
            lineRenderer.positionCount = 2;
            currentGrapplePos = gunTipTransform.position;
        }
    }

    // Stops a grapple
    private void StopGrapple()
    {
        // Destroy line and joint
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }
    
    // Draws rope with line renderer
    private void DrawRope()
    {
        // If no spring joint, do not draw rope
        if (!joint) return;

        currentGrapplePos = Vector3.Lerp(currentGrapplePos, grapplePoint, Time.deltaTime * 8);
        
        lineRenderer.SetPosition(0, gunTipTransform.position);
        lineRenderer.SetPosition(1, currentGrapplePos);
    }

    public bool IsGrappling() => joint != null;

    public Vector3 GetGrapplePoint() => grapplePoint;
}
