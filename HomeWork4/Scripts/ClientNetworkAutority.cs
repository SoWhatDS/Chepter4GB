
using UnityEngine;
using Unity.Netcode.Components;

public class ClientNetworkAutority : NetworkTransform
{
    public bool OnIsServer => OnIsServerAuthoritative();

    protected override bool OnIsServerAuthoritative()
    {
        return false;
    }
}
