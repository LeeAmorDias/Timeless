using UnityEngine;
using UnityEngine.Audio;

public class ApplyPlayersPreferences : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private PlayerPreferences playerPreferences;

    void Awake()
    {
        float db = Mathf.Lerp(-80f, 20f, playerPreferences.masterVolume / 10f);
        if (db > 20)
            db = 20;
        audioMixer.SetFloat("Master", db);

        //meter aqui a mudar a sensibilidade algo como "setting da sens = playerPreferences.sensitivity"
    }

    public void UpdateThePreferences()
    {
        float db = Mathf.Lerp(-40f, 20f, playerPreferences.masterVolume / 10f);
        if (db > 20)
            db = 20;
        else if(db == -40)
            db = -80;
        audioMixer.SetFloat("Master", db);

        //meter aqui a mudar a sensibilidade algo como "setting da sens = playerPreferences.sensitivity"
    }
}
