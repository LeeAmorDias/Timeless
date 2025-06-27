using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal; // Use Rendering.HighDefinition if you're on HDRP

public class DOFToggler : MonoBehaviour
{
    [SerializeField]
    private Volume volume;

    public void DeactivateDOF()
    {
        if (volume != null && volume.profile.TryGet<DepthOfField>(out var dof))
        {
            dof.active = false;
        }
    }

    public void ActivateDOF()
    {
        if (volume != null && volume.profile.TryGet<DepthOfField>(out var dof))
        {
            dof.active = true;
        }
    }
}

