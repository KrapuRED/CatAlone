using UnityEngine;
using UnityEngine.UI;

public class ControllerStatusMeowBarUI : MonoBehaviour
{
    [SerializeField] private Slider meowBar;

    public void CalculateMeowBar(int playerScore, int neighbourScore)
    {
        float diffranceScore = (float)playerScore / (playerScore + neighbourScore);

        meowBar.value = diffranceScore;
    }
}
