using System;
using ProjectBPop.Magic;
using UnityEngine;

public abstract class MagicArtifact : MonoBehaviour
{
    [SerializeField] protected bool active;
    [SerializeField] protected SourceType acceptType;
    [SerializeField] protected SourceType type;
    [SerializeField] protected ArtifactType artifactType;

    public static Action GiveMagic;
    public static Action TakeMagic;
    public bool Active => active;
    public SourceType ArtifactSourceType => type;
    public ArtifactType ArtifactType => artifactType;

    protected virtual void RetrieveMagic()
    {
        GiveMagic?.Invoke();
    }
    protected virtual void SendMagic()
    {
        TakeMagic?.Invoke();
    }
}
