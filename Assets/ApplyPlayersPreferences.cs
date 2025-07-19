using UnityEngine;
using UnityEngine.Audio;

public class ApplyPlayersPreferences : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private PlayerPreferences playerPreferences;
    [SerializeField]
    private PlayerInputs playerInputs;

    void Awake()
    {
        UpdateThePreferences();
    }

    public void UpdateThePreferences()
    {
        float value = playerPreferences.masterVolume;

        // Ensure the volume is not too small or negative to avoid invalid calculations
        if (value <= 1e-5f)
            value = 1e-5f; // Clamp to the lower bound

        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);

        //meter aqui a mudar a sensibilidade algo como "setting da sens = playerPreferences.sensitivity"
        if (playerInputs != null)
        {
            float sens = Mathf.Lerp(0.001f, 0.5f, playerPreferences.mouseSensitivity / 10);
            playerInputs.changeSensitivity(sens);
        }
    }
}
