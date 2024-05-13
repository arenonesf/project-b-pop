using UnityEngine;

public abstract class MagicArtifact : MonoBehaviour
{
    [SerializeField] protected bool active;
    [SerializeField] protected SourceType acceptType;
    [SerializeField] protected SourceType type;
    public bool Active => active;

    protected abstract void RetrieveMagic();
    protected abstract void SendMagic();
}
