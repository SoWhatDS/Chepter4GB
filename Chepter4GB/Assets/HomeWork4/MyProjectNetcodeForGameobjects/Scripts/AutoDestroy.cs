using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AutoDestroy : NetworkBehaviour
{
    public float delayBeforeDestroy = 5f;

    private void Start()
    {
        Destroy(gameObject, delayBeforeDestroy);
    }

    [ServerRpc(RequireOwnership = false)]
    private void DestroyParticlesServerRpc()
    {
        GetComponent<NetworkObject>().Despawn();
        Destroy(gameObject, delayBeforeDestroy);
    }
}
