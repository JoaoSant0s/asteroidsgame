using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

using JoaoSant0s.CommonWrapper;

namespace AsteroidsGame
{
    public class RewardedVideoObject : IUnityAdsShowListener, IUnityAdsListener
    {
        private UnityAction completeAction;
        public RewardedVideoObject(UnityAction action)
        {
            completeAction = action;
            Advertisement.AddListener(this);
        }

        #region Implemented IUnityAdsShowListener Methods

        public void OnUnityAdsShowClick(string placementId)
        {
            Debugs.Log("OnUnityAdsShowClick", placementId);
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            Debugs.Log("OnUnityAdsShowComplete", placementId, showCompletionState);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Debugs.Log("OnUnityAdsShowFailure", placementId, error, message);
        }

        public void OnUnityAdsShowStart(string placementId)
        {
            Debugs.Log("OnUnityAdsShowStart", placementId);
        }

        #endregion

        #region Implemented IUnityAdsListener Methods

        public void OnUnityAdsDidError(string message)
        {
            Debugs.Log("OnUnityAdsDidError", message);
            completeAction?.Invoke();
            Advertisement.RemoveListener(this);
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
        {
            Debugs.Log("OnUnityAdsDidFinish", placementId, showResult);
            completeAction?.Invoke();
            Advertisement.RemoveListener(this);
        }

        public void OnUnityAdsDidStart(string placementId)
        {
            Debugs.Log("OnUnityAdsDidStart", placementId);
        }

        public void OnUnityAdsReady(string placementId)
        {
            Debugs.Log("OnUnityAdsReady", placementId);
        }

        #endregion
    }
}