using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dhlworks.utilities;
using dhlworks.utilities.android;

namespace dhlworks.voicerecon.texttospeech
{
    public class TextToSpeech
    {

        #region Private Fields
        private const string TAG = "TextToSpeech";

        private AndroidJavaObject tts = null;

        private string languageType = null;
        private string voiceType = null;
        #endregion

        #region Public Fields
        public TextToSpeech()
        {
            tts = new AndroidJavaObject("it.dhlworks.voicerecon.TextToSpeech");
        }
        #endregion

        #region Public Methods

        public void Initialize(string Language = null, string Voice = null)
        {
            tts.Call("Initialize", Utilities.getUnityActivity(), Language, Voice);
        }

        /// <summary>
        /// Says a text instantly
        /// </summary>
        /// <param name="Text">What to say</param>
        public void Say(string Text)
        {
            tts.Call("Say", Text);
        }

        /// <summary>
        /// Says a text after the previous queued ones have been said.
        /// </summary>
        /// <param name="Text">What to say</param>
        public void SayQueue(string Text)
        {
            tts.Call("SayQueue", Text);
        }

        /// <summary>
        /// Stops talking.
        /// </summary>
        public void Stop()
        {
            tts.Call("Stop");
        }

        /// <summary>
        /// Sets the language of the speaking plugin.
        /// </summary>
        /// <param name="Language">The language to speak into</param>
        public void SetLanguage(string Language)
        {
            tts.Call("SetLanguage", Language);
        }

        /// <summary>
        /// Sets the speed of the speech.
        /// </summary>
        /// <param name="SpeechRate">The speed.</param>
        public void SetSpeechRate(float SpeechRate)
        {
            tts.Call("SetSpeechRate", SpeechRate);
        }

        /// <summary>
        /// Sets the pitch of the speech.
        /// </summary>
        /// <param name="Pitch">The pitch</param>
        public void SetPitch(float Pitch)
        {
            tts.Call("SetPitch", Pitch);
        }

        /// <summary>
        /// Checks in what king of language the plugin is speaking into.
        /// </summary>
        /// <returns></returns>
        public string GetLanguage()
        {
            return tts.Call<string>("GetLanguage");
        }

        /// <summary>
        /// Checks wheter or not the plugin is talking right now.
        /// </summary>
        /// <returns></returns>
        public bool GetIsTalking()
        {
            return tts.Call<bool>("GetIsTalking");
        }
        /// <summary>
        /// Frees the resources.
        /// </summary>
        public void Destroy()
        {
            tts.Call("Destroy");
        }

        /// <summary>
        /// Gets all the available voices on the device.
        /// </summary>
        /// <returns>A string array with the singular voice IDs available on the device</returns>
        public string[] GetVoices()
        {
            return tts.Call<string[]>("GetVoices");
        }

        /// <summary>
        /// Sets the voice of the Text To Speech by the given string ID. These IDs are available in the Voices struct.
        /// </summary>
        /// <param name="VoiceID">The voice ID to set the Text To Speech to. Voice IDs are available in the Voices struct.</param>
        public void SetVoice(string VoiceID)
        {
            tts.Call("SetVoice", VoiceID);
        }
        #endregion
    }
}