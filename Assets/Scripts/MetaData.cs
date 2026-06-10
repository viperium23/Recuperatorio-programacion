using UnityEngine;

public class MetaData : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        PointData Data = other.GetComponent<PointData>();

        if (Data != null)
        {
            Data.DeliverPoints();
        }
    }
}
