using UnityEngine;
using Unity.Netcode;


public class TokenSpawner : NetworkBehaviour
{

    [SerializeField] private Transform[] _tokenSpawn;
    [SerializeField] private GameObject _tokenPF;

    public static TokenSpawner instance;

    private void Awake()
    {
        instance = this;
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        for (int i =0; i<6; i++)
        {
            SpawnToken();

        }
    }

    public void SpawnToken()
    {
        int index = Random.Range(0, _tokenSpawn.Length);

        Vector3 pos = _tokenSpawn[index].position;

        GameObject token = Instantiate(_tokenPF,pos,Quaternion.identity);
        token.GetComponent<NetworkObject>().Spawn(); 

    }

}
