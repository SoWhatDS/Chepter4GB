using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class UILookAtCamera : NetworkBehaviour
{
    void LateUpdate()
    {
        transform.LookAt(transform.position + Camera.main.transform.forward);
    }
}
