using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dhlworks.voicerecon.texttospeech;

public class TTSDemo : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField textInputField;

    public void Read() {
        TextToSpeechManager.Instance.Say(textInputField.text);
    }
}
