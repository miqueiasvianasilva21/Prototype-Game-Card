using UnityEngine;


/// <summary>
/// Classe que gerencia o campo do inimigo
/// Como o jogo só tem função de ataque, possui uma função que faz todas as cartas do inimigo atacarem
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
