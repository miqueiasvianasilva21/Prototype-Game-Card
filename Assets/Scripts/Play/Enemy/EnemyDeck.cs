using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Classe que representa a instância do deck do inimigo
/// </summary>
public class EnemyDeck : MonoBehaviour
{
    public static EnemyDeck Instance { get; private set; }
    private List<Card> enemyCards = new List<Card>();

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

    public void InitializeDeck(List<Card> cards)
    {
        enemyCards = new List<Card>(cards);
    }

    public List<Card> GetCards()
    {
        return enemyCards;
    }

    public int GetLength()
    {
        return enemyCards.Count;
    }

    public void RemoveCard(Card card)
    {
        enemyCards.Remove(card);
    }
    public void AddCard(Card card)
    {
        enemyCards.Add(card);
    }
}
