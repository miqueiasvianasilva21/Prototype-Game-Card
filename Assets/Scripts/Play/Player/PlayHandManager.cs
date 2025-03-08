using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

/// <summary>
/// Classe responsável por gerenciar a mão do jogador
/// Compra uma carta para a mão do jogador
/// Adiciona essa carta no painel da mão do jogador
/// Invoca uma carta no campo do jogador e chama o PlayerFieldManager para adicionar a carta nele.
/// </summary>
public class PlayerHandManager : MonoBehaviour
{
    public GameObject cardPrefab; // PlayCardFrefab
    public Transform handPanel; // Painel da mão do jogador
    public PlayDeckManager playDeckManager; // Referência ao gerenciador de deck
    public Transform fieldPanel;// Painel do campo do jogador

    
    
    public void DrawCardToHand() // Compra uma carta para a mão do jogador por turno
    {
        if (TurnManager.Instance.CanDrawCard(true)) // true = turno do jogador
        {
            Card newCard = playDeckManager.DrawCard();
            if (newCard != null)
            {
                AddCardToHand(newCard);
                TurnManager.Instance.DrawCard(true); // Marca que o jogador já comprou
            }
        }
        else
        {
            Debug.Log("Você já comprou uma carta neste turno!");
        }
    }


    private void AddCardToHand(Card card) // Adiciona uma carta à mão do jogador
    {
        GameObject cardUI = Instantiate(cardPrefab, handPanel); 
        PlayCardManager displayCard = cardUI.GetComponent<PlayCardManager>();
        if (displayCard != null)
        {
            displayCard.SetCard(card, true);
            displayCard.canZoom = true;
                                                   
        }
    }
    public void PlayCardToField(GameObject cardUI) // Adiciona uma carta ao campo do jogador
    {
        if (cardUI.transform.parent == handPanel)
        {
            // Move a carta para o campo
            cardUI.transform.SetParent(fieldPanel, false);

            // Obtém o componente PlayCardManager
            PlayCardManager displayCard = cardUI.GetComponent<PlayCardManager>();
            if (displayCard != null)
            {
                displayCard.SetCard(displayCard.GetCard(), true);// o true representa que a carta está virada para cima
                displayCard.SetAttackMode(); // Ativar modo de ataque

                // Adicionar ao PlayerFieldManager
                PlayerFieldManager playerFieldManager = FindFirstObjectByType<PlayerFieldManager>();
                if (playerFieldManager != null)
                {
                    playerFieldManager.AddCardToField(displayCard);
                }
            }
        }
    }








}
