using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerTransform;

    private void Update()
    {
        // Move to player position
        transform.position = playerTransform.position;
    }
}
