using UnityEngine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private SceneReference scene;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.Instance.LoadScene(scene);
        }
    }
    
    private void ActivateCollider()
    {
        boxCollider.enabled = true;
    }
}
