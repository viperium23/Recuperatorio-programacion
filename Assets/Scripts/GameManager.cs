using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform[] _spawnPoints;


    public NetworkVariable<int> RedScore = new();
    public NetworkVariable<int> BlueScore = new();
    public NetworkVariable<float> Timer = new();

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
            case Team.Red:RedScore.Value = RedScore.Value + points; break;

            case Team.Blue:BlueScore.Value = BlueScore.Value + points; break;
        }


    }

    public override void OnNetworkSpawn()
    {
        if (IsServer) 
        {
            Timer.Value = 60;
        }
    }

    void Update()
    {
        
        if (IsServer)
        {
            Timer.Value -= Time.deltaTime;

            if(Timer.Value <= 0)
            {
                Timer.Value = 0;
            }

        }

    }

    public Vector3 GetSpawmPosition(int Index)
    {
        return _spawnPoints[Index].position;
    }

}