using UnityEngine;

public class Collector : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            CollectibleManager.score++;    
        }
    }
}
