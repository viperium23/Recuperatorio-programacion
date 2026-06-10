using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    public Rigidbody Rbd;

    //Movimiento
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    private Vector2 _moveInput;

    //Input
    [SerializeField] private PlayerInput _playerInput;

    private void Start()
    {
        Rbd = GetComponent<Rigidbody>();

        if (!IsOwner)
        {
            
            _playerInput.enabled = false;
            return;
        }

    }

    private void OnMove(InputValue inputValue)
    {
        if (!IsOwner) return;
        

        _moveInput = inputValue.Get<Vector2>();
    }
    
    private Vector3 GetMoveDirection()
    {
        //movimento
        Vector3 forward = Vector3.forward;
        Vector3 right = Vector3.right;


        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return forward * _moveInput.y + right * _moveInput.x;
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        

        Vector3 moveDir = GetMoveDirection();

        Vector3 targetVelocity = new Vector3(moveDir.x * _speed, Rbd.linearVelocity.y, moveDir.z * _speed);
        Rbd.linearVelocity = Vector3.Lerp(Rbd.linearVelocity, targetVelocity, 0.2f);

       

        //Rotacion de personaje
        if (moveDir != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(moveDir);

            Rbd.MoveRotation(Quaternion.Slerp(Rbd.rotation, rotation, _rotationSpeed * Time.fixedDeltaTime));
        }

    }

}
