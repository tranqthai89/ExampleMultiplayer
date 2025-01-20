using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button btnHost;
    [SerializeField] private Button btnJoin;
    [SerializeField] public TMP_InputField nameInputField;

    void Start()
    {
        btnHost.onClick.AddListener(()=>NetworkManager.Singleton.StartHost());
        btnJoin.onClick.AddListener(()=>NetworkManager.Singleton.StartClient());
    }
}
