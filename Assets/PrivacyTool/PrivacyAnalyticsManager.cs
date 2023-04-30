using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Analytics;
using UnityEngine.UI;

public class PrivacyAnalyticsManager : MonoBehaviour {
    
    private bool consentHasBeenChecked = true;
    [SerializeField] Toggle toggle;
    [SerializeField] Canvas popupCanvas;

    // Open the popup
    public void OpenPopup() {
        popupCanvas.gameObject.SetActive(true);
    }
    
    // Close the popup
    public void ClosePopup() {

        // If the user has opted out, disable analytics
        if (toggle.isOn) {
            consentHasBeenChecked = false;
            OptOut();
        }

        popupCanvas.gameObject.SetActive(false);
    }

    /// <summary>
    /// Function to opt-out of analytics.
    /// </summary>
    public void OptOut() {
        
        try {
            if (!consentHasBeenChecked) {
                // Show a GDPR/COPPA/other opt-out consent flow
                // If a user opts out
                AnalyticsService.Instance.OptOut();
            }
            // Record that we have checked a user's consent, so we don't repeat the flow unnecessarily.
            // In a real game, use PlayerPrefs or an equivalent to persist this state between sessions
            consentHasBeenChecked = true;
        } catch (ConsentCheckException e) {
            // Handle the exception by checking e.Reason
        }
    }

    /// <summary>
    /// Function to show the privacy URL
    /// </summary>
    public void ShowPrivacyPolicy() {
        Application.OpenURL(AnalyticsService.Instance.PrivacyUrl);
    }

}
