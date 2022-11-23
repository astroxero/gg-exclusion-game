using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TargetPlayer : MonoBehaviour
{

    public float health = 50f;
    public TextMeshProUGUI hText;

    public void takeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Update()
    {
        hText.text = health.ToString();
    }

    void Die()
    {
        SceneManager.LoadScene("DeathScene");
    }
}
