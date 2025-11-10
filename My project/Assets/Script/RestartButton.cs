using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    [Header("UI References")]
    public Button restartButton;

    void Start()
    {
        Debug.Log("🧠 RestartButton script initialized.");

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
            Debug.Log("✅ Restart listener added!");
        }
        else
        {
            Debug.LogWarning("⚠️ Restart button not assigned!");
        }
    }

    void RestartGame()
    {
        Debug.Log("🔄 Restarting game...");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
