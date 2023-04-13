using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using dhlworks.utilities;
using UnityEngine.Events;

namespace dhlworks.voicerecon.speechtotext
{
    /// <summary>
    /// Singleton Manager for handling all interaction with the Android's Speech To Text.
    /// </summary>
    public class SpeechToTextManager : MonoBehaviour
    {
        #region ENUMS
        public enum ListeningMode { AWAKE, START, DELAYED, CONTINUOUS, EXTERNAL }
        #endregion

        #region PRIVATE VARIABLES
        private const string TAG = "SpeechToTextManager";

        private SpeechToText speechToText = null;
        private bool has_ReceivedResults = false;
        private bool should_ContinueListening = false;

        [Header("Parameters")]
        [Tooltip("Target language for the recognition process.")]
        [SerializeField] private LanguageType language = LanguageType.ENGLISH_US;
        [Tooltip("How OnResult and OnPartialResult should return data.")]
        [SerializeField] private RecognitionMode recognitionMode = RecognitionMode.PLAINTEXT;
        [Tooltip("How the Speech Recognition service should work.\n" +
            "AWAKE = Starts automatically on Awake. All further calls have to be done externally.\n" +
            "START = Starts automatically on Start. All further calls have to be done externally.\n" +
            "DELAYED = Starts recognition after a certain delay. All further calls have to be done externally.\n" +
            "CONTINUOUS = Continuously recognizes speech, providing periodic results.\n" +
            "EXTERNAL = No autonomous start. All further recognitions have to be done externally.\n")]
        [SerializeField] private ListeningMode listeningMode = ListeningMode.AWAKE;
        [Tooltip("Optional field to specify how long the delay for starting to listen should be when ListeningMode is set to DELAYED.")]
        [SerializeField] private float delay = 1.0f;
        [Tooltip("Whether the recognition process should return partial results, until OnResult is called.")]
        [SerializeField] private bool should_GetPartialResults = false;
        [Tooltip("Max number of alternatives per recognition.")]
        [SerializeField] private int maxResults = 3;
        #endregion

        #region PUBLIC VARIABLES
        [Header("Callbacks")]
        public UnityEvent<string> OnResult;
        public UnityEvent<string> OnPartialResult;
        public UnityEvent<int> OnError;
        public UnityEvent OnReadyForSpeech, OnBeginningOfSpeech, OnEndOfSpeech;
        public UnityEvent<float> OnRmsChanged;
        public UnityEvent<byte[]> OnBufferReceived;

        public static SpeechToTextManager Instance;
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Waits a while, then starts recognizing speech.
        /// </summary>
        /// <param name="Delay">How long in seconds to wait</param>
        private IEnumerator DelayedStartListening(float Delay)
        {
            yield return new WaitForSeconds(Delay);
            StartListening();
        }

        /// <summary>
        /// Listens continuously, periodically returning results.
        /// </summary>
        private IEnumerator ContinuousListening()
        {
            while (should_ContinueListening)
            {
                StartListening();
                while (!has_ReceivedResults)
                {
                    yield return null;
                }
                has_ReceivedResults = false;
            }
        }

        /// <summary>
        /// Utility method to know when OnResult has been called.
        /// </summary>
        /// <param name="Results">Unused, has to be present for the callback to work</param>
        private void ContinuousListeningFlagSetter(string Results)
        {
            has_ReceivedResults = true;
        }
        #endregion

        #region PUBLIC METHODS

        /// <summary>
        /// Starts the speech recognition module 
        /// </summary>
        public void StartListening()
        {
            speechToText.StartListening();
        }

        /// <summary>
        /// Stops the speech recognition module. Can throw error if the speech recognition has already stopped.
        /// </summary>
        public void StopListening()
        {
            if (listeningMode == ListeningMode.CONTINUOUS)
            {
                should_ContinueListening = false;
            }
            else
            {
                speechToText.StopListening();
            }
        }

        /// <summary>
        /// Sets the language for the recognition
        /// </summary>
        /// <param name="Language">Language ID compliant to the IETF BCP 47</param>
        public void SetLanguage(LanguageType Language)
        {
            language = Language;
            speechToText.SetLanguage(language.GetStringValue());
        }

        /// <summary>
        /// Sets whether the recognition process should return partial results through the recognition, until the full results in OnResults.
        /// </summary>
        /// <param name="Value">True if it should, false otherwise</param>
        public void SetShouldGetPartialResults(bool Value)
        {
            should_GetPartialResults = Value;
            speechToText.SetShouldGetPartialResults(Value);
        }

        /// <summary>
        /// Sets the callbacks for the various events. All parameters are optional. If null is provided, the old event is used instead.
        /// </summary>
        /// <param name="OnResult">Callback for OnResult</param>
        /// <param name="OnPartialResult">Callback for OnPartialResult</param>
        /// <param name="OnError">Callback for OnError</param>
        /// <param name="OnReadyForSpeech">Callback for OnReadyForSpeech</param>
        /// <param name="OnBeginningOfSpeech">Callback for OnBeginningOfSpeech</param>
        /// <param name="OnEndOfSpeech">Callback for OnEndOfSpeech</param>
        /// <param name="OnRmsChanged">Callback for OnRmsChanged</param>
        /// <param name="OnBufferReceived">Callback for OnBufferReceived</param>
        public void SetCallbacks(UnityEvent<string> OnResult = null,
        UnityEvent<string> OnPartialResult = null,
        UnityEvent<int> OnError = null,
        UnityEvent OnReadyForSpeech = null, UnityEvent OnBeginningOfSpeech = null, UnityEvent OnEndOfSpeech = null,
        UnityEvent<float> OnRmsChanged = null,
        UnityEvent<byte[]> OnBufferReceived = null)
        {
            if (OnResult != null)
            {
                this.OnResult = OnResult;
            }
            if (OnPartialResult != null)
            {
                this.OnPartialResult = OnPartialResult;
            }
            if (OnError != null)
            {
                this.OnError = OnError;
            }
            if (OnReadyForSpeech != null)
            {
                this.OnReadyForSpeech = OnReadyForSpeech;
            }
            if (OnBeginningOfSpeech != null)
            {
                this.OnBeginningOfSpeech = OnBeginningOfSpeech;
            }
            if (OnEndOfSpeech != null)
            {
                this.OnEndOfSpeech = OnEndOfSpeech;
            }
            if (OnRmsChanged != null)
            {
                this.OnRmsChanged = OnRmsChanged;
            }
            if (OnBufferReceived != null)
            {
                this.OnBufferReceived = OnBufferReceived;
            }

            speechToText.SetCallbacks(new SpeechToTextCallbacks(
                    OnResult,
                    OnPartialResult,
                    OnError,
                    OnReadyForSpeech,
                    OnBeginningOfSpeech,
                    OnRmsChanged,
                    OnBufferReceived,
                    OnEndOfSpeech));
        }
        #endregion


        #region UNITY METHODS
        private void Awake()
        {
            //Singleton Pattern
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }

            //Asking permission to the user, since recognition requires the mic
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }

            //Initialization
            speechToText = new SpeechToText();

            speechToText.Initialize(
                this.gameObject.name,
                language.GetStringValue(), new SpeechToTextCallbacks(
                    OnResult,
                    OnPartialResult,
                    OnError,
                    OnReadyForSpeech,
                    OnBeginningOfSpeech,
                    OnRmsChanged,
                    OnBufferReceived,
                    OnEndOfSpeech),
                should_GetPartialResults,
                maxResults,
                (int)recognitionMode
                );

            if (listeningMode == ListeningMode.AWAKE)
            {
                StartListening();
            }
        }

        private void Start()
        {
            if (listeningMode == ListeningMode.START)
            {
                StartListening();
            }
            else if (listeningMode == ListeningMode.DELAYED)
            {
                StartCoroutine(DelayedStartListening(delay));
            }
            else if (listeningMode == ListeningMode.CONTINUOUS)
            {
                should_ContinueListening = true;
                OnResult.AddListener((Results) => ContinuousListeningFlagSetter(Results));
                StartCoroutine(ContinuousListening());
            }
        }
        #endregion
    }
}