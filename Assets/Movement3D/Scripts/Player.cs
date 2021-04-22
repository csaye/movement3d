using UnityEngine;

namespace Movement3D
{
    public class Player : MonoBehaviour
    {
        [Header("Attrubutes")]
        [SerializeField] private float mouseSpeed = 100;

        private Vector2 rotation = new Vector2();

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            Look();
        }

        private void Look()
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

            // Update and clamp x rotation
            rotation.x -= mouseY;
            rotation.x = Mathf.Clamp(rotation.x, -90, 90);

            // Rotate camera vertically
            cameraTransform.localRotation = Quaternion.Euler(rotation.x, 0, 0);

            // Rotate player horizontally
            rotation.y += mouseX;
            transform.localRotation = Quaternion.Euler(0, rotation.y, 0);
        }
    }
}
