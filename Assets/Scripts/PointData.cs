using UnityEngine;
using Unity.Netcode;

public class PointData : NetworkBehaviour
{
    public NetworkVariable<int> CurrentPoints = new();
    public NetworkVariable<int> TotalPoints = new();

    public void AddPoint()
    {
        if (!IsServer) return;

        CurrentPoints.Value++;
    }

    public void DeliverPoints()
    {
        if (!IsServer) return;

        PlayerTeam playerTeam = GetComponent<PlayerTeam>();

        GameManager.instance.AddTeamPoints(playerTeam.PlayerTeamValue.Value,CurrentPoints.Value);

        TotalPoints.Value += CurrentPoints.Value;
        CurrentPoints.Value = 0;
    }



}
