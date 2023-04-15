using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameLogic gameLogic_Script;
    [SerializeField] private GameOver gameOver_Script;
    public int raw;
    public int column;
    public GameObject endPosition;
    [HideInInspector] public bool enable;
    public int cpuNumber;

    private bool center;
    private Transform_Modifier transform_Modifier;
    #endregion

    #region Flow
    private void Update()
    {
        if (enable)
        {
            Button buttonComponent = gameObject.GetComponent<Button>();

            // collect end position from gameOver script
            endPosition = gameOver_Script.buttonAnimationCenter;

            // find center & setUp
            if (transform.localPosition == endPosition.transform.localPosition)
                CenterSetUp();

            // activate fade out unused buttons
            if (!center && buttonComponent.colors.disabledColor == gameOver_Script.gameOver_Color)
                ScriptableManager.Image_ModifierSetUp(gameLogic_Script.alpha_Modifier, this.gameObject);
            // move used buttons
            else
            {
                transform_Modifier = ScriptableManager.Transform_ModifierSetUp(gameLogic_Script.position_Modifier, this.gameObject);
                if (center)
                    transform_Modifier.stop = gameOver_Script.gameCenter.localPosition;
                else
                {
                    transform_Modifier.useTransform_Stop = true;
                    transform_Modifier.stop_Transform = gameOver_Script.buttonAnimationCenter.transform;
                }
            }
            enable = false;
        }
    }
    #endregion

    #region Methods
    private void CenterSetUp()
    {
        ScriptableManager.Transform_ModifierSetUp(gameLogic_Script.scale_Modifier, this.gameObject);
        Text text = transform.GetComponentInChildren<Text>();
        Button buttonComponent = transform.GetComponent<Button>();
        text.text = gameOver_Script.endText;
        text.fontSize = 22;

        if (text.text == "Draw")
        {
            ColorBlock colorBlock = buttonComponent.colors;
            colorBlock.disabledColor = gameOver_Script.gameOver_Color;
            buttonComponent.colors = colorBlock;
        }
        center = true;
        enable = false;
    }
    #endregion
}
