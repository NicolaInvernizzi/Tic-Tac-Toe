using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Net : MonoBehaviour
{
    /* JSON
     * key:value
     * 
     * Es. Next move
     * {
	     "status":  "success",
	     "message":  "",
       	 "data":  {
		        "nextMove":  5
	         }
       }
     * Es. Result
     * {
	     "status":  "success",
	     "message":  "",
       	 "data":  {
		        "result":  "x"
	         }
       }
    */
    [SerializeField] private GameOver gameOver_Script;
    [SerializeField] private GameLogic gameLogic_Script;
    public Text cpuStatus;
    public Text callStatus;
    private string url_api = "https://twinwolves-studio.com/api/tictactoe/";
    private string url_next = "nextmove.php";
    private string url_result = "result.php";
    private string header_name = "auth";
    private string header_value = "0d18e169-1708-4f25-b0da-e2c6434bc3af";
    public NextMove response_nextMove { get; private set; }
    public Result response_result { get; private set; }
    private void Start()
    {
        cpuStatus.GetComponent<Text>().text = "CPU off";
    }

    // set up the URL and the web request. Finally start GetRequest coroutine
    public void RequestSetUp(Operation operation)
    {
        string url;
        switch (operation)
        {
            case Operation.NEXT_MOVE:
                url = url_api + url_next; break;
            default:
                url = url_api + url_result; break;
        }

        // create url
        url = url + "?field=" + CreateField();
        UnityWebRequest webRequest = new UnityWebRequest(url);

        // define a RequestHeader in order to make a web request using the correct token
        webRequest.SetRequestHeader(header_name, header_value);

        // downloadHandler process the response body
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        StartCoroutine(GetRequest(webRequest, operation));
    }
    IEnumerator GetRequest(UnityWebRequest webRequest, Operation operation)
    {
        cpuStatus.GetComponent<Text>().text = "CPU on";
        yield return new WaitForSeconds(0.5f);
        // send request
        yield return webRequest.SendWebRequest();

        // handle webRequest result
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                callStatus.GetComponent<Text>().text = "Success";
                Parse(webRequest, operation);
                yield break;
            case UnityWebRequest.Result.InProgress:
                callStatus.GetComponent<Text>().text = "InProgress";
                break;
            case UnityWebRequest.Result.ConnectionError:
                callStatus.GetComponent<Text>().text = "ConnectionError";
                break;
            case UnityWebRequest.Result.ProtocolError:
                callStatus.GetComponent<Text>().text = "ProtocolError";
                break;
            case UnityWebRequest.Result.DataProcessingError:
                callStatus.GetComponent<Text>().text = "DataProcessingError";
                break;
        }
    }

    // parse web request result into a GameStatus in order to access data JSON object
    private void Parse(UnityWebRequest webRequest, Operation operation)
    {
        string jsonResponse = ((DownloadHandlerBuffer)webRequest.downloadHandler).text;
        switch (operation)
        {
            case Operation.NEXT_MOVE:
                response_nextMove = JsonUtility.FromJson<NextMove>(jsonResponse);
                response_nextMove.Print();
                FindButtonCPU();
                break;
            default:
                response_result = JsonUtility.FromJson<Result>(jsonResponse);
                response_result.Print();
                if (response_result.data.result == "_")
                    gameLogic_Script.ChangeRound();
                break;
        }
    }
    public void FindButtonCPU()
    {
        List<GameObject> buttons = new List<GameObject>(gameLogic_Script.buttons);
        foreach (GameObject button in buttons)
        {
            ButtonInfo buttonInfo_Script = button.GetComponent<ButtonInfo>();
            if (buttonInfo_Script.cpuNumber == response_nextMove.data.nextMove)
                gameLogic_Script.Click(button);
        }
    }

    // create the field in the correct sintax for the web request reading the game matrix
    private string CreateField()
    {
        string field = "";
        string[,] game = (string[,])gameOver_Script.game.Clone();
        for (int raw = 0; raw < game.GetLength(0); raw++)
        {
            for (int column = 0; column < game.GetLength(1); column++)
            {
                switch(game[raw, column])
                {
                    case "X":
                        field += "1"; break;
                    case "0":
                        field += "0"; break;
                    default:
                        field += "_"; break;
                }
            }    
        }
        return field;
    }
}
public enum Operation
{
    NEXT_MOVE,
    RESULT
}
