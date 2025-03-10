using System;
using UnityEngine;

/// <summary>
/// Classe que gerencia a m�o do inimigo
/// Fun��o para comprar uma carta para a m�o do inimigo
/// Fun��o para adicionar uma carta � m�o do inimigo
/// Fun��o para atacar uma carta do jogador ou atac�-lo diretamente
/// 
/// </summary>
public class EnemyHandManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab da carta
    public Transform enemyHandPanel; // Painel da m�o do inimigo
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
        if (enemyHandPanel.childCount > 0) // Verifica se h� cartas na m�o
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
            Debug.Log("Inimigo n�o tem cartas para invocar.");
        }
    }

    public void AttackCard(EnemyCardManager enemyCard)
    {
        PlayerFieldManager playerFieldManager = FindFirstObjectByType<PlayerFieldManager>();
        GameManager gameManager = FindFirstObjectByType<GameManager>();

        if (playerFieldManager != null)
        {
            PlayCardManager cardLowerAttack = playerFieldManager.CardWithLowerAttack();// Obt�m a carta com menor ataque no campo do jogador

            if (cardLowerAttack != null)
            {
                Debug.Log("Card no campo: " + cardLowerAttack.GetCard().Name);
                int lowerAttack = cardLowerAttack.GetCard().Attack;
                int attackEnemyCard = enemyCard.GetCard().Attack;// obt�m o ataque da carta
                Debug.Log("O menor ataque �: " + lowerAttack);

                if (lowerAttack < attackEnemyCard) // Verifica se o ataque da carta do inimigo � maior do que o ataque da carta d� player
                {
                    Debug.Log("A carta entrou em conflito: " + cardLowerAttack.GetCard().Name);
                    cardLowerAttack.DestroyCard();



                    int damage = attackEnemyCard - lowerAttack;// a diferen�a entre os ataques � deduzida do HP do player
                   
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
            else// Se n�o h� cartas no campo do jogador ele recebe um ataque direto
            {
                gameManager.PlayerTakeDamage(enemyCard.GetCard().Attack);

            }
        }
        else
        {
            Debug.Log("Player FieldManager n�o encontrado");
        }
    }



}
