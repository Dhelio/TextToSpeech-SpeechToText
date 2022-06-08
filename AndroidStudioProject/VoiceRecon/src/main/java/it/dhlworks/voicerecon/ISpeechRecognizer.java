package it.dhlworks.voicerecon;

public interface ISpeechRecognizer {

    public void OnResult(String Result);
    public void OnPartialResult(String PartialResult);
    public void OnError(int ErrorCode);
    public void OnReadyForSpeech();
    public void OnBeginningOfSpeech();
    public void OnRmsChanged(float Value);
    public void OnBufferReceived(byte[] Buffer);
    public void OnEndOfSpeech();
    public void OnEvent(int i);

}
