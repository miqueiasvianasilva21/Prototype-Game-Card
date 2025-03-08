using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
/// <summary>
/// Exibe as cartas do deck no DeckScene
/// Possui a fun��o para remover uma carta
/// Atualiza a tela sempre que ocorre uma altera��o no deck
/// </summary>
public class DeckUIManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab da carta
    public Transform deckPanel;  // Painel para exibir as cartas

    private void OnEnable()
    {
        // Adiciona o ouvinte para o evento de altera��o do deck
        Deck.Instance.OnDeckChanged += DisplayDeck;
    }

    private void OnDisable()
    {
        // Remove o ouvinte
        Deck.Instance.OnDeckChanged -= DisplayDeck;
    }

    private void Start()
    {
        DisplayDeck();
    }

    private void DisplayDeck()
    {
        
        foreach (Transform child in deckPanel)
        {
            Destroy(child.gameObject);
        }

        List<Card> listCards = Deck.Instance.GetCards();
        foreach (Card card in listCards)
        {
            GameObject cardUI = Instantiate(cardPrefab, deckPanel);
            DisplayDeckCard displayCard = cardUI.GetComponent<DisplayDeckCard>();
            if (displayCard != null)
            {
                displayCard.SetCard(card);
            }
        }
    }

    public void RemoveCardFromDeck(Card cardToRemove)
    {
        Deck.Instance.RemoveCard(cardToRemove);
    }

    
}
