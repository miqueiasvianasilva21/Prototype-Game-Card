using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Classe responsável por gerenciar a cópia do Deck que é gerada para jogar
/// Exibe as cartas no painel de deck
/// Chama a função de comprar carta
/// Remove a carta comprada da lista de cartas
/// </summary>
public class PlayDeckManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab da carta
    public Transform playDeckPanel;// Onde as cartas aparecem para serem compradas
    private List<Card> remainingCards; // Cartas que ainda não foram compradas



    private void Start()
    {
        remainingCards = new List<Card>(Deck.Instance.GetCards());
        DisplayPlayDeck();

    }

    private void DisplayPlayDeck() // Exibe as cartas no painel de deck no canto direito
    {
        foreach (Transform child in playDeckPanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Card card in remainingCards)
        {
            GameObject cardUI = Instantiate(cardPrefab, playDeckPanel);
            PlayCardManager displayCard = cardUI.GetComponent<PlayCardManager>();
            if (displayCard != null)
            {
                displayCard.SetCard(card);
            }
        }
    }
    public Card DrawCard()
    {
        if (remainingCards.Count > 0) // Verifica se ainda há cartas para comprar
        {
            Card drawnCard = remainingCards[0]; // Pega a primeira carta da lista
            remainingCards.RemoveAt(0); // Remove da lista temporária (mas não do Singleton do Deck)
            DisplayPlayDeck(); // Atualiza a exibição do deck
            return drawnCard;
        }
        return null;



    }
}