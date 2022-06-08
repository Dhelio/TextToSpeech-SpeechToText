package it.dhlworks.voicerecon;

import android.content.Context;
import android.speech.tts.Voice;
import android.util.Log;

import java.util.Locale;
import java.util.Set;

public class TextToSpeech {

    private final String TAG = "UU-TextToSpeech";
    private android.speech.tts.TextToSpeech textToSpeech = null;
    private boolean is_Ready = false;

    public void Initialize(Context UnityContext, String LanguageID, String VoiceID) {
        if (is_Ready) {
            Log.e(TAG, "Tried to initialize the service, but it is already initialized.");
            return;
        }
        textToSpeech = new android.speech.tts.TextToSpeech(UnityContext, new android.speech.tts.TextToSpeech.OnInitListener() {
            @Override
            public void onInit(int i) {
                is_Ready = true;

                SetLanguage(LanguageID);
                SetVoice(VoiceID);
            }
        });
    }

    /***
     * Says a text instantly.
     * @param Text What to say
     */
    public void Say(String Text) {
        if (!is_Ready) {
            Log.e(TAG, "Tried to say something, but service isn't ready yet.");
            return;
        }
        textToSpeech.speak(Text, android.speech.tts.TextToSpeech.QUEUE_FLUSH, null, "");
    }

    /***
     * Says a text, enqueuing it if other texts are in queue
     * @param Text What to say
     */
    public void SayQueue(String Text) {
        if (!is_Ready) {
            Log.e(TAG, "Tried to say something, but service isn't ready yet.");
            return;
        }
        textToSpeech.speak(Text, android.speech.tts.TextToSpeech.QUEUE_ADD, null);
    }

    /***
     * Checks if the text to speech is talking or not.
     * @return true if it is talking, false otherwise
     */
    public boolean GetIsTalking () {
        return textToSpeech.isSpeaking();
    }

    /***
     * Stops speaking.
     */
    public void Stop() {
        if (!is_Ready) {
            Log.e(TAG, "Tried to stop speaking, but service isn't ready yet.");
            return;
        }

        if (textToSpeech.isSpeaking()) {
            textToSpeech.stop();
        }
    }

    /***
     * Sets language of the speaking app.
     * @param LanguageID
     */
    public void SetLanguage(String LanguageID) {
        if (!is_Ready) {
            Log.e(TAG, "Tried to set language, but service isn't ready yet.");
            return;
        }

        switch (LanguageID) {
            case "en-AU":
                textToSpeech.setLanguage(Locale.ENGLISH);
                break;
            case "en-CA":
                textToSpeech.setLanguage(Locale.CANADA);
                break;
            case "en-GB":
                textToSpeech.setLanguage(Locale.ENGLISH);
                break;
            case "en-IE":
                textToSpeech.setLanguage(Locale.ENGLISH);
                break;
            case "en-IN":
                textToSpeech.setLanguage(Locale.ENGLISH);
                break;
            case "en-NZ":
                textToSpeech.setLanguage(Locale.ENGLISH);
                break;
            case "en-US":
                textToSpeech.setLanguage(Locale.US);
                break;
            case "en-ZA":
                textToSpeech.setLanguage(Locale.ENGLISH);
                break;
            case "fr-BE":
                textToSpeech.setLanguage(Locale.FRENCH);
                break;
            case "fr-CA":
                textToSpeech.setLanguage(Locale.CANADA_FRENCH);
                break;
            case "fr-CH":
                textToSpeech.setLanguage(Locale.FRENCH);
                break;
            case "fr-FR":
                textToSpeech.setLanguage(Locale.FRENCH);
                break;
            case "it-CH":
                textToSpeech.setLanguage(Locale.ITALIAN);
                break;
            case "it-IT":
                textToSpeech.setLanguage(Locale.ITALY);
                break;
            case "jp-JP":
                textToSpeech.setLanguage(Locale.JAPANESE);
                break;
            case "ko-KR":
                textToSpeech.setLanguage(Locale.KOREAN);
                break;
            case "zh-CN":
                textToSpeech.setLanguage(Locale.CHINA);
                break;
            case "zh-HK":
                textToSpeech.setLanguage(Locale.CHINESE);
                break;
            case "zh-TW":
                textToSpeech.setLanguage(Locale.CHINESE);
                break;
            default:
                textToSpeech.setLanguage(Locale.getDefault());
                break;
        }
    }

    /***
     * Sets the speed of the voice
     * @param SpeechRate The speed
     */
    public void SetSpeechRate(float SpeechRate) {
        if (!is_Ready) {
            Log.e(TAG,"Tried to set speech rate, but service isn't ready yet.");
            return;
        }
        textToSpeech.setSpeechRate(SpeechRate);
    }

    /***
     * Sets the pitch of the voice
     * @param Pitch The pitch
     */
    public void SetPitch (float Pitch) {
        if (!is_Ready) {
            Log.e(TAG,"Tried to set speech pitch, but service isn't ready yet.");
            return;
        }
        textToSpeech.setPitch(Pitch);
    }

    /***
     * Obtains current speaking language
     * @return a string representing the language
     */
    public String GetLanguage () {
        if (!is_Ready) {
            Log.e(TAG, "Tried to get language, but service isn't ready yet.");
            return null;
        }
        return textToSpeech.getLanguage().getLanguage();
    }

    /***
     * Returns an array of strings of VoiceIDs
     * @return The array.
     */
    public String[] GetVoices() {
        Set<Voice> voices = textToSpeech.getVoices();
        String[] result = new String[voices.size()];
        int i=0;
        for (Voice voice : voices) {
            result[i] = voice.getName();
            i++;
        }
        return result;
    }

    /***
     * Sets the voice generator.
     * @param VoiceID The voice ID. Get one by calling "GetVoices".
     */
    public void SetVoice(String VoiceID) {
        if (VoiceID == null) {
            textToSpeech.setVoice(textToSpeech.getDefaultVoice());
            return;
        }
        Set<Voice> voices = textToSpeech.getVoices();
        int i=0;
        try {
            for (Voice voice : voices) {
                if (voice.getName().equals(VoiceID)) {
                    textToSpeech.setVoice(voice);
                    return;
                }
            }
        } catch (Exception e) {
            Log.e(TAG,"Fatal error while trying to set voice. " +e);
        }
        Log.e(TAG,"Error: no such VoiceID "+VoiceID);
    }

    /***
     * Destroys the service
     */
    public void Destroy() {
        if (!is_Ready) {
            Log.e(TAG,"Tried to destroy service, but service isn't ready yet.");
            return;
        }

        textToSpeech.shutdown();
    }
}
