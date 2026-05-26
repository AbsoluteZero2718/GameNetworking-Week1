using UnityEngine;
using Unity.Netcode;
public class NetworkPlayerJump : NetworkBehaviour
{
    [SerializeField] float jumpForce = 5.0f;

    public Rigidbody rb;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!IsOwner)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            RequestJumpServerRpc();
        }

       
    }

    [ServerRpc]
    void RequestJumpServerRpc()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
