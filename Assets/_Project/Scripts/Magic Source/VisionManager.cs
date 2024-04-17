using System.Collections.Generic;
using UnityEngine;

public class VisionManager : MonoBehaviour
{
    public static VisionManager Instance { get; private set; }
    [SerializeField] private List<GameObject> specialObjects;
    [SerializeField] private Material specialVisionMaterial;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }

        Instance = this;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }
    
    public void SetSpecialMaterial()
    {
        foreach (var element in specialObjects)
        {
            element.GetComponent<MeshRenderer>().material = specialVisionMaterial;
            element.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void HideObjects()
    {
        foreach (var element in specialObjects)
        {
            element.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
