using UnityEngine;

public class PackageInfoManager : MonoBehaviour
{
    [Header("Route Displays")]
    [SerializeField] private SimpleRouteDisplay routeDisplay; // Reference to the SimpleRouteDisplay script

    [Header("Package Info Menus")]
    [SerializeField] private GameObject packageInfo1Menu; // Menu for PackageInfo1
    [SerializeField] private GameObject packageInfo2Menu; // Menu for PackageInfo2

    [Header("StackAssist Menu")]
    [SerializeField] private GameObject stackAssistMenu; // Menu for StackAssist

    public void OnRouteABActivated()
    {
        if (packageInfo1Menu == null)
        {
            Debug.LogError("PackageInfo1 menu is not assigned!");
            return;
        }

        // Enable PackageInfo1 menu
        packageInfo1Menu.SetActive(true);

        Debug.Log("PackageInfo1 menu activated when A->B navigation started.");
    }

    public void OnRouteABDeactivated()
    {
        if (packageInfo1Menu == null)
        {
            Debug.LogError("PackageInfo1 menu is not assigned!");
            return;
        }

        // Disable PackageInfo1 menu
        packageInfo1Menu.SetActive(false);

        Debug.Log("PackageInfo1 menu deactivated when A->B navigation stopped.");
    }

    public void OnRouteBCActivated()
    {
        if (packageInfo2Menu == null)
        {
            Debug.LogError("PackageInfo2 menu is not assigned!");
            return;
        }

        // Enable PackageInfo2 menu
        packageInfo2Menu.SetActive(true);

        Debug.Log("PackageInfo2 menu activated when B->C navigation started.");
    }

    public void OnRouteBCDeactivated()
    {
        if (packageInfo2Menu == null)
        {
            Debug.LogError("PackageInfo2 menu is not assigned!");
            return;
        }

        // Disable PackageInfo2 menu
        packageInfo2Menu.SetActive(false);

        Debug.Log("PackageInfo2 menu deactivated when B->C navigation stopped.");
    }

    public void ShowStackAssistMenu()
    {
        if (stackAssistMenu == null)
        {
            Debug.LogError("StackAssist menu is not assigned!");
            return;
        }

        // Enable StackAssist menu
        stackAssistMenu.SetActive(true);

        Debug.Log("StackAssist menu displayed.");
    }

    public void HideStackAssistMenu()
    {
        if (stackAssistMenu == null)
        {
            Debug.LogError("StackAssist menu is not assigned!");
            return;
        }

        // Disable StackAssist menu
        stackAssistMenu.SetActive(false);

        Debug.Log("StackAssist menu hidden.");
    }

    // Triggered by "NextBTN" on PackageInfo1
    public void OnNextButtonPressedFromPackageInfo1()
    {
        if (packageInfo1Menu == null || packageInfo2Menu == null || routeDisplay == null)
        {
            Debug.LogError("PackageInfo1 menu, PackageInfo2 menu, or RouteDisplay is not assigned!");
            return;
        }

        // Hide navigation A->B
        routeDisplay.HideRoute();

        // Show navigation B->C
        routeDisplay.DrawRouteBC();

        // Hide PackageInfo1 menu
        packageInfo1Menu.SetActive(false);

        // Show PackageInfo2 menu
        packageInfo2Menu.SetActive(true);

        Debug.Log("Transitioned from PackageInfo1 to PackageInfo2 with navigation B->C.");
    }

    // Triggered by "NextBTN" on PackageInfo2
    public void OnNextButtonPressedFromPackageInfo2()
    {
        if (packageInfo2Menu == null || stackAssistMenu == null || routeDisplay == null)
        {
            Debug.LogError("PackageInfo2 menu, StackAssist menu, or RouteDisplay is not assigned!");
            return;
        }

        // Hide navigation B->C
        routeDisplay.HideRoute();

        // Hide PackageInfo2 menu
        packageInfo2Menu.SetActive(false);

        // Show StackAssist menu
        stackAssistMenu.SetActive(true);

        Debug.Log("Transitioned from PackageInfo2 to StackAssist menu.");
    }
}
