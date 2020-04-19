using UnityEngine;
using TMPro;

public class StatusHUD : MonoBehaviour
{
    public TextMeshProUGUI ElapsedTimeValue;
    public TextMeshProUGUI BystandersRescuedValue;
    public TextMeshProUGUI SharksKilledValue;
    public TextMeshProUGUI BystandersKilledValue;

    void Update()
    {
        int elapsedTimeSec = Mathf.RoundToInt(GameController.instance.ElapsedTime);
        ElapsedTimeValue.text = elapsedTimeSec.ToString();
        BystandersRescuedValue.text = GameController.instance.RescuedBystanders.ToString();
        SharksKilledValue.text = GameController.instance.SharksKilled.ToString();
        BystandersKilledValue.text = GameController.instance.KilledBystanders.ToString();
    }
}
