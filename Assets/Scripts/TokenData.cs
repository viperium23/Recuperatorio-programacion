using UnityEngine;
using Unity.Netcode;

public class TokenData : NetworkBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;
        
        PointData Data = other.GetComponent<PointData>();

        if (Data != null)
        {
            if (!IsServer) return ;

            Data.AddPoint();

            TokenSpawner.instance.SpawnToken();

            NetworkObject.Despawn();

        }
    }
}
