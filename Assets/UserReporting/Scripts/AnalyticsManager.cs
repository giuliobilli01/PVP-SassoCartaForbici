using UnityEngine;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class AnalyticsManager : MonoBehaviour {

    async void Start() {

        try {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        } catch (ConsentCheckException e) {
            // Debug.Log(e);
        }
    }

    /// <summary>
    /// Sends the match result to the analytics service.
    /// Example of a custom event with parameters.
    /// The custom event needs to be created in the Unity Analytics dashboard.
    /// </summary>
    public void SendMatchResultAnalytics(MatchResults matchResult, int player1Swaps, int player2Swaps) {
        
        var parameters = new Dictionary<string, object>
        {
            { "matchResult", matchResult.ToString() },
            { "player1Swaps", player1Swaps },
            { "player2Swaps", player2Swaps }
        };

        AnalyticsService.Instance.CustomData("matchResult", parameters); // stores the event locally until it is sent to the analytics service
        AnalyticsService.Instance.Flush(); // flushes the locally stored events to the analytics service
    }
     
}
    
    

