using UnityEngine;
using Unity.Netcode;

public class NetworkPlayerShooter : NetworkBehaviour
{
    [SerializeField] private GameObject bulletPrefab; // calls projectile prefab
    [SerializeField] private Transform bulletSpawnPoint; // calls bullet spawn point
    [SerializeField] private float fireCooldown = 0.5f; // cooldown time in between spawning of bullets when shooting
    [SerializeField] KeyCode fireKey = KeyCode.F; // right click for pew pew

    private float lastFireTime;
 
    // Update is called once per frame
    void Update()
    {
        if(!IsOwner)
        {
            return;
        }

        if(Input.GetKeyDown(fireKey) && Time.time >= lastFireTime)
        {
            lastFireTime = Time.time + fireCooldown;
            RequestShootServerRpc(bulletSpawnPoint.position, bulletSpawnPoint.forward);
        }
    }

    [ServerRpc]

    private void RequestShootServerRpc(Vector3 spawnPosition, Vector3 spawnDirection)
    {
        GameObject projectileInstance = Instantiate(bulletPrefab, spawnPosition, Quaternion.LookRotation(spawnDirection));

        NetworkObject networkObj = projectileInstance.GetComponent<NetworkObject>();
        networkObj.Spawn();

    }
}
