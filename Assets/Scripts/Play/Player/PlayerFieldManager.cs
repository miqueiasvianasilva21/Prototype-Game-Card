using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Classe responsável por gerenciar o campo do jogador
/// </summary>
public class PlayerFieldManager : MonoBehaviour
{
    public Transform fieldPanel; // O painel onde as cartas do campo do jogador estão armazenadas
    private PlayCardManager selectedCard = null;// Classe que representa e gerencia uma carta do jogador
    private List<PlayCardManager> fieldCards = new List<PlayCardManager>(); // Lista de cartas no campo

    public PlayCardManager GetSelectedCard()
    {
        return selectedCard;
    }

    public void SelectCardToAttack(PlayCardManager card) // Define uma carta no campo do jogador como atacante
    {
        selectedCard = card;
        Debug.Log("Carta selecionada: " + card.GetCard().Name);
    }

    public void ResetAllCardsAttack() // Define que todas as cartas podem atacar, é chamada no ínicio de cada turno do jogador
    {
        foreach (PlayCardManager card in fieldCards)
        {
            card.ResetAttack();
        }
        Debug.Log("Todas as cartas podem atacar novamente!");
    }

    public PlayCardManager CardWithLowerAttack() // Retorna a carta com menor ataque do campo do jogador, o inimigo sempre ataca a carta com menor ataque
    {
        PlayCardManager cardLower = null;
        int lowerAttack = 100000;

        foreach (PlayCardManager card in fieldCards)
        {
            if (lowerAttack > card.GetCard().Attack)
            {
                cardLower = card;
                lowerAttack = card.GetCard().Attack;
            }
        }
        return cardLower;
    }

    public void RemoveCardFromField(PlayCardManager card)// Remove da lista de cartas no campo, 
    {
        if (fieldCards.Contains(card))
        {
            fieldCards.Remove(card); 
        }
    }

    public void AddCardToField(PlayCardManager card)// Adiciona à lista de cartas no campo
    {
        if (!fieldCards.Contains(card))
        {
            fieldCards.Add(card); 
        }
    }
}
