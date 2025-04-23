using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public float dashStrength = 2;
    public float dashCooldown = 0.5f;
    public float dashDuration = 0.3f;
    public float dashIFrames = 0.2f;
    public event System.Action Dashed;

    public bool canDash;
    public bool isDashing;

    private Vector3 dashStartPos;
    private Vector3 moveDirection;

    void Awake()
    {
        // Get rigidbody
        playerRigidbody = GetComponent<Rigidbody>();

        canDash = true;
        isDashing = false;
    }

    void LateUpdate()
    {
        // Dash when the Dash button is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ActivateDash();   
        }
    }

    private void ActivateDash()
    {
        if (canDash && !isDashing)
        {
            isDashing = true;
            dashStartPos = transform.position;
            moveDirection = GetDashDirection();

            if (moveDirection == Vector3.zero) // if stationary, dash forward
            {
                StartCoroutine(DashCoroutine(transform.forward));
            }
            else 
            {
                StartCoroutine(DashCoroutine(moveDirection));
            }

            GetComponent<AudioSource>().PlayOneShot(AudioManager.Instance.playerSounds[2]);
        }
    }

    private IEnumerator DashCoroutine(Vector3 dashDirection)
    {
        float currTime = 0f;
        Vector3 initVelocity = playerRigidbody.velocity;

        GetComponent<CapsuleCollider>().enabled = false;

        while (currTime < dashDuration)
        {
            float dashProgress = currTime / dashDuration;
            dashProgress = Mathf.SmoothStep(0f, 1f, dashProgress);

            Vector3 newPosition = Vector3.Lerp(dashStartPos, dashStartPos + dashDirection * 10f * dashStrength, dashProgress);

            playerRigidbody.MovePosition(newPosition);
            currTime += Time.deltaTime;

            yield return null;
        }

        isDashing = false;
        Dashed?.Invoke();

        yield return new WaitForSeconds(dashIFrames);
        GetComponent<CapsuleCollider>().enabled = true;

        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private Vector3 GetDashDirection()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) // Dash forward
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S)) // Dash backward
        {
            moveDirection -= transform.forward;
        }
        if (Input.GetKey(KeyCode.D)) // Dash right
        {
            moveDirection += transform.right;
        }
        if (Input.GetKey(KeyCode.A)) // Dash left
        {
            moveDirection -= transform.right;
        }

        moveDirection.Normalize();

        return moveDirection;
    }
}
