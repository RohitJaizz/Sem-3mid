using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    private bool isDead = false;

    [Header("UI References")]
    public Image healthBarFill;      // Drag the INNER bar (not background)
    public GameObject restartPanel;  // Drag the restart panel here

    [Header("Smooth UI Settings")]
    public float smoothSpeed = 5f;   // Speed of bar animation

    private float targetFill;        // For smooth animation

    void Start()
    {
        currentHealth = maxHealth;
        targetFill = 1f;
        UpdateHealthUIInstant();

        if (restartPanel != null)
            restartPanel.SetActive(false);
    }

    void Update()
    {
        // Smoothly animate bar
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = Mathf.Lerp(
                healthBarFill.fillAmount,
                targetFill,
                Time.unscaledDeltaTime * smoothSpeed
            );

            UpdateBarColor(); // also update color smoothly
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        targetFill = currentHealth / maxHealth;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Player Died!");
        Time.timeScale = 0f; // Pause the game

        if (restartPanel != null)
            restartPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateHealthUIInstant()
    {
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = currentHealth / maxHealth;
            UpdateBarColor();
        }
    }

    private void UpdateBarColor()
    {
        if (healthBarFill == null) return;

        float healthPercent = currentHealth / maxHealth;

        // Lerp from Red (low health) to Yellow (mid) to Green (full)
        Color low = Color.red;
        Color mid = Color.yellow;
        Color high = Color.green;

        if (healthPercent > 0.5f)
            healthBarFill.color = Color.Lerp(mid, high, (healthPercent - 0.5f) * 2f);
        else
            healthBarFill.color = Color.Lerp(low, mid, healthPercent * 2f);
    }
}
