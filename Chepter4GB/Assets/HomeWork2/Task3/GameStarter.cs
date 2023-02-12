using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private float _cameraSpeed;

    private CameraMove _cameraMove;

    private void Start()
    {
        _mainCamera = Camera.main;
        _cameraMove = new CameraMove(_mainCamera,_cameraSpeed);
    }

    private void Update()
    {
        _cameraMove.Update();
    }


}
