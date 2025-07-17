using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField]
    private PlayerPreferences playerPreferences;
    [SerializeField]
    private Image fillVolume;
    [SerializeField]
    private Image fillSensitivity;
    [SerializeField]
    private ApplyPlayersPreferences applyPlayersPreferences;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fillVolume.fillAmount = playerPreferences.masterVolume/10;
        fillSensitivity.fillAmount = playerPreferences.mouseSensitivity / 10;
    }

    public void TakeVolume()
    {
        fillVolume.fillAmount -= 0.1f;
        UpdateThePreferences();
    }
    public void AddVolume()
    {
        fillVolume.fillAmount += 0.1f;
        UpdateThePreferences();
    }
    public void TakeSensitivity()
    {
        fillSensitivity.fillAmount -= 0.1f;
        UpdateThePreferences();
    }
    public void AddSensitivity()
    {
        fillSensitivity.fillAmount += 0.1f;
        UpdateThePreferences();
    }

    // Update is called once per frame
    private void UpdateThePreferences()
    {
        playerPreferences.masterVolume = fillVolume.fillAmount * 10;
        playerPreferences.mouseSensitivity = fillSensitivity.fillAmount * 10;
        applyPlayersPreferences.UpdateThePreferences();
    }
}
