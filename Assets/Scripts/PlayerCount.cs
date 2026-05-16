using UnityEngine;
using TMPro;
using Unity.Netcode;


public class PlayerCount : NetworkBehaviour
{
    [SerializeField] private TextMeshProUGUI playerCountText;

    private NetworkVariable<int> totalPlayers = new NetworkVariable<int>(0);
   
    public override void OnNetworkSpawn()
    {
        playerCountText = GetComponent<TextMeshProUGUI>();

        totalPlayers.OnValueChanged += OnPlayerCountChanged;
        UpdateUIText(totalPlayers.Value);

        if (IsServer)
        {
            UpdatePlayerCountServerRPC(1);
            
        }

        
    }

    private void OnPlayerCountChanged(int previousValue, int newValue)
    {
        UpdateUIText(newValue);

        Debug.Log($"Player Count: {newValue}");
    }

    private void UpdateUIText(int count)
    {
        if(playerCountText != null)
        {
            playerCountText.text = $"Player Count: {count}";
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void UpdatePlayerCountServerRPC(int amountToAdd)
    {
        totalPlayers.Value += amountToAdd;   
    }
}
