using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.SceneManagement;
using Unity.Services.Matchmaker.Models;

public class PlayerTeam : NetworkBehaviour
{
    [SerializeField] private Renderer _renderer;

    public NetworkVariable<Team> PlayerTeamValue = new();

    public override void OnNetworkSpawn()
    {
        PlayerTeamValue.OnValueChanged += OnTeamChanged;
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (IsServer)
        {
            int index = (int)OwnerClientId;
           
            transform.position = GameManager.instance.GetSpawmPosition(index);
            
            PlayerTeamValue.Value =
                (OwnerClientId % 2 == 0) ? Team.Red : Team.Blue;
            
        }

        ApplyTeamColor();

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!IsServer) return;
        if (scene.name != "Game") return;
        

            int index = (int)OwnerClientId;

            transform.position = GameManager.instance.GetSpawmPosition(index);

            PlayerTeamValue.Value = (OwnerClientId % 2 == 0) ? Team.Red : Team.Blue;

            Debug.Log("Jugador " + OwnerClientId + " -> Equipo " + PlayerTeamValue.Value);

        

        PlayerTeamValue.OnValueChanged += OnTeamChanged;

        ApplyTeamColor();

    }

    public override void OnNetworkDespawn()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        PlayerTeamValue.OnValueChanged -= OnTeamChanged;
    }

    private void OnTeamChanged(Team previous, Team current)
    {
        ApplyTeamColor();
    }

    private void ApplyTeamColor()
    {
        if (_renderer == null) return;

        switch (PlayerTeamValue.Value)
        {
            case Team.Red:_renderer.material.color = Color.red; break;



            case Team.Blue:_renderer.material.color = Color.blue; break;


        }
    }

}
