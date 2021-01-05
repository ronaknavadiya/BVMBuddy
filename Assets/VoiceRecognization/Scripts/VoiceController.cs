using System.Collections;
using System.Collections.Generic;
using TextSpeech;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;


public class VoiceController : MonoBehaviour
{
    
    public string messageFromUser;
    public ChatWindowExampleMobileWeb chatWindowRobot;

    const string LANG_CODE = "en-US";

    [SerializeField]
    Text userQuery, robotOutput;



    private void Start()
    {
        Setup(LANG_CODE);
        CheckPermission();

#if UNITY_ANDROID
        SpeechToText.instance.onPartialResultsCallback = OnPartialSpeechResult;
#endif
       
        SpeechToText.instance.onResultCallback = OnFinalSpeechResult;
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;

    }

    void CheckPermission()
    {
#if UNITY_ANDROID
        if(!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
#endif

    }

    #region Speech To Text

    public void StartListening()
    {
        SpeechToText.instance.StartRecording();
    }

    public void StopListening()
    {
        SpeechToText.instance.StopRecording();
    }

    void OnFinalSpeechResult(string result)
    {
        userQuery.text = result;

        getMessageFromRobot(result);
       
        
    }

    void OnPartialSpeechResult(string result)
    {
        userQuery.text = result;
    }

    #endregion


    #region Text To Speech

    public void StartSpeeking(string message)
    {
        TextToSpeech.instance.StartSpeak(message);
    }

    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }

    void OnSpeakStart()
    {
        Debug.Log("speaking Started...");
    }

    void OnSpeakStop()
    {
        Debug.Log("speaking Stopped...");
    }

    #endregion

   

    void Setup(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);

        SpeechToText.instance.Setting(code);
    }


    #region To get the robot answer
    public void getMessageFromRobot(string result)
    {
        messageFromUser = result;
        string answer = chatWindowRobot.SendQuestionToRobot(messageFromUser);

        robotOutput.text = answer;

        Debug.Log("Your result from robot:" + answer);

        StartSpeeking(answer);

    }

    #endregion

}
