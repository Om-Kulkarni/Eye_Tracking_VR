using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // For XR Interaction Toolkit components

/// <summary>
/// This script listens to events from an XRSimpleInteractable component
/// and changes the object's color to provide visual feedback for hovering.
/// This is the standard way to create highlighting within the XR Interaction Toolkit.
/// </summary>
/// 
[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(Renderer))]

public class XRIHighlightFeedback : MonoBehaviour
{
    // Start is called before the first frame update
    public Color highlightColor = Color.yellow; // Color to change to when hovered

    private XRGrabInteractable interactable; // Reference to the interactable component
    private Renderer objectRenderer; // Reference to the Renderer component
    private Color originalColor; // Store the original color of the object

    void Awake()
    {
        // Get the interactable and renderer components
        interactable = GetComponent<XRGrabInteractable>();
        objectRenderer = GetComponent<Renderer>();

        // Store the original color of the object
        originalColor = objectRenderer.material.color;
    }

    private void OnEnable()
    {
        // Subscribe to hover events
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
    }

    private void OnDisable()
    {
        // Unsubscribe from hover events
        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);
    }

    /// <summary>
    /// This method is called by the XRSimpleInteractable when a gaze or controller hover begins.
    /// </summary>
    /// 
    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        // Change the object's color to the highlight color
        objectRenderer.material.color = highlightColor;
    }

    /// <summary>
    /// This method is called by the XRSimpleInteractable when a gaze or controller hover ends.
    /// </summary>
    /// 
    private void OnHoverExited(HoverExitEventArgs args)
    {
        // Change the object's color back to the original color
        objectRenderer.material.color = originalColor;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
