using NaughtyAttributes;
using UnityEngine;

/// <summary>
/// Handles player interaction with interactable objects in the game. 
/// It uses raycasting to detect interactables in the player's line of sight and allows the player to interact with them using input actions.
///
/// The player can interact with objects by aiming at them and pressing the interact button.
/// The system includes visual feedback through the crosshair, which changes size based on whether an object can be interacted with.
///
/// <para>
/// The class listens for the following inputs:
/// <list type="bullet">
///     <item><description>InteractButtonDown: Activates interaction with the currently targeted object.</description></item>
/// </list>
/// </para>
/// </summary>
public class PlayerInteractor : MonoBehaviour
{
    [SerializeField]
    private GameObject interactToolTip;

    [Header("Interaction Settings")]
    [Tooltip("Layer mask used to define which objects are interactable.")]
    [SerializeField] private LayerMask interactablesLayerMask; // Defines which layers are interactable.

    [Tooltip("Maximum distance for interaction with objects.")]
    [SerializeField] private float interactionDistance = 2f; // Distance at which the player can interact with objects.

    [Tooltip("Prefab for the crosshair that shows when an interactable object is within range.")]
    [SerializeField] private GameObject crosshairPrefab; // Prefab used for the crosshair UI.

    [Header("Debugging")]
    [Tooltip("Enable this to display debug messages from this script in the Console.")]
    [SerializeField] private bool showDebugMessages = false; // Flag to enable or disable debug messages.

    [SerializeField, Tooltip("If enabled, debug messages will include the object's name as an identifier."), ShowIf(nameof(showDebugMessages))]
    private bool identifyObject = true; // Flag to include the object name in debug messages if debugging is enabled.
    
    private PlayerInputs playerInputs; // Reference to the PlayerInputs script to get player input.
    private PlayerInventory playerInventory; // Reference to the PlayerInventory script to access the player's inventory.
    private CrosshairUI crosshair; // The crosshair UI component that will change size based on interaction availability.

    private bool canInteract = true;

    private void Start()
    {
        try
        {
            // Attempt to find an existing crosshair UI in the scene.
            crosshair = FindAnyObjectByType<CrosshairUI>();
        }
        catch
        {
            // If no crosshair is found, instantiate a new one and add it to the scene.
            Debug.LogWarning(
                "Player Interactor didn't find a crosshair and is creating one. I recommend adding one to the scene.",
                this);
            crosshair = Instantiate(crosshairPrefab).GetComponent<CrosshairUI>();
        }

        // Find the PlayerInputs component.
        playerInputs = FindFirstObjectByType<PlayerInputs>();
        playerInventory = GetComponent<PlayerInventory>();
        interactToolTip?.SetActive(false);
        canInteract = false;
    }

    private void Update()
    {
        if (!canInteract) return;

        // Default action: Shrink the crosshair if no interaction happens.
        bool shouldGrowCrosshair = false;

        // Raycast to check if there is an interactable object in front of the player.
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactionDistance, interactablesLayerMask))
        {
            Interactable interactable = hit.transform.GetComponent<Interactable>(); // Get the Interactable component.

            bool isItemValid = false;

            if (interactable == null)
            {
                shouldGrowCrosshair = false;
            }
            else if (interactable.TryGetComponent(out InventoryItemMatcher itemMatcher))
            {
                Item selected = playerInventory.GetSelectedItem();
                Item rightItem = itemMatcher.RightItem;

                
                var displayer = interactable.GetComponent<ItemDisplayer>();

                if (displayer != null && displayer.IsItemDisplayed)
                {
                    isItemValid = true;
                }
                else if (selected == null || rightItem == null )
                {
                    isItemValid = false;
                }
                else if (itemMatcher.AcceptEra)
                {
                    isItemValid = selected.era == itemMatcher.Era && selected.Puzzlenumber == itemMatcher.puzzleNumber;
                    Debug.Log($"{(isItemValid ? "Era match" : "Era mismatch")}");
                }
                else
                {
                    isItemValid = selected == rightItem;
                }
            }
            else isItemValid = true;

            if (interactable?.TryGetComponent(out Pedestal pedestal) == true)
            {
                if (pedestal.CanInteract)
                {
                    if (playerInputs.InteractButtonDown)
                    {
                        interactable.Interact();
                        shouldGrowCrosshair = false;
                    }
                    else if (!pedestal.HaveCristal && isItemValid)
                    {
                        shouldGrowCrosshair = true;
                    }
                    else if (pedestal.HaveCristal)
                    {
                        shouldGrowCrosshair = true;
                    }
                }
            }
            else if (interactable != null && interactable.CanInteract && isItemValid) // Check if the interactable object is interactable.
            {
                shouldGrowCrosshair = true; // We need to grow the crosshair when interactable and can interact.

                // If the interact button is pressed, interact with the object.
                if (playerInputs.InteractButtonDown)
                {
                    interactable.Interact();
                    shouldGrowCrosshair = false;
                }
            }
            else shouldGrowCrosshair = false;
        }
        else shouldGrowCrosshair = false;

        // Only change crosshair state if necessary
        if (shouldGrowCrosshair)
        {
            interactToolTip?.SetActive(true);
            crosshair.Grow();
        }
        else
        {
            interactToolTip?.SetActive(false);
            crosshair.Shrink();
        }

    }

    /// <summary>
    /// Logs a debug message to the Console if debugging is enabled.
    /// Includes the object's name as an identifier if 'identifyObject' is true.
    /// </summary>
    /// <param name="message">The debug message to log.</param>
    private void Log(string message)
    {
        // Check if debug messages are enabled.
        if (showDebugMessages)
        {
            if (identifyObject)
                Debug.Log(message, this); // Log with object name if 'identifyObject' is true.
            else
                Debug.Log(message); // Log without object name.
        }
    }

    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }
}
