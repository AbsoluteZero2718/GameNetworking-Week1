using TMPro;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class MultiplayerMenuSimple : NetworkBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] public TMP_Text PlayerCountText;

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void StartServer()
    {
        NetworkManager.Singleton.StartServer();
    }

    public void HideMenu()
    {
        if (menuUI != null)
        {
            menuUI.SetActive(false);
        }
    }

    void Update()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        int playerCount = players.Length;
        // Update TMP text
        PlayerCountText.text = "Players: " + playerCount;
    }

}