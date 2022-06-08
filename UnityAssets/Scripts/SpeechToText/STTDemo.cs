using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dhlworks.utilities.android;

namespace dhlworks.voicerecon.speechtotext
{
    public class test : MonoBehaviour
    {

        private System.Collections.Concurrent.ConcurrentQueue<string> q = new System.Collections.Concurrent.ConcurrentQueue<string>();
        [SerializeField] private TMPro.TMP_InputField logInputField;

        public void OnResultCallback(string Value)
        {
            q.Enqueue($"\nResults received: {Value}");
        }

        public void OnPartialResultsCallback(string Value)
        {
            q.Enqueue($"\nPartial results received: {Value}");
        }

        public void OnErrorCallback(int Value)
        {
            q.Enqueue($"\nError code received: {Value}");
        }

        public void OnReadyForSpeechCallback()
        {
            q.Enqueue($"\nReady for speech recognition.");
        }

        public void OnBeginningOfSpeechCallback()
        {
            q.Enqueue($"\nRecognition started.");
        }

        public void OnEndOfSpeechCallback()
        {
            q.Enqueue($"\nRecognition ended.");
        }

        public void OnRmsChangedCallback(float Value)
        {
            q.Enqueue($"\nDecibel volume shifted: {Value}");
        }

        public void OnBufferReceived(byte[] Value)
        {
            q.Enqueue($"\nBuffer received: {Value}");
        }

        public void StartRecognition()
        {
            SpeechToTextManager.Instance.StartListening();
        }

        private void Update()
        {
            string r;
            if (q.TryDequeue(out r))
            {
                logInputField.text += r;
            }

        }
    }
}