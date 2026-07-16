using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;

    private float currentSpeed;

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            if (AudioManager.Instance != null)
                AudioManager.Instance.StopFootsteps();

            return;
        }
        // Sprint
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (isSprinting)
            currentSpeed = sprintSpeed;
        else
            currentSpeed = walkSpeed;

        // Movement Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move =
            transform.right * x +
            transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);

        // -----------------------------
        // Footstep Sound
        // -----------------------------
        bool isMoving = controller.velocity.magnitude > 0.1f;

        if (AudioManager.Instance != null)
        {
            if (isMoving)
            {
                if (isSprinting)
                    AudioManager.Instance.StartFootsteps(1.3f);
                else
                    AudioManager.Instance.StartFootsteps(1f);
            }
            else
            {
                AudioManager.Instance.StopFootsteps();
            }
        }
    }
}