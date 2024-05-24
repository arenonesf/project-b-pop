using System;
using UnityEngine;

public abstract class MagicArtifact : MonoBehaviour
{
    [SerializeField] protected bool active;
    [SerializeField] protected SourceType acceptType;
    [SerializeField] protected SourceType type;

    public static Action GiveMagic;
    public static Action TakeMagic;
    public bool Active => active;
    public SourceType ArtifactSourceType => type;

    protected virtual void RetrieveMagic()
    {
        GiveMagic?.Invoke();
    }
    protected virtual void SendMagic()
    {
        TakeMagic?.Invoke();
    }
}
