using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dhlworks.utilities.android;

namespace dhlworks.voicerecon.speechtotext
{
    /// <summary>
    /// Class to interface with the Android Native plugin.
    /// </summary>
    public class SpeechToText
    {
        private AndroidJavaObject stt = null;

        public SpeechToText()
        {
            if (stt == null)
            {
                stt = new AndroidJavaObject("it.dhlworks.voicerecon.SpeechToText");
            }
        }

        public SpeechToText(string TargetGameObjectName, string Language, SpeechToTextCallbacks Callbacks, bool Should_GetPartialResults = false, int MaxResults = 3, int RecognitionMode = 0)
        {
            if (stt == null)
            {
                stt = new AndroidJavaObject("it.dhlworks.voicerecon.SpeechToText");
            }
            Initialize(TargetGameObjectName, Language, Callbacks, Should_GetPartialResults, MaxResults, RecognitionMode);
        }

        public void Initialize(string TargetGameObjectName, string Language, SpeechToTextCallbacks Callbacks, bool Should_GetPartialResults = false, int MaxResults = 3, int RecognitionMode = 0)
        {
            AndroidJNI.AttachCurrentThread();
            stt.Call("Initialize", Utilities.getUnityActivity(), TargetGameObjectName, Language, Callbacks, Should_GetPartialResults, MaxResults, RecognitionMode);
        }

        public void Destroy()
        {
            stt.Call("Destroy");
        }

        public void SetShouldGetPartialResults(bool Value)
        {
            stt.Call("SetShouldGetPartialResults", Value);
        }

        public void SetMaxResults(int MaxResults)
        {
            stt.Call("SetMaxResults", MaxResults);
        }

        public void SetCallbacks(SpeechToTextCallbacks Callbacks)
        {
            stt.Call("SetCallbacks", Callbacks);
        }

        public void SetLanguage(string Language)
        {
            stt.Call("SetLanguage", Language);
        }

        public void StartListening()
        {
            stt.Call("StartListening");
        }

        public void StopListening()
        {
            stt.Call("StopListening");
        }
    }
}