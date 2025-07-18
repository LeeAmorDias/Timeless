using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PauseManager : MonoBehaviour
{
    // Reference to player input handling.
    private PlayerInputs playerInputs;

    public bool IsPaused { get; private set; } = false;

    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private GameObject Crosshair;
    [SerializeField]
    private GameObject invUI;
    [SerializeField]
    private DOFToggler dOFToggler;

    private bool canPause = true;

    private void Start()
    {
        //Ensures the game will start moving
        Time.timeScale = 1;

        // Try to find the PlayerInputs object in the scene.
        playerInputs = FindFirstObjectByType<PlayerInputs>();

        // If PlayerInputs is missing, instantiate it and log a warning.
        if (playerInputs == null)
        {
            Debug.LogWarning($"{nameof(InspectionsHandler)} needs a {nameof(PlayerInputs)} object in the scene.");
        }
    }
    private void Update()
    {
        if (!canPause) return;
        if (playerInputs.PauseButtonDown)
        {
            ButtonPressed();
        }
    }
    public void ButtonPressed()
    {
        PauseSystem();
        IsPaused = !IsPaused;
    }

    private void PauseSystem()
    {
        if (!IsPaused)
        {
            Crosshair.SetActive(false);
            invUI?.SetActive(false);
            PauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            dOFToggler.ActivateDOF();
        }
        else
        {
            Crosshair.SetActive(true);
            if (invUI != null)
            {
                invUI.SetActive(true);
                var InventoryUI = invUI.GetComponentInChildren<PlayerUIInventory>();
                if (InventoryUI)
                {
                    InventoryUI.UpdateUI();
                }
            }
            Cursor.lockState = CursorLockMode.Locked;
            PauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
            Time.timeScale = 1;
            dOFToggler.DeactivateDOF();
        }
    }

    public void SetCanPause(bool value)
    {
        canPause = value;
    }
}
