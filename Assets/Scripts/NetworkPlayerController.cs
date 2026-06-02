using UnityEngine;
using Unity.Netcode;

public class NetworkPlayerController : NetworkBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float groundedGravity = -2f;
    [SerializeField] float jumpHeight = 5.0f;

    private CharacterController controller;
    private float verticalVelocity;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputDiretion = new Vector2(horizontalInput, verticalInput);

        bool jump = Input.GetKeyDown(KeyCode.Space);

        if(IsServer)
        {
            MovePlayer(inputDiretion, jump);
        }
        else
        {
            MovePlayerRPC(inputDiretion, jump);
        }
    }

    [Rpc(SendTo.Server)] //marks the next method as an RPC that runs on the server.

    private void MovePlayerRPC(Vector2 movementInput, bool jump)
    {
        MovePlayer(movementInput, jump);
    }

    private void MovePlayer(Vector2 movementInput, bool jump)
    {
        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = groundedGravity;
        }
        if (jump)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        else
        {
            verticalVelocity += groundedGravity * Time.deltaTime;
        }

        

        Vector3 moveDirection = new Vector3(movementInput.x, 0f, movementInput.y).normalized;
        Vector3 horizontalMovement = moveDirection * moveSpeed;
        Vector3 vertialMovement = Vector3.up * verticalVelocity;
        Vector3 finalMovement = horizontalMovement + vertialMovement;   

        controller.Move(finalMovement * Time.deltaTime);
    }
}
