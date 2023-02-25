using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ShootFireBall : NetworkBehaviour
{
    [SerializeField] private GameObject _fireBall;
    [SerializeField] private Transform _shootTransform;
    [SerializeField] private List<GameObject> _spawnedFireBalls = new List<GameObject>();

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ShootServerRpc();
           
        }
    }

    [ServerRpc]
    private void ShootServerRpc()
    {
        GameObject go = Instantiate(_fireBall, _shootTransform.position, _shootTransform.rotation);
        _spawnedFireBalls.Add(go);
        go.GetComponent<MoveProjectTile>().Parent = this;
        go.GetComponent<NetworkObject>().Spawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRpc()
    {
        GameObject toDestroy = _spawnedFireBalls[0];
        toDestroy.GetComponent<NetworkObject>().Despawn();
        _spawnedFireBalls.Remove(toDestroy);
        Destroy(toDestroy);
    }
}
