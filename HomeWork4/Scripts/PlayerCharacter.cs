using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    [Range(0, 100)]
    [SerializeField] private int _health = 100;
    [Range(0.5f, 10.0f)]
    [SerializeField] private float _movingSpeed = 8.0f;
    [SerializeField] private float acceleration = 3.0f;
    private const float gravity = -9.8f;

    private CharacterController _characterController;
    private MouseLook _mouseLook;
    private Vector3 _currentVelocity;
    private ClientNetworkAutority _clientNetworkAutority;
    
    protected override FireAction FireAction { get; set; }

    private void Start()
    {
        Initiate();
    }

    private void OnGUI()
    {
        if (Camera.main == null)
        {
            return;
        }

        var info = $"Health: {_health} \nClip:{FireAction.countBullet}";
        var size = 12;
        var bulletCountSize = 50;
        var posX = Camera.main.pixelWidth / 2 - size / 4;
        var posY = Camera.main.pixelHeight / 2 - size / 2;
        var posXBul = Camera.main.pixelWidth - bulletCountSize * 2;
        var posYBul = Camera.main.pixelHeight - bulletCountSize;
        GUI.Label(new Rect(posX, posY, size, size), " + ");
        GUI.Label(new Rect(posXBul, posYBul, bulletCountSize * 2, bulletCountSize * 2), info);
    }

    protected override void Initiate()
    {
        base.Initiate();
        FireAction = gameObject.AddComponent<RayShooter>();
        FireAction.Reloading();
        _characterController = GetComponentInChildren<CharacterController>();
        _characterController ??= gameObject.AddComponent<CharacterController>();
        _mouseLook = GetComponentInChildren<MouseLook>();
        _mouseLook ??= gameObject.AddComponent<MouseLook>();
        _clientNetworkAutority = GetComponentInChildren<ClientNetworkAutority>();
        _clientNetworkAutority ??= gameObject.AddComponent<ClientNetworkAutority>();
        
    }

    protected override void Movement()
    {
        if (_mouseLook != null && _mouseLook.PlayerCamera != null)
        {
            _mouseLook.PlayerCamera.enabled = !_clientNetworkAutority.OnIsServer;
        }
        if (_clientNetworkAutority.OnIsServer == false)
        {
            var moveX = Input.GetAxis("Horizontal") * _movingSpeed;
            var moveZ = Input.GetAxis("Vertical") * _movingSpeed;
            var movement = new Vector3(moveX, 0, moveZ);
            movement = Vector3.ClampMagnitude(movement, _movingSpeed) * Time.deltaTime;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement *= acceleration;
            }
            movement.y = gravity;
            movement = transform.TransformDirection(movement);
            _characterController.Move(movement);
            _mouseLook.Rotation();
            CmdUpdatePosition(transform.position);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, ServerPosition, ref _currentVelocity, _movingSpeed * Time.deltaTime);
        }
    }
}
