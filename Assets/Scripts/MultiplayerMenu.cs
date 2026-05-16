using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;

public class MultiplayerMenu : MonoBehaviour
{
    public GameObject hidebuttons;
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

    public void HideUI()
    {
        if (hidebuttons != null)
        {
            hidebuttons.SetActive(false);
        }
    }

}
