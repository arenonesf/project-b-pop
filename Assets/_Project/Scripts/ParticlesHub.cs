using UnityEngine;

[ExecuteAlways]
public class ParticlesHub : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Asegúrate de que la cámara principal existe
        if (Camera.main != null)
        {
            Vector3 cameraRotation = Camera.main.transform.eulerAngles;
            // Ajusta la rotación del sistema de partículas para que siga la rotación Y de la cámara
            transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
        }
    }
}
