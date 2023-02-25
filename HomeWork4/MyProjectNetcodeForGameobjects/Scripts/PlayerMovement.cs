using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _startPosition = 100f;

    public override void OnNetworkSpawn()
    {
        UpdatePositionServerRpc();
    }


    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        transform.Translate(movementDirection * _movementSpeed * Time.deltaTime, Space.World);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation,_rotationSpeed * Time.deltaTime);
            Debug.Log(toRotation);
        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdatePositionServerRpc()
    {
        transform.position = new Vector3(Random.Range(_startPosition, -_startPosition), 1, Random.Range(_startPosition, -_startPosition));
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
