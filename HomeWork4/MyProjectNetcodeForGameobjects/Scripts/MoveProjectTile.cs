using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MoveProjectTile : NetworkBehaviour
{
    public ShootFireBall Parent;
    [SerializeField] private GameObject _hitParticles;
    [SerializeField] private float _shootForce;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.velocity = rb.transform.forward * _shootForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!IsOwner)
        {
            return;
        }
        InstantiateHitParticlesServerRpc();
        Parent.DestroyServerRpc();
    }

    [ServerRpc]
    private void InstantiateHitParticlesServerRpc()
    {
        GameObject hitImpact = Instantiate(_hitParticles, transform.position, Quaternion.identity);
        hitImpact.GetComponent<NetworkObject>().Spawn();
        hitImpact.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
    }

}
