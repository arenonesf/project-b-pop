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
        // Aseg�rate de que la c�mara principal existe
        if (Camera.main != null)
        {
            Vector3 cameraRotation = Camera.main.transform.eulerAngles;
            // Ajusta la rotaci�n del sistema de part�culas para que siga la rotaci�n Y de la c�mara
            transform.rotation = Quaternion.Euler(0, cameraRotation.y, 0);
        }
    }
}
