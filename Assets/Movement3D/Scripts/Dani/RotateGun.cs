using UnityEngine;

public class RotateGun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GrapplingGun grapplingGun;

    private Quaternion desiredRotation;

    private const float rotationSpeed = 5;

    private void Update()
    {
        // If grappling gun not grappling, set desired rotation
        if (!grapplingGun.IsGrappling()) desiredRotation = transform.parent.rotation;
        // If grappling, look at grapple point
        else desiredRotation = Quaternion.LookRotation(grapplingGun.GetGrapplePoint() - transform.position);

        // Lerp rotation to desired rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }

}
