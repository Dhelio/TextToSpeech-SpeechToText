package it.dhlworks.voicerecon;

public class RecognitionModeUtils {
    public static RecognitionMode GetRecognitionModeFromInt(int Value) {
        switch (Value) {
            case 0:
                return RecognitionMode.RAW;
            case 1:
                return RecognitionMode.JSON;
            case 2:
                return RecognitionMode.PLAINTEXT;
            default:
                return RecognitionMode.RAW;
        }
    }
}