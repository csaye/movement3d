using UnityEngine;

namespace Movement3D
{
    public class Player : MonoBehaviour
    {
        [Header("Attrubutes")]
        [SerializeField] private float moveSpeed = 100;
        [SerializeField] private float mouseSpeed = 100;
        [SerializeField] private float gravityForce = 1000;

        [Header("References")]
        [SerializeField] private Rigidbody rb;

        private Vector2 rotation = new Vector2();

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            Move();
            Look();
        }


        private void Move()
        {
            // Gravity
            rb.AddForce(Vector3.down * gravityForce * Time.deltaTime);

            // Get horizontal and vertical input
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // Calculate rigidbody velocity based on input
            Vector3 velocity = (transform.right * x + transform.forward * z) * moveSpeed;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
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
