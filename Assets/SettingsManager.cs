using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private PlayerPreferences playerPreferences;
    [SerializeField]
    private Slider fillVolume;
    [SerializeField]
    private Slider fillSensitivity;
    [SerializeField]
    private ApplyPlayersPreferences applyPlayersPreferences;

    private bool canUpdate;

    private void Awake()
    {
        canUpdate = false;
        fillVolume.value = playerPreferences.masterVolume;
        fillSensitivity.value = playerPreferences.mouseSensitivity;
        canUpdate = true;
    }

    public void UpdateThePreferences()
    {
        if (canUpdate)
        {
            playerPreferences.masterVolume = fillVolume.value;
            playerPreferences.mouseSensitivity = fillSensitivity.value;
            applyPlayersPreferences.UpdateThePreferences();
            Debug.Log("update");            
        }
    }
}
