using UnityEngine;

public class PlayerInspect : MonoBehaviour
{
    private PlayerInputs playerInputs; // Reference to the PlayerInputs script to get player input.
    private PlayerInventory playerInventory; // Reference to the PlayerInventory script to get player inventory.
    private InspectionsHandler inspectionsHandler; // Reference to the InspectionsHandler script to get inspectionsHandler.

    private bool canInspect = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Find the PlayerInputs component.
        playerInputs = FindFirstObjectByType<PlayerInputs>();
        // Find the PlayerInventory component.
        playerInventory = FindFirstObjectByType<PlayerInventory>();
        // Find the inspectionsHandler component.
        inspectionsHandler = FindFirstObjectByType<InspectionsHandler>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!canInspect) return;

        //check if player click inspect button and goes trhough if he isnt inspecting anything
        if (playerInputs.InspectButtonDown && !inspectionsHandler.inspecting)
        {
            Inspect();
        }
    }

    // If there is an item selected, start inspection and notify it is inspecting from the inventory
    public void Inspect()
    {
        Item item = playerInventory.GetSelectedItem() ?? null;
        if (item != null)
        {
            Debug.Log($"Inspecting item: {item.name}");
            inspectionsHandler.StartInspection(item, false, true);
        }
        else
        {
            Debug.Log("No item selected to inspect.");
        }
    }

    public void SetCanInspect(bool value)
    {
        canInspect = value;
    }
}
