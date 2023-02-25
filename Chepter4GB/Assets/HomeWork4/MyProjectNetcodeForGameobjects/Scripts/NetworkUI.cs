
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : NetworkBehaviour
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _clientButton;
    [SerializeField] private TextMeshProUGUI _playersCountOnServer;

    private NetworkVariable<int> _playersNum = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone);

    private void Awake()
    {
        _hostButton.onClick.AddListener(() => StartHost());
        _clientButton.onClick.AddListener(() => StartClient());
    }

    private void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    private void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    private void Update()
    {
        _playersCountOnServer.text = $"Players: {_playersNum.Value.ToString()}";
        if (!IsServer)
        {
            return;
        }
        _playersNum.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }

}
