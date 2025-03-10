using System;
using UnityEngine;

/// <summary>
/// Classe que gerencia a mão do inimigo
/// Função para comprar uma carta para a mão do inimigo
/// Função para adicionar uma carta à mão do inimigo
/// Função para atacar uma carta do jogador ou atacá-lo diretamente
/// 
/// </summary>
public class EnemyHandManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab da carta
    public Transform enemyHandPanel; // Painel da mão do inimigo
    public EnemyDeckManager enemyDeckManager;// Gerenciador do deck
    public Transform enemyFieldPanel;// Painel do campo do inimigo

    public void DrawCardToEnemyHand()
    {
        if (enemyDeckManager != null)
        {
            Card newCard = enemyDeckManager.DrawCard(); // Compra uma carta do deck
            if (newCard != null)
            {
                AddCardToEnemyHand(newCard);
            }
        }
    }

    private void AddCardToEnemyHand(Card card)
    {
        GameObject cardUI = Instantiate(cardPrefab, enemyHandPanel);
        EnemyCardManager displayCard = cardUI.GetComponent<EnemyCardManager>();
        if (displayCard != null)
        {
            displayCard.SetCard(card, false); // false  = Carta virada para baixo
        }
    }
    public void SummonCard()
    {
        if (enemyHandPanel.childCount > 0) // Verifica se há cartas na mão
        {
            Transform cardToSummon = enemyHandPanel.GetChild(0); // Pega a primeira carta
            cardToSummon.SetParent(enemyFieldPanel, false);// Move a carta para o campo


            EnemyCardManager displayCard = cardToSummon.GetComponent<EnemyCardManager>();
            displayCard.canZoom = true;
            if (displayCard != null)
            {
                displayCard.SetCard(displayCard.GetCard(), true); // Revela a carta
                
            }

            Debug.Log("Inimigo invocou " + displayCard.GetCard().Name);
        }
        else
        {
            Debug.Log("Inimigo não tem cartas para invocar.");
        }
    }

    public void AttackCard(EnemyCardManager enemyCard)
    {
        PlayerFieldManager playerFieldManager = FindFirstObjectByType<PlayerFieldManager>();
        GameManager gameManager = FindFirstObjectByType<GameManager>();

        if (playerFieldManager != null)
        {
            PlayCardManager cardLowerAttack = playerFieldManager.CardWithLowerAttack();// Obtém a carta com menor ataque no campo do jogador

            if (cardLowerAttack != null)
            {
                Debug.Log("Card no campo: " + cardLowerAttack.GetCard().Name);
                int lowerAttack = cardLowerAttack.GetCard().Attack;
                int attackEnemyCard = enemyCard.GetCard().Attack;// obtém o ataque da carta
                Debug.Log("O menor ataque é: " + lowerAttack);

                if (lowerAttack < attackEnemyCard) // Verifica se o ataque da carta do inimigo é maior do que o ataque da carta dó player
                {
                    Debug.Log("A carta entrou em conflito: " + cardLowerAttack.GetCard().Name);
                    cardLowerAttack.DestroyCard();



                    int damage = attackEnemyCard - lowerAttack;// a diferença entre os ataques é deduzida do HP do player
                   
                    gameManager.PlayerTakeDamage(damage);// reduz o hp do player
                }
                else if(attackEnemyCard == lowerAttack)
                {
                    cardLowerAttack.DestroyCard();
                    enemyCard.DestroyCard();
                }
                else
                {
                    Debug.Log("Inimigo resistiu ao ataque");
                }
            }
            else// Se não há cartas no campo do jogador ele recebe um ataque direto
            {
                gameManager.PlayerTakeDamage(enemyCard.GetCard().Attack);

            }
        }
        else
        {
            Debug.Log("Player FieldManager não encontrado");
        }
    }



}
