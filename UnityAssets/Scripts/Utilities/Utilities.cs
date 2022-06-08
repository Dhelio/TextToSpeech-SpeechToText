using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dhlworks.utilities.android {
    public class Utilities {
        public static void LogD(string TAG, string Content) {
            Debug.Log($"-{TAG}::Debug- {Content}");
        }

        public static void LogW(string TAG, string Content) {
            Debug.LogWarning($"-{TAG}::Warning- {Content}");
        }

        public static void LogE(string TAG, string Content) {
            Debug.LogError($"-{TAG}::Error- {Content}");
        }

        public static void LogI(string TAG, string Content) {
            Debug.Log($"-{TAG}::Info- {Content}");
        }

        /// <summary>
        /// Returns the current Unity Activity. Useful for Native plugins.
        /// </summary>
        /// <returns>And AndroidJavaObject representing the Unity Activity</returns>
        public static AndroidJavaObject getUnityActivity() {
            if (!Application.isEditor) {
                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
                return unityActivity;
            }
            return null;
        }
    }
}