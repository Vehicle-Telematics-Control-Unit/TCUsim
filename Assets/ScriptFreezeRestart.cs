using UnityEngine;

public class ScriptFreezeRestart : MonoBehaviour
{
    public float freezeThreshold = 5.0f; // Freeze threshold in seconds
    public float checkInterval = 1.0f; // Interval to check for freeze

    private float freezeTimer;
    private bool isFrozen;

    private void Start()
    {
        freezeTimer = 0.0f;
        isFrozen = false;

        InvokeRepeating("CheckScriptFreeze", checkInterval, checkInterval);
    }

    private void Update()
    {
        if (isFrozen)
        {
            // Perform necessary cleanup and restart the game
            RestartGame();
        }
    }

    private void CheckScriptFreeze()
    {
        if (Time.timeScale == 0.0f)
        {
            // The script is frozen
            freezeTimer += checkInterval;
        }
        else
        {
            // The script is running
            freezeTimer = 0.0f;
        }

        if (freezeTimer >= freezeThreshold)
        {
            // The script has been frozen for longer than the threshold
            isFrozen = true;
        }
    }

    private void RestartGame()
    {
        // Perform necessary cleanup and restart the game
        // This could involve reloading scenes, resetting player progress, etc.
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}