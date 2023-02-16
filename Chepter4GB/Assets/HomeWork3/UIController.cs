using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button _buttonConnectClient;
    [SerializeField] private Button _buttonDisconnectClient;
    [SerializeField] private Button _buttonStartServer;
    [SerializeField] private Button _buttonShutDownServer;
    [SerializeField] private Button _buttonSendMessage;

    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private TextField _textField;
    [SerializeField] private TMP_InputField _playerName;
    

    [SerializeField] private Client _client;
    [SerializeField] private Server _server;


    private void Start()
    {
        _buttonConnectClient.onClick.AddListener(() => Connect());
        _buttonDisconnectClient.onClick.AddListener(() => Disconnect());
        _buttonShutDownServer.onClick.AddListener(() => ShutDownServer());
        _buttonStartServer.onClick.AddListener(() => StartServer());
        _buttonSendMessage.onClick.AddListener(() => SendMessage());
        _client.onMessageReceive += ReceiveMessage;

    }

    private void Connect()
    {
        _client.Connect();
        _client.SendMessage(_playerName.text);
        _inputField.text = "";
    }

    private void Disconnect()
    {
        _client.Disconnect();
    }

    private void StartServer()
    {
        _server.StartServer();
    }

    private void ShutDownServer()
    {
        _server.ShutDownServer();
    }

    private void SendMessage()
    {
        _client.SendMessage(_inputField.text);
        _inputField.text = "";
    }

    public void ReceiveMessage(object message)
    {
        _textField.ReceiveMessage(message);
    }
}
