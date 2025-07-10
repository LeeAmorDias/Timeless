using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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

