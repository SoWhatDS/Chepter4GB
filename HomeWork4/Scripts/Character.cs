using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class Character : NetworkBehaviour
{
    protected Action OnUpdateAction { get; set; }
    protected abstract FireAction FireAction { get; set; }
    protected Vector3 ServerPosition { get; set; }

    protected virtual void Initiate()
    {
        OnUpdateAction += Movement;
    }

    private void Update()
    {
        OnUpdate();
    }

    private void OnUpdate()
    {
        OnUpdateAction?.Invoke();
    }
   
    protected void CmdUpdatePosition(Vector3 position)
    {
        ServerPosition = position;
    }

    protected abstract void Movement();
}
