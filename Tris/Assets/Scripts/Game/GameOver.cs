using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    #region Variables
    public Color gameOver_Color;
    public float speedScale_Center;
    [Range(0f, 5f)] public float scaleMultiplier_Center;
    public Transform gameCenter;
    [Range(0f, 5f)] public float speedCenter_toCenter;
    [Range(0f, 5f)] public float speed_FadeOut;
    public int contextTest;

    [HideInInspector] public GameObject buttonAnimationCenter;
    [HideInInspector] public string endText;
    [HideInInspector] public string[,] game;
    [HideInInspector] public bool gameOver;
    private GameLogic gameLogic_Script;
    private List<GameObject> buttons;
    private int winCounter;
    #endregion

    #region Flow
    void Start()
    {
        gameLogic_Script = GetComponent<GameLogic>();   
        buttons = new List<GameObject>(gameLogic_Script.buttons);
        gameOver = false;
        winCounter = 0;
        game = new string[3, 3];
    }
    void Update()
    {
        if (gameOver)
        {
            // check raw
            CheckRawColumn(Check.Raw);

            // check column
            CheckRawColumn(Check.Column);

            // check diagonal
            CheckDiagonal(Check.DiagonalSx);

            CheckDiagonal(Check.DiagonalDx);

            if (gameOver && gameLogic_Script.buttons.Count == 0)
            {
                DisableButtons();
                endText = "Draw";
            }
            gameOver = false;
        }
    }
    #endregion

    #region Methods
    private void CheckRawColumn(Check check)
    {
        if (!gameOver)
            return;
        int dimension1 = 0;
        int dimension2 = 0;

        if (check == Check.Raw)
        {
            dimension1 = 0;
            dimension2 = 1;
        }          
        else if (check == Check.Column)
        {
            dimension1 = 1;
            dimension2 = 0;
        }
          
        for (int value1 = 0; value1 < game.GetLength(dimension1); value1++)
        {
            for (int value2 = 0; value2 < game.GetLength(dimension2) - 1; value2++)
                CheckWin(check, value1, value2, 0);
            winCounter = 0;
        }
    }
    private void CheckDiagonal(Check check)
    {
        if (!gameOver)
            return;
        int column = 0;
        int columnSign = 0;
        if (check == Check.DiagonalSx)
        {
            column = 0;
            columnSign = 1;
        }
        else if (check == Check.DiagonalDx)
        {
            column = 2;
            columnSign = -1;
        }

        for (int raw = 0; raw < 2; raw ++)
        {
            CheckWin(check, raw, column, columnSign);
            column += columnSign;
        }
        winCounter = 0;
    }
    private void CheckWin(Check check, int value1, int value2, int columnSign)
    {
        if (check == Check.Raw)
        {
            if (game[value1, value2] != null && game[value1, value2] == game[value1, value2 + 1])
                GameEnd(game[value1, value2]);
        }
        else if (check == Check.Column)
        {
            if (game[value2, value1] != null && game[value2, value1] == game[value2 + 1, value1])
                GameEnd(game[value2, value1]);
        }     
        else if (check == Check.DiagonalSx || check == Check.DiagonalDx)
        {
            if (game[value1, value2] != null && game[value1, value2] == game[value1 + 1, value2 + columnSign])
                GameEnd(game[value1, value2]);
        }
    }
    private void GameEnd(string value)
    {
        winCounter++;
        if (winCounter == 2)
        {
            if (value == "X")
                endText = "Player2 wins";
            else
                endText = "Player1 wins";
            DisableButtons();
        }
    }
    private void DisableButtons()
    {
        gameLogic_Script.buttonObject.transform.SetSiblingIndex(gameLogic_Script.buttonObject.transform.parent.childCount);
        buttonAnimationCenter = gameLogic_Script.buttonObject;
        gameOver = false;

        foreach (GameObject button in gameLogic_Script.buttons)
        {
            Button buttonComponent = button.GetComponent<Button>();
           
            ColorBlock colorBlock = buttonComponent.colors;
            colorBlock.disabledColor = gameOver_Color;
            buttonComponent.colors = colorBlock;
            buttonComponent.interactable = false;          
        }

        foreach (GameObject button in buttons)
        {
            ButtonInfo buttonInfoScript = button.GetComponent<ButtonInfo>();
            buttonInfoScript.enable = true;
        }
    }
    private void DebugGame()
    {
        Debug.Log("Win Counter: " + winCounter);
        for (int dim1 = 0; dim1 < game.GetLength(0); dim1++)
        {
            for (int dim2 = 0; dim2 < game.GetLength(1); dim2++)
                Debug.Log(game[dim1, dim2] + "\t" + dim1 + dim2);
        }
    }
    [ContextMenu("Test ContextMenu()")]
    private void ContextMenu()
    {
        // context menu -> attivo metodo con tasto dx. Le modifiche non vengono rilevate.
        // Quindi devo toccare almeno una volta i valori di inpector manualmente. Oppure utilizzo UnityEditor.SceneManagement.MarkSceneDirty
        // UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        // UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
        contextTest = Random.Range(0, 5);
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetSceneByName("Game"));
    }
    #endregion
    private enum Check
    {
        Raw,
        Column, 
        DiagonalSx,
        DiagonalDx,
    }
}
