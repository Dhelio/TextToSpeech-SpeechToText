using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace dhlworks.voicerecon.speechtotext
{
    /// <summary>
    /// Class that handles the callbacks for the recognition process
    /// </summary>
    public class SpeechToTextCallbacks : AndroidJavaProxy
    {
        private UnityEvent<string> onResult;
        private UnityEvent<string> onPartialResult;
        private UnityEvent<int> onError;
        private UnityEvent onReadyForSpeech, onBeginningOfSpeech, onEndOfSpeech;
        private UnityEvent<float> onRmsChanged;
        private UnityEvent<byte[]> onBufferReceived;

        public SpeechToTextCallbacks(
            UnityEvent<string> onResult,
            UnityEvent<string> onPartialResult,
            UnityEvent<int> onError,
            UnityEvent onReadyForSpeech,
            UnityEvent onBeginningOfSpeech,
            UnityEvent<float> onRmsChanged,
            UnityEvent<byte[]> onBufferReceived,
            UnityEvent onEndOfSpeech) : base("it.dhlworks.voicerecon.ISpeechRecognizer")
        {
            this.onResult = onResult;
            this.onPartialResult = onPartialResult;
            this.onError = onError;
            this.onReadyForSpeech = onReadyForSpeech;
            this.onBeginningOfSpeech = onBeginningOfSpeech;
            this.onRmsChanged = onRmsChanged;
            this.onBufferReceived = onBufferReceived;
            this.onEndOfSpeech = onEndOfSpeech;
        }

        public virtual void OnResult(string Result)
        {
            onResult.Invoke(Result);
        }

        public virtual void OnPartialResult(string PartialResult)
        {
            onPartialResult.Invoke(PartialResult);
        }

        public virtual void OnError(int ErrorCode)
        {
            onError.Invoke(ErrorCode);
        }

        public virtual void OnReadyForSpeech()
        {
            onReadyForSpeech.Invoke();
        }

        public virtual void OnBeginningOfSpeech()
        {
            onBeginningOfSpeech.Invoke();
        }

        public virtual void OnRmsChanged(float Value)
        {
            onRmsChanged.Invoke(Value);
        }

        public virtual void OnBufferReceived(byte[] Buffer)
        {
            onBufferReceived.Invoke(Buffer);
        }

        public virtual void OnEndOfSpeech()
        {
            onEndOfSpeech.Invoke();
        }

        public void OnEvent(int i)
        {
            throw new System.NotImplementedException();
        }
    }
}