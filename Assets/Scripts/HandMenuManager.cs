using UnityEngine;

public class HandMenuManager : MonoBehaviour
{
    [Header("StackAssist Menu")]
    [SerializeField] private GameObject stackAssistMenu;

    // Button #1 in the hand menu: Show Route A->B
    public void ShowRouteAB()
    {
        // Find the SimpleRouteDisplay in the scene
        SimpleRouteDisplay routeDisplay = FindObjectOfType<SimpleRouteDisplay>();
        if (routeDisplay == null)
        {
            Debug.LogError("No SimpleRouteDisplay found in the scene!");
            return;
        }

        // Hide any previously displayed route before showing the new one
        routeDisplay.HideRoute();

        // Draw the A->B route
        routeDisplay.DrawRouteAB();

        Debug.Log("Hand Menu Button 1 pressed: Displayed the route A->B.");
    }

    // Button #2 in the hand menu: Show Route B->C
    public void ShowRouteBC()
    {
        // Find the SimpleRouteDisplay in the scene
        SimpleRouteDisplay routeDisplay = FindObjectOfType<SimpleRouteDisplay>();
        if (routeDisplay == null)
        {
            Debug.LogError("No SimpleRouteDisplay found in the scene!");
            return;
        }

        // Hide any previously displayed route before showing the new one
        routeDisplay.HideRoute();

        // Draw the B->C route
        routeDisplay.DrawRouteBC();

        Debug.Log("Hand Menu Button 2 pressed: Displayed the route B->C.");
    }

    // Button #3 in the hand menu: Toggle the StackAssist menu
    public void ToggleStackAssistMenu()
    {
        if (stackAssistMenu == null)
        {
            Debug.LogError("StackAssist Menu is not assigned in the Inspector!");
            return;
        }

        // Toggle the active state of the stackAssistMenu
        bool isActive = stackAssistMenu.activeSelf;
        stackAssistMenu.SetActive(!isActive);

        if (!isActive)
        {
            Debug.Log("Hand Menu Button (StackAssist) pressed: StackAssist menu is now visible.");
        }
        else
        {
            Debug.Log("Hand Menu Button (StackAssist) pressed: StackAssist menu is now hidden.");
        }
    }
}