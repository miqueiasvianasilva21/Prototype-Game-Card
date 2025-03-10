using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ShuffledPlayerDeck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private List<Card> shuffledCards;
    //public void Start()
    //{
    //    ShufflePlayerDeck();
    //}
    public List<Card> ShufflePlayerDeck()
    {
        List<Card> auxListCard = new List<Card>(Deck.Instance.GetCards());
        shuffledCards = new List<Card>();
        int deckLenght = Deck.Instance.GetLength();
        int cardsRemaining = auxListCard.Count;
        int rand = 0;
       
        for (int i = 0; i < deckLenght; i++)
        {
            rand = Random.Range(0, cardsRemaining);
            shuffledCards.Add(auxListCard[rand]);
            auxListCard.RemoveAt(rand);
            cardsRemaining = auxListCard.Count;
        }
        return shuffledCards;
        
        
    }

    // Update is called once per frame
    
}
