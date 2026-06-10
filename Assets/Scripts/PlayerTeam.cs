using UnityEngine;
using Unity.Netcode;
using System;

public class PlayerTeam : NetworkBehaviour
{
    [SerializeField] private Renderer _renderer;

    public NetworkVariable<Team> PlayerTeamValue = new();

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        { 
        int index = (int)OwnerClientId;

        transform.position = GameManager.instance.GetSpawmPosition(index);
        
        PlayerTeamValue.Value = (OwnerClientId % 2 == 0) ? Team.Red : Team.Blue;

        Debug.Log("Jugador " + OwnerClientId + " -> Equipo " + PlayerTeamValue.Value);
        
        }

        PlayerTeamValue.OnValueChanged += OnTeamChanged;

        ApplyTeamColor();
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
