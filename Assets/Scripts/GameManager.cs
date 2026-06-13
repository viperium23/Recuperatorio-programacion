using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance;

    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _victoryR;
    [SerializeField] private GameObject _victoryB;
    [SerializeField] private GameObject _draw;
     
    

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

                EndGameClientRpc();

                enabled = false;

            }

        }

    }

    public Vector3 GetSpawmPosition(int Index)
    {
        return _spawnPoints[Index].position;
    }


    public void BackToMenu()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.Shutdown();
            Destroy(NetworkManager.Singleton.gameObject);
        }

        SceneManager.LoadScene("Menu");
    }



[ClientRpc]
    private void EndGameClientRpc()
    {
        // Bloquear movimiento
        PlayerMovement[] players = FindObjectsByType<PlayerMovement>();


        foreach (PlayerMovement player in players)
        {
            player.GetComponent<PlayerInput>().enabled = false;
        }

        // Mostrar pantalla final
        if (RedScore.Value > BlueScore.Value)
        {
            _victoryR.SetActive(true);
        }
        else if (BlueScore.Value > RedScore.Value)
        {
            _victoryB.SetActive(true);
        }
        else
        {
            _draw.SetActive(true);
        }
    }

}