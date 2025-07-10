using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPreferences", menuName = "Scriptable Objects/Player Preferences", order = 1)]
public class PlayerPreferences : ScriptableObject
{
    [Header("Audio Settings")]
    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    [Header("Gameplay Settings")]
    public float mouseSensitivity = 1f;
    public bool invertYAxis = false;
}
