using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Net net_Script;
    public ScriptableImage_Modifier alpha_Modifier;
    public ScriptableTransform_Modifier position_Modifier;
    public ScriptableTransform_Modifier scale_Modifier;
    public List<GameObject> buttons = new List<GameObject>();
    [SerializeField] private Color player1_Color;
    [SerializeField] private Color player2_Color;
    [SerializeField, Range(0f, 1f)] private float additiveWhite_Multiplyer;

    [HideInInspector] public GameObject buttonObject;

    private GameOver gameOver_Script;
    private Round round;
    private Color currentColor;
    private Color colorMultiplier_highlighted;
    private Color colorMultiplyer_normal;
    private string simbol;
    private bool cpuMove;

    private void Start()
    {
        gameOver_Script = GetComponent<GameOver>();
        colorMultiplier_highlighted = (new Color(1f, 1f, 1f, 1f / additiveWhite_Multiplyer) * additiveWhite_Multiplyer);
        colorMultiplyer_normal = (new Color(1f, 1f, 1f, 1f / (additiveWhite_Multiplyer/2f)) * (additiveWhite_Multiplyer / 2f));
        int random = Random.Range(0,2);
        switch(random)
        {
            case 0:
                round = Round.Player1;
                break;
            case 1:
                round = Round.Player2;
                break;
        }
        ChangeRound();
    }


    private void Update()
    {
        Debug.Log(cpuMove);
        if (Input.GetMouseButtonUp(0))
        {
            if (EventSystem.current.currentSelectedGameObject != null)
                EventSystem.current.SetSelectedGameObject(null);
        }

        if (GameMode.gameMode == Mode.Single && round == Round.Player2 && cpuMove)
        {
            net_Script.RequestSetUp(Operation.NEXT_MOVE);
            cpuMove = false;
        }
    }
    public void ButtonClick()
    {
        if (GameMode.gameMode == Mode.Multi || (GameMode.gameMode == Mode.Single && round == Round.Player1))
        {
            buttonObject = EventSystem.current.currentSelectedGameObject.gameObject;
            Click(buttonObject);
        }    
    }
    public void Click(GameObject but)
    {
        buttonObject = but;
        Text text = buttonObject.GetComponentInChildren<Text>();
        Button button = buttonObject.GetComponent<Button>();
        ButtonInfo buttonInfo = buttonObject.GetComponent<ButtonInfo>();

        text.text = simbol;
        button.interactable = false;
        buttonObject.GetComponent<Transform_Modifier>().enabled = true;
        buttons.Remove(buttonObject);
        gameOver_Script.game[buttonInfo.raw, buttonInfo.column] = simbol;
        gameOver_Script.gameOver = true;

        if (GameMode.gameMode == Mode.Single)
            net_Script.RequestSetUp(Operation.RESULT);
        else
            ChangeRound();
    }
    public void ChangeRound()
    {
        switch (round)
        {
            case Round.Player1:
                GameInfo("X", Round.Player2, player2_Color);
                cpuMove = true;
                break;
            case Round.Player2:
                GameInfo("0", Round.Player1, player1_Color);
                net_Script.cpuStatus.GetComponent<Text>().text = "CPU off";
                break;
        }
        ChangeColor(currentColor);
    }
    public void ChangeColor(Color color)
    {       
        foreach (GameObject button in buttons)
        {
            Button buttonComponent = button.GetComponent<Button>();
            ColorBlock colorBlock = buttonComponent.colors;
            colorBlock.normalColor = currentColor * colorMultiplyer_normal;
            colorBlock.highlightedColor = currentColor * colorMultiplier_highlighted;
            colorBlock.pressedColor = color;
            colorBlock.disabledColor = color;
            buttonComponent.colors = colorBlock;
        }
    }
    private void GameInfo(string currentSimbol, Round currentRound, Color color)
    {
        simbol = currentSimbol;
        round = currentRound;
        currentColor = color;
    }
}

public enum Round
{
    Player1,
    Player2,
}
