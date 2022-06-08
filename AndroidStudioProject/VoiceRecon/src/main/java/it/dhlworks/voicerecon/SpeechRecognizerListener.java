package it.dhlworks.voicerecon;


import android.os.Bundle;
import android.speech.RecognitionListener;
import android.speech.SpeechRecognizer;
import android.util.JsonWriter;
import android.util.Log;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;

public class SpeechRecognizerListener implements RecognitionListener {

    private final String TAG = "UU-SRListener";

    private ArrayList<String> results = new ArrayList<>();
    private ArrayList<String> partialResults = new ArrayList<>();
    private ISpeechRecognizer callbacks = null;

    public void SetCallbacks(ISpeechRecognizer Callbacks) {
        callbacks = Callbacks;
    }

    @Override
    public void onReadyForSpeech(Bundle bundle) {
        callbacks.OnReadyForSpeech();
    }

    @Override
    public void onBeginningOfSpeech() {
        callbacks.OnBeginningOfSpeech();
    }

    @Override
    public void onRmsChanged(float v) {
        callbacks.OnRmsChanged(v);
    }

    @Override
    public void onBufferReceived(byte[] bytes) {
        callbacks.OnBufferReceived(bytes);
    }

    @Override
    public void onEndOfSpeech() {
        callbacks.OnEndOfSpeech();
    }

    @Override
    public void onError(int i) {
        callbacks.OnError(i);
    }

    @Override
    public void onResults(Bundle bundle) {
        switch (SpeechToText.Instance.recognitionMode) {
            case RAW:
                results = bundle.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION);
                callbacks.OnResult(results.toString());
                break;
            case JSON:
                JSONObject jsonObject = new JSONObject();
                results = bundle.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION);
                if (results != null) {
                    for (int i=0; i<results.size(); i++) {
                        try {
                            jsonObject.put("RecognitionData",results.get(i));
                        } catch (JSONException jsonException) {
                            Log.e(TAG,"Fatal error while parsing Results to Json! "+jsonException);
                        }
                    }
                }
                callbacks.OnResult(jsonObject.toString());
                break;
            case PLAINTEXT:
                StringBuilder stringBuilder = new StringBuilder();
                results = bundle.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION);
                if (results != null) {
                    stringBuilder.append(results.get(0));
                    for (int i=1; i < results.size(); i++) {
                        stringBuilder.append("~").append(results.get(i));
                    }
                }
                callbacks.OnResult(stringBuilder.toString());
                break;
            default:
                break;
        }
    }

    @Override
    public void onPartialResults(Bundle bundle) {
        switch (SpeechToText.Instance.recognitionMode) {
            case RAW:
                partialResults = bundle.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION);
                callbacks.OnPartialResult(partialResults.toString());
                break;
            case JSON:
                JSONObject jsonObject = new JSONObject();
                partialResults = bundle.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION);
                if (partialResults != null) {
                    for (int i=0; i<partialResults.size(); i++) {
                        try {
                            jsonObject.put("RecognitionData",partialResults.get(i));
                        } catch (JSONException jsonException) {
                            Log.e(TAG,"Fatal error while parsing Results to Json! "+jsonException);
                        }
                    }
                }
                callbacks.OnPartialResult(jsonObject.toString());
                break;
            case PLAINTEXT:
                StringBuilder stringBuilder = new StringBuilder();
                partialResults = bundle.getStringArrayList(SpeechRecognizer.RESULTS_RECOGNITION);
                if (partialResults != null) {
                    stringBuilder.append(results.get(0));
                    for (int i=1; i < results.size(); i++) {
                        stringBuilder.append("~").append(results.get(i));
                    }
                }
                callbacks.OnPartialResult(stringBuilder.toString());
                break;
            default:
                break;
        }
    }

    @Override
    public void onEvent(int i, Bundle bundle) {
        callbacks.OnEvent(i);
    }
}