using UnityEngine;
using TMPro;
using Unity.Netcode;


public class PlayerCount : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI playerCountText;
    public NetworkVariable<int> networkPlayerCount = new NetworkVariable<int>(0);

    public override void OnNetworkSpawn()
    {
        networkPlayerCount.OnValueChanged += OnPlayerCountChanged;

        // Initialize UI for whoever just spawned in
        UpdateUI(networkPlayerCount.Value);

        // Server-side: Hook into connection callbacks
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

            // Set initial count
            networkPlayerCount.Value = NetworkManager.Singleton.ConnectedClients.Count;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        // Server updates the NetworkVariable
        networkPlayerCount.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }

    private void OnClientDisconnected(ulong clientId)
    {
        // Server updates the NetworkVariable
        networkPlayerCount.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }

    private void OnPlayerCountChanged(int oldValue, int newValue)
    {
        UpdateUI(newValue);
    }

    private void UpdateUI(int count)
    {
        if (playerCountText != null)
        {
            playerCountText.text = $"Players: {count}";
        }
    }

    void Update()
    {
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer)
        {
            // Get total connected clients (including the Host if applicable)
            int count = NetworkManager.Singleton.ConnectedClients.Count;
            Debug.Log($"Current Players: {count}");
            
            if (playerCountText != null)
            {
                playerCountText.text = $"Player Count: {count}";
            }
        }
    }
}
