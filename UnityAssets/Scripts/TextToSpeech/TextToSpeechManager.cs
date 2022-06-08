using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dhlworks.utilities;

namespace dhlworks.voicerecon.texttospeech
{
    /// <summary>
    /// Singleton implementation of the Text To Speech plugin
    /// </summary>
    public class TextToSpeechManager : MonoBehaviour
    {
        #region PRIVATE VARIABLES
        private TextToSpeech textToSpeech = null;

        [Header("Parameters")]
        [Tooltip("Language to use for the synthesis")]
        [SerializeField] private LanguageType languageType = LanguageType.ENGLISH_UK;
        [Tooltip("What kind of voice to use for the synthesis")]
        [SerializeField] private VoiceType voiceType = VoiceType.EN_GB_LANGUAGE;

        #endregion

        #region PUBLIC VARIABLES
        public static TextToSpeechManager Instance;
        #endregion

        #region PRIVATE METHODS

        #endregion

        #region PUBLIC METHODS
        public void Say(string Text)
        {
            textToSpeech.Say(Text);
        }

        public void SayQueue(string Text)
        {
            textToSpeech.SayQueue(Text);
        }

        public void Stop()
        {
            textToSpeech.Stop();
        }

        public void SetLanguage(LanguageType Language)
        {
            languageType = Language;
            textToSpeech.SetLanguage(Language.GetStringValue());
        }

        public void SetVoice(VoiceType Voice)
        {
            voiceType = Voice;
            textToSpeech.SetVoice(Voice.GetStringValue());
        }

        public void SetSpeechRate(float SpeechRate)
        {
            textToSpeech.SetSpeechRate(SpeechRate);
        }

        public void SetPitch(float Pitch)
        {
            textToSpeech.SetPitch(Pitch);
        }

        public bool GetIsTalking()
        {
            return textToSpeech.GetIsTalking();
        }
        #endregion

        #region UNITY METHODS
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(this);
            }

            textToSpeech = new TextToSpeech();

            textToSpeech.Initialize(languageType.GetStringValue(), voiceType.GetStringValue());
        }

        private void OnApplicationQuit()
        {
            textToSpeech.Destroy();
        }
        #endregion
    }
}