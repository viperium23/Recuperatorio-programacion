using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform[] _spawnPoints;

    public NetworkVariable<int> RedScore = new();
    public NetworkVariable<int> BlueScore = new();

    private int _playerCount;


    public Team GetTeam()
    {
        _playerCount++;

        if (_playerCount % 2 == 1)
        {
            return Team.Red;
        }

        return Team.Blue;
    }

    private void Awake()
    {
        instance = this;
    }

    public void AddTeamPoints(Team teams, int points)
    {
        if(!IsServer) return;

        switch(teams) 
        {
            case Team.Red:RedScore.Value = points; break;

            case Team.Blue:BlueScore.Value = points; break;
        }

    }

    void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            NetworkManager.Singleton.StartHost(); //Iniciamos el Host
        }

        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            NetworkManager.Singleton.StartClient(); //Iniciamos como client
        }
    }

    public Vector3 GetSpawmPosition(int Index)
    {
        return _spawnPoints[Index].position;
    }

}