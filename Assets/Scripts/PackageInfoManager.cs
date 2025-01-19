using UnityEngine;

public class PackageInfoManager : MonoBehaviour
{
    [Header("Route Displays")]
    [SerializeField] private SimpleRouteDisplay routeDisplay; // Reference to the SimpleRouteDisplay script

    [Header("Package Info Menus")]
    [SerializeField] private GameObject packageInfo1Menu; // Reference to PackageInfo1 menu
    [SerializeField] private GameObject packageInfo2Menu; // Reference to PackageInfo2 menu

    [Header("StackAssist Menu")]
    [SerializeField] private GameObject stackAssistMenu; // Reference to the StackAssist menu

    private bool isNavigatingAtoB = true; // Tracks whether we're navigating from A to B

    public void ShowPackageInfo1()
    {
        if (packageInfo1Menu == null || routeDisplay == null)
        {
            Debug.LogError("PackageInfo1 menu or RouteDisplay is not assigned!");
            return;
        }

        // Enable PackageInfo1 menu
        packageInfo1Menu.SetActive(true);

        // Show navigation from A to B
        routeDisplay.DrawRouteAB();

        Debug.Log("PackageInfo1 menu displayed and navigation from A to B started.");
    }

    public void OnNextButtonPressedFromPackageInfo1()
    {
        if (isNavigatingAtoB)
        {
            // Transition from A->B to B->C
            TransitionToPackageInfo2();
        }
        else
        {
            Debug.LogWarning("Already on PackageInfo2. No further transition is possible.");
        }
    }

    private void TransitionToPackageInfo2()
    {
        if (packageInfo1Menu == null || packageInfo2Menu == null || routeDisplay == null)
        {
            Debug.LogError("PackageInfo1, PackageInfo2 menus or RouteDisplay are not assigned!");
            return;
        }

        // Hide PackageInfo1 menu
        packageInfo1Menu.SetActive(false);

        // Enable PackageInfo2 menu
        packageInfo2Menu.SetActive(true);

        // Switch navigation from A->B to B->C
        routeDisplay.HideRoute();
        routeDisplay.DrawRouteBC();

        // Update navigation state
        isNavigatingAtoB = false;

        Debug.Log("Switched to PackageInfo2 and navigation from B to C started.");
    }

    public void OnNextButtonPressedFromPackageInfo2()
    {
        if (packageInfo2Menu == null || stackAssistMenu == null)
        {
            Debug.LogError("PackageInfo2 menu or StackAssist menu is not assigned!");
            return;
        }

        // Hide PackageInfo2 menu
        packageInfo2Menu.SetActive(false);

        // Hide navigation from B to C
        if (routeDisplay != null)
        {
            routeDisplay.HideRoute();
            Debug.Log("Navigation from B to C hidden.");
        }

        // Enable StackAssist menu
        stackAssistMenu.SetActive(true);

        Debug.Log("StackAssist menu displayed after PackageInfo2.");
    }
}
