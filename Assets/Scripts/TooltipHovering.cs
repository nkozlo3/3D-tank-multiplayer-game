using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHovering : MonoBehaviour
{
    [Tooltip("Tip we want to show")]
    public string tipToShow;

    [Tooltip("Checks if mouse is already hovering over gameObject")]
    private bool alreadyHovering = false;

    // Time before tooltip is shown
    private float timeToWait = 0.5f;
    
    /// <summary>
    /// Called when mouse hovers over attached gameObject
    /// </summary>
    void OnMouseOver()
    {
        // Showing message
        if (!alreadyHovering)
        {
            // Stop any corountines already running
            StopAllCoroutines();
            // Start coroutine to show message
            StartCoroutine(StartTimer());
        }
        // set alreadyHovering to true
        alreadyHovering = true;
    }

    /// <summary>
    /// Called when mouse stops hovering over attached gameObject
    /// </summary>
    void OnMouseExit()
    {
        // Stop all coroutines (for if we have hovered over it but not long enough to show tip
        StopAllCoroutines();

        // Hide message
        TooltipsManager.OnMouseOff();

        // Set alreadyHovering to false
        alreadyHovering = false;
    }

    /// <summary>
    /// Displays Message when you hover over gameObject
    /// </summary>
    private void ShowMessage()
    {
        // calls tooltipsManager class and activates onMouseOver function with our tip
        TooltipsManager.OnMouseOver(tipToShow, Input.mousePosition);
    }

    /// <summary>
    /// Starts timer to display tooltip
    /// </summary>
    /// <returns>Waits 0.5 seconds</returns>
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(timeToWait);

        // Shows message
        ShowMessage();
    }
}
