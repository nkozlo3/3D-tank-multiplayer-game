using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class TooltipsManager : MonoBehaviour
{
    [Tooltip("Text we display")]
    public TextMeshProUGUI tipText;

    [Tooltip("Size of tip window")]
    public RectTransform tipWindow;

    [Tooltip("When the mouse is over the object: displays controls message and position of message")]
    public static Action<string, Vector2> OnMouseOver;

    [Tooltip("When the mouse is not over the objec0t: disables tip window")]
    public static Action OnMouseOff;

    /// <summary>
    /// Enables the tip
    /// </summary>
    private void OnEnable()
    {
        // Start displaying tip message
        OnMouseOver += ShowTip;
        OnMouseOff += HideTip;
    }

    /// <summary>
    /// Disables the tip
    /// </summary>
    private void OnDisable()
    {
        // Stops displaying tip message
        OnMouseOver -= ShowTip;
        OnMouseOff -= HideTip;
    }

    /// <summary>
    /// Called at the first frame
    /// </summary>
    void Start()
    {
        // Avoid null exception errors
        HideTip();
    }

    /// <summary>
    /// Displays text when mouse hovers over object
    /// </summary>
    /// <param name="tip">The message we want to display</param>
    /// <param name="mousePos">The position of the mouse</param>
    private void ShowTip(string tip, Vector2 mousePos)
    {
        // Setting tip 
        tipText.text = tip;
        // Setting size of tip window
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);

        // Activating gameObject
        tipWindow.gameObject.SetActive(true);

        // Setting tip to be over gameObject
        tipWindow.transform.position = new Vector2(mousePos.x, mousePos.y);
    }

    /// <summary>
    /// Hides tip when mouse is no longer hovering over object
    /// </summary>
    private void HideTip()
    {
        // Setting the text to default to avoid null exceptions in start
        tipText.text = default;
        // Disabling 
        tipWindow.gameObject.SetActive(false);
    }
}
