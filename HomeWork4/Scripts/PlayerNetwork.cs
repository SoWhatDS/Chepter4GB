using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private GameObject _playerPrefab;

    private GameObject _playerCharacter;

    private void Start()
    {
        SpawnCharacter();
    }

    private void SpawnCharacter()
    {

        _playerCharacter = Instantiate(_playerPrefab);
       
    }
}
