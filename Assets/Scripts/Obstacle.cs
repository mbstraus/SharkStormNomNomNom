using UnityEngine;

public class Obstacle : MonoBehaviour
{
    internal bool StopMoving = false;

    void Update()
    {
        if (GameController.instance.IsGameOver || StopMoving)
        {
            return;
        }
        float moveSpeed = 1.5f;
        if (GameSettingsManager.instance != null)
        {
            moveSpeed = GameSettingsManager.instance.GetSwimSpeed();
        }
        transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
    }
}
