using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Classe que controla o jogo
/// Define os HPs iniciais do jogador e inimigo
/// Verifica se um HP chegou à zero e define um vencedor
/// Aplica dano ao HP do jogador ou inimigo
/// </summary>
public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get; private set; }
    private float playerHP = 4000;
    private float enemyHP = 4000;
    public const float maxHP = 4000;
    public TextMeshProUGUI playerHPText;
    public TextMeshProUGUI enemyHPText;
    public Image playerHPImage;
    public Image enemyHPImage;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayerTakeDamage(int damage) 
    {
        playerHP = playerHP - damage;
        Debug.Log("PlayerHP: " + playerHP);
        UpdatePlayerHP();
        EnemyWins();
    }
    public void EnemyTakeDamage(int damage)
    {
        enemyHP = enemyHP - damage;
        Debug.Log("EnemyHP: " + enemyHP);
        UpdateEnemyHP();
        PlayerWins();
    }
    public void UpdatePlayerHP() // Atualiza o texto e a imagem do HP do player
    {
        playerHPText.text = playerHP.ToString();
        playerHPImage.fillAmount = playerHP / maxHP;
    }
    public void UpdateEnemyHP()// Atualiza o texto e a imagem do HP do inimigo
    {
        enemyHPText.text = enemyHP.ToString();
        enemyHPImage.fillAmount = enemyHP / maxHP;
        
    }

    public void PlayerWins()
    {
        if (enemyHP <= 0)
        {
            SceneLoader sceneLoader = FindFirstObjectByType<SceneLoader>();
            sceneLoader.LoadVictoryScene();

        }
    }
    public void EnemyWins()
    {
        if (playerHP <= 0)
        {
            Debug.Log("Enemy Wins");
            SceneLoader sceneLoader = FindFirstObjectByType<SceneLoader>();
            sceneLoader.LoadGameOverScene();
        }
    }
}
