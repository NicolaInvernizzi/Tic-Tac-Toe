using UnityEngine;

public class PauseAnimation : MonoBehaviour
{
    #region Variables
    [SerializeField] private Transform_Modifier movePosition_Script;
    [SerializeField] private GameOver gameOver_Script;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameMenu;

    [HideInInspector] public GameObject endPosition = null;
    [HideInInspector] public bool enable;
    #endregion

    #region Flow
    void Update()
    {
        if (enable)
        {
            if (endPosition == null)
                endPosition = gameOver_Script.buttonAnimationCenter;

            movePosition_Script.stop = endPosition.transform.localPosition;

            if (transform.position == endPosition.transform.localPosition)
            {

                enable = false;
            }
                
        }
    }
    #endregion
}
