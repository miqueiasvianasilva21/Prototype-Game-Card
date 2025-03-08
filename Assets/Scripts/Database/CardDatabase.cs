using System.Collections.Generic;
using UnityEngine;


public class CardDatabase : MonoBehaviour
{
    public List<Card> CardCollection = new List<Card>();

    private void Awake()
    {
        CardCollection.Add(new Card(10, "Dragão", 1000, "Um dragão feroz!", "dragao_flamejante"));
        CardCollection.Add(new Card(20, "Feiticeiro", 800, "Um mago das trevas!", "feiticeiro_fogo"));
        CardCollection.Add(new Card(30, "Golem", 1200, "Criatura feita de pedra!", "golem_fogo"));
    }
    public List<Card> GetAllCards()
    {
        return CardCollection;
    }
}
