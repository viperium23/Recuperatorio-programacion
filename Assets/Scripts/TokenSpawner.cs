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

        for (int i =0; i< 5 ; i++)
        {

            SpawnToken();

        }
    }

    private bool OffSpawn(int index)
    {
        Collider[] coliders = Physics.OverlapSphere(_tokenSpawn[index].position, 0.2f);

        foreach (Collider col in coliders) 
        { 
            if(col.GetComponent<TokenData>() != null)
            {
                return true;
            }
        }

        return false;

    }

    public void SpawnToken()
    {
        int index = Random.Range(0, _tokenSpawn.Length);

        while (OffSpawn(index))
        {
            index = Random.Range(0, _tokenSpawn.Length);
        }
        
        Vector3 pos = _tokenSpawn[index].position;

        GameObject token = Instantiate(_tokenPF,pos,Quaternion.identity);
        token.GetComponent<NetworkObject>().Spawn(); 

    }

}
