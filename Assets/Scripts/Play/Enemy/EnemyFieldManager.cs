using UnityEngine;


/// <summary>
/// Classe que gerencia o campo do inimigo
/// Como o jogo s� tem fun��o de ataque, possui uma fun��o que faz todas as cartas do inimigo atacarem
/// </summary>
public class EnemyFieldManager : MonoBehaviour
{
   
    public Transform fieldPanel;
    
    public void AttackAllCards()
    {
        EnemyHandManager enemyHandManager = FindFirstObjectByType<EnemyHandManager>();
        foreach (Transform cardTransform in fieldPanel)
        {
           
            EnemyCardManager card = cardTransform.GetComponent<EnemyCardManager>();
            Debug.Log("Nome da carta: " + card.GetCard().Name);
            enemyHandManager.AttackCard(card);
            
        }
    }
}
