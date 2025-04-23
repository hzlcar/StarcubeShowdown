using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour
{
    Rigidbody rigidbody;
    public float jumpStrength = 2;
    public float doubleJumpCooldown = 0.5f;
    public event System.Action Jumped;

    public bool canDoubleJump;

    [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
    GroundCheck groundCheck;


    void Reset()
    {
        // Try to get groundCheck.
        groundCheck = GetComponentInChildren<GroundCheck>();
    }

    void Awake()
    {
        // Get rigidbody.
        rigidbody = GetComponent<Rigidbody>();
        canDoubleJump = true;
    }

    void LateUpdate()
    {
        // Jump when the Jump button is pressed
        if (Input.GetButtonDown("Jump") && canDoubleJump)// && (!groundCheck || groundCheck.isGrounded))
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(Vector3.up * 100 * jumpStrength);
            Jumped?.Invoke();
            canDoubleJump = false;
            StartCoroutine(StartCooldown());
        }
    }

    private IEnumerator StartCooldown()
    {
        yield return new WaitForSeconds(doubleJumpCooldown);
        canDoubleJump = true;
    }
}
