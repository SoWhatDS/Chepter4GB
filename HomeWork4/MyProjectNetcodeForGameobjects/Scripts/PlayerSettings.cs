using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerSettings : NetworkBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private TextMeshProUGUI _playerName;

    public List<Color> Colors = new List<Color>();
    private NetworkVariable<FixedString128Bytes> _networkPlayerName =
        new NetworkVariable<FixedString128Bytes>("Player: 0", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public override void OnNetworkSpawn()
    {
        _networkPlayerName.Value = "Player: " + (OwnerClientId + 1);
        _playerName.text = _networkPlayerName.Value.ToString();
        _meshRenderer.material.color = Colors[(int)OwnerClientId];
    }


}
