using UnityEngine;

public class SkyboxAnimator : MonoBehaviour
{
    [SerializeField] private Material skybox;
    [SerializeField] private float animationSpeed = 20.0f;
    void Start()
    {
        
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * animationSpeed);
    }
}
