using UnityEngine;
using System.Collections;
//
using System.Xml;
using System.Collections.Generic;


using UnityEngine.UI;
/*

    Import AIML files within the Resources

*/

public class ChatWindowExampleMobileWeb : MonoBehaviour
{
    
    private TextAsset[] aimlFiles;
    private List<string> aimlXmlDocumentListFileName = new List<string>();
    private List<XmlDocument> aimlXmlDocumentList = new List<XmlDocument>();
    //
    private TextAsset GlobalSettings, GenderSubstitutions, Person2Substitutions, PersonSubstitutions, Substitutions, DefaultPredicates, Splitters;
    //
    private ChatbotMobileWeb bot;

    public Text robotOutput;

    // 
    public string answer="";
   
    

    // Use this for initialization
    void Start()
    {
        bot = new ChatbotMobileWeb();
        LoadFilesFromConfigFolder();
        bot.LoadSettings(GlobalSettings.text, GenderSubstitutions.text, Person2Substitutions.text, PersonSubstitutions.text, Substitutions.text, DefaultPredicates.text, Splitters.text);
        TextAssetToXmlDocumentAIMLFiles();
        bot.loadAIMLFromXML(aimlXmlDocumentList.ToArray(), aimlXmlDocumentListFileName.ToArray());
        bot.LoadBrain();
    }


    // Update is called once per frame
    void Update()
    {


    }


    /// <summary>
    /// Button to send the question to the robot
    /// </summary>
    public string SendQuestionToRobot(string userQuestion)
    {

        ChatbotMobileWeb bot1 = bot;
        if (!userQuestion.Equals(""))
        {
            // Response Bot AIML
            var x = bot1.getOutput(userQuestion);
            // Response BotAIml in the Chat window
            robotOutput.text = x;
            answer = x;
            Debug.Log("Inside the sendquestionto robot function");
            // for empty the voice input
      
        }
        else
        {
            answer = "Sorry can you repete that again...";
        }

        Debug.Log("Answer of question:" + answer);

        return answer;
    }


    void LoadFilesFromConfigFolder()
    {
        GlobalSettings = Resources.Load<TextAsset>("config/Settings");
        GenderSubstitutions = Resources.Load<TextAsset>("config/GenderSubstitutions");
        Person2Substitutions = Resources.Load<TextAsset>("config/Person2Substitutions");
        PersonSubstitutions = Resources.Load<TextAsset>("config/PersonSubstitutions");
        Substitutions = Resources.Load<TextAsset>("config/Substitutions");
        DefaultPredicates = Resources.Load<TextAsset>("config/DefaultPredicates");
        Splitters = Resources.Load<TextAsset>("config/Splitters");
    }

    void TextAssetToXmlDocumentAIMLFiles()
    {
        aimlFiles = Resources.LoadAll<TextAsset>("aiml");
        foreach (TextAsset aimlFile in aimlFiles)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(aimlFile.text);
                aimlXmlDocumentListFileName.Add(aimlFile.name);
                aimlXmlDocumentList.Add(xmlDoc);
            }catch(System.Exception e)
            {
                Debug.LogWarning(e.ToString());
            }
        }
    }


    void OnDisable()
    {
        bot.SaveBrain();
    }

   

}
