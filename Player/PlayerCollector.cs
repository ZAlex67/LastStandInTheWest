using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IPickup pickup))
        {
            pickup.Collect(this);
        }
    }
}