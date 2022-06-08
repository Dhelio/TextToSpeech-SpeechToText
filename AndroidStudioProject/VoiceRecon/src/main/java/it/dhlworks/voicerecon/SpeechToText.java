package it.dhlworks.voicerecon;

import android.app.Activity;
import android.content.ActivityNotFoundException;
import android.content.Intent;
import android.os.Build;
import android.os.Bundle;
import android.speech.RecognizerIntent;
import android.speech.SpeechRecognizer;
import android.util.Log;
import android.app.Fragment;

import androidx.annotation.Nullable;

public class SpeechToText extends Fragment {

    //---------------------------------------------------------------------------------------------- PRIVATE VARIABLES
    private final String TAG = "UU-SpeechToText";

    private String targetGameObjectName = null;

    private SpeechRecognizer speechRecognizer = null;
    private SpeechRecognizerListener speechRecognizerListener = null;
    private ISpeechRecognizer callbacks = null;
    private boolean has_Initialized = false;
    private boolean should_GetPartialResults = false;
    private String language = "en-US";
    private int maxResults = 3;

    //---------------------------------------------------------------------------------------------- PUBLIC VARIABLES

    public RecognitionMode recognitionMode;

    public static SpeechToText Instance;

    //---------------------------------------------------------------------------------------------- PRIVATE METHODS

    //---------------------------------------------------------------------------------------------- PUBLIC METHODS

    public void Initialize(Activity UnityActivity, String TargetGameObjectName, String Language, ISpeechRecognizer Callbacks, boolean Should_GetPartialResults, int MaxResults, int RecognitionMode) {
        if (has_Initialized) {
            Log.e(TAG,"Tried to initialize SpeechToText service, but it has already been initialized. Only one instance of the service can run at any given time. Destroy the other one first.");
            return;
        }
        
        if (Instance == null) {
            Instance = this;
        } else if (Instance != this) {
            Log.e(TAG,"Tried to initialize SpeechToText service, but the singleton instance is already running. Destroy the old one first.");
            return;
        }

        speechRecognizerListener = new SpeechRecognizerListener();
        callbacks = Callbacks;
        speechRecognizerListener.SetCallbacks(callbacks);

        language = Language;
        should_GetPartialResults = Should_GetPartialResults;
        maxResults = MaxResults;
        recognitionMode = RecognitionModeUtils.GetRecognitionModeFromInt(RecognitionMode);

        UnityActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N) {
                    UnityActivity.getFragmentManager().beginTransaction().add(Instance, TAG).commitNow();
                } else {
                    UnityActivity.getFragmentManager().beginTransaction().add(Instance, TAG).commit();
                }
            }
        });

        Log.d(TAG,"Done!");
        has_Initialized = true;
    }

    public void Destroy() {
        StopListening();
        speechRecognizer.destroy();
        speechRecognizer = null;
        has_Initialized = false;
    }

    public void SetShouldGetPartialResults(boolean Value) {
        should_GetPartialResults = Value;
    }

    public void SetMaxResults(int MaxResults) {
        maxResults = MaxResults;
    }

    public void SetLanguage (String Language) {
        language = Language;
    }

    public void SetCallbacks(ISpeechRecognizer Callbacks) {
        callbacks = Callbacks;
    }

    public void StartListening() {
        if (!has_Initialized) {
            Log.e(TAG,"Tried to start listening, but SpeechToText hasn't been initialized yet. Did you forget to call Initialize()?");
            return;
        }
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                speechRecognizer = SpeechRecognizer.createSpeechRecognizer(getActivity());
                speechRecognizer.setRecognitionListener(speechRecognizerListener);

                Intent intent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
                intent.putExtra(RecognizerIntent.EXTRA_LANGUAGE_MODEL, RecognizerIntent.LANGUAGE_MODEL_FREE_FORM);
                intent.putExtra(RecognizerIntent.EXTRA_PARTIAL_RESULTS, should_GetPartialResults);
                intent.putExtra(RecognizerIntent.EXTRA_LANGUAGE, language);
                intent.putExtra(RecognizerIntent.EXTRA_MAX_RESULTS, maxResults);
                try {
                    speechRecognizer.startListening(intent);
                } catch (ActivityNotFoundException anfe) {
                    Log.e(TAG, "Fatal error while starting the speech recognizer: activity not found. -> "+anfe);
                }
            }
        });
    }

    public void StopListening() {
        if (!has_Initialized) {
            Log.e(TAG,"Tried to stop listening, but SpeechToText hasn't been initialized yet. Did you forget to call Initialize()?");
            return;
        }
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                speechRecognizer.stopListening();
            }
        });
    }

    //---------------------------------------------------------------------------------------------- ANDROID OVERRIDES

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        Log.d(TAG,"Speech To Text OnCreate");
    }
}