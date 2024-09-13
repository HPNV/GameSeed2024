using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedPanelHandler : MonoBehaviour
{
    public GameObject seedPanel;
    public GameObject slimePanel;
    
    [SerializeField] private TextMeshProUGUI seedpediaText;
    [SerializeField] private TextMeshProUGUI slimepediaText;

    [SerializeField] private GameObject sliderImage;
    
    public float animationDuration = 2f; // Duration for animation

    private bool isAnimating = false; // Flag to prevent toggling while animating

    public void TogglePanel()
    {
        // Prevent toggling if an animation is in progress
        if (isAnimating)
        {
            Debug.Log("Animation in progress. Cannot toggle right now.");
            return; // Exit the method to prevent further toggling
        }

        isAnimating = true; // Set the flag to indicate animation is in progress

        // Determine positions (set according to your layout)
        Vector3 seedPanelHidePosition = new Vector3(-1200, seedPanel.transform.localPosition.y, 0); // Off-screen left
        Vector3 seedPanelShowPosition = new Vector3(0, seedPanel.transform.localPosition.y, 0);    // On-screen
        Vector3 slimePanelHidePosition = new Vector3(1200, slimePanel.transform.localPosition.y, 0); // Off-screen right
        Vector3 slimePanelShowPosition = new Vector3(0, slimePanel.transform.localPosition.y, 0);   // On-screen
        
        Vector3 sliderImageLeftPosition = new Vector3(-135, sliderImage.transform.localPosition.y, 0); // Off-screen right
        Vector3 sliderImageRightPosition = new Vector3(135, sliderImage.transform.localPosition.y, 0);   // On-screen

        // If seed panel is inactive, animate seed panel in and slime panel out
        if (!seedPanel.activeSelf)
        {
            // Slide the seed panel in from the left
            
            slimepediaText.color = new Color(0.427451f, 0.427451f, 0.427451f, 1f);
            seedpediaText.color = new Color(1f, 1f, 1f, 1f);
            
            LeanTween.moveLocal(sliderImage, sliderImageLeftPosition, animationDuration);
            
            seedPanel.SetActive(true); // Activate first, then animate
            LeanTween.moveLocal(seedPanel, seedPanelShowPosition, animationDuration).setOnComplete(() =>
            {
                // Animation complete, allow toggling again
                isAnimating = false;
            });

            // Slide the slime panel out to the right and deactivate after animation
            LeanTween.moveLocal(slimePanel, slimePanelHidePosition, animationDuration).setOnComplete(() =>
            {
                slimePanel.SetActive(false); // Deactivate after animation
            });
        }
        else
        {
            seedpediaText.color = new Color(0.427451f, 0.427451f, 0.427451f, 1f);
            slimepediaText.color = new Color(1f, 1f, 1f, 1f);
            
            LeanTween.moveLocal(sliderImage, sliderImageRightPosition, animationDuration);
            // Slide the seed panel out to the left and deactivate after animation
            LeanTween.moveLocal(seedPanel, seedPanelHidePosition, animationDuration).setOnComplete(() =>
            {
                seedPanel.SetActive(false); // Deactivate after animation
                // Animation complete, allow toggling again
                isAnimating = false;
            });

            // Slide the slime panel in from the right
            slimePanel.SetActive(true); // Activate first, then animate
            LeanTween.moveLocal(slimePanel, slimePanelShowPosition, animationDuration);
        }
    }
}
