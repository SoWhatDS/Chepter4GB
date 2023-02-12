using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove 
{
    private Camera _mainCamera;
    private float _cameraSpeed;
   
        

    public CameraMove(Camera mainCamera,float cameraSpeed)
    {
        _mainCamera = mainCamera;
        _cameraSpeed = cameraSpeed;
       
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _mainCamera.transform.localPosition += Vector3.forward * Time.deltaTime * _cameraSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _mainCamera.transform.localPosition += -Vector3.forward * Time.deltaTime * _cameraSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _mainCamera.transform.localPosition += -Vector3.right * Time.deltaTime * _cameraSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _mainCamera.transform.localPosition += Vector3.right * Time.deltaTime * _cameraSpeed;
        }

    }
}
