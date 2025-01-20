using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerSettings : NetworkBehaviour
{
    [SerializeField] private TextMeshPro playerName;
    NetworkVariable<NetworkString> networkPlayerName = new NetworkVariable<NetworkString>(
        "Unknown",
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Owner
        );
    public override void OnNetworkSpawn()
    {
        if(IsOwner){
            networkPlayerName.Value = GameObject.Find("UIManager").GetComponent<UIManager>().nameInputField.text;
        }

        playerName.text = networkPlayerName.Value.ToString();
        networkPlayerName.OnValueChanged += NetworkPlayerName_OnValueChanged;
    }
    void NetworkPlayerName_OnValueChanged(NetworkString _previousValue, NetworkString _newValue){
        playerName.text = _newValue;
    }
}

public struct NetworkString : INetworkSerializeByMemcpy{
    private ForceNetworkSerializeByMemcpy<FixedString32Bytes> info;
    public void NetworkSerialize<T>(BufferSerializer<T> _serializer) where T : IReaderWriter{
        _serializer.SerializeValue(ref info);
    }
    public override string ToString()
    {
        return info.Value.ToString();
    }
    
    public static implicit operator string (NetworkString _s) => _s.ToString();
    public static implicit operator NetworkString(string _s) => new NetworkString() {info = new FixedString32Bytes(_s)};
}