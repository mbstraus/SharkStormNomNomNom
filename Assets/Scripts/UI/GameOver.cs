using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public TextMeshProUGUI ElapsedTimeValue;
    public TextMeshProUGUI BystandersRescuedValue;
    public TextMeshProUGUI SharksKilledValue;
    public TextMeshProUGUI BystandersKilledValue;

    private void OnEnable()
    {
        int elapsedTimeSec = Mathf.RoundToInt(GameController.instance.ElapsedTime);
        ElapsedTimeValue.text = elapsedTimeSec.ToString();
        BystandersRescuedValue.text = GameController.instance.RescuedBystanders.ToString();
        SharksKilledValue.text = GameController.instance.SharksKilled.ToString();
        BystandersKilledValue.text = GameController.instance.KilledBystanders.ToString();
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
