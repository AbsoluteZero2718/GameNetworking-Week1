using UnityEngine;
using Unity.Netcode;

public class PlayerSpawnManager : NetworkBehaviour
{
    private static int nextSpawnIndex;
    public override void OnNetworkSpawn() //runs when the player obj is spawned by netcode
    {
        if(!IsServer)
        {
            return;
        }
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("Spawn Point");

        if(spawnPointObjects.Length == 0)
        {
            Debug.LogWarning("No spawn points found.");
            return;
        }

        Transform selectedSpawnPoint = spawnPointObjects[nextSpawnIndex].transform;
        CharacterController characterController = GetComponent<CharacterController>();

        if(characterController != null)
        {
            characterController.enabled = false;
        }

        transform.position = selectedSpawnPoint.position;   
        transform.rotation = selectedSpawnPoint.rotation;
        if (characterController != null)
        {
            characterController.enabled = true;
        }

        nextSpawnIndex++;
        if(nextSpawnIndex >= spawnPointObjects.Length)
        {
            nextSpawnIndex = 0;
        }

    }
}
