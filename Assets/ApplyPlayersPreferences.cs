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
        float db = Mathf.Lerp(-80f, 20f, playerPreferences.masterVolume / 10f);
        if (db > 20)
            db = 20;
        audioMixer.SetFloat("Master", db);

        //meter aqui a mudar a sensibilidade algo como "setting da sens = playerPreferences.sensitivity"
        if (playerInputs != null)
        {
            float sens = Mathf.Lerp(0.01f, 0.5f, playerPreferences.mouseSensitivity / 10);
            playerInputs.changeSensitivity(sens);            
        }

    }

    public void UpdateThePreferences()
    {
        float db = Mathf.Lerp(-20f, 20f, playerPreferences.masterVolume / 10f);
        if (db > 20)
            db = 20;
        else if (db == -40)
            db = -80;
        audioMixer.SetFloat("Master", db);
        //meter aqui a mudar a sensibilidade algo como "setting da sens = playerPreferences.sensitivity"
        if (playerInputs != null)
        {
            float sens = Mathf.Lerp(0.001f, 0.5f, playerPreferences.mouseSensitivity / 10);
            playerInputs.changeSensitivity(sens);            
        }
    }
}
