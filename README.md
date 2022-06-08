# TextToSpeech-SpeechToText
A Unity plugin for Android to handle realtime TTS and STT.

## Introduction

This repo contains the code to realize a plugin for Unity Android for using native Text-To-Speech and Speech-To-Text functionalitites.

For the TTS portion, it's really simple: it just provides hooks to Android native functionalities inside Unity.

For the STT portion the matter is a bit more complex. Since STT needs to be executed inside an activity, I've opted to use it in a viewless fragment inside Unity's activity, so that the two processes are linked together.

Further descriptions are available inside the doxygen doc in the Unity project.
