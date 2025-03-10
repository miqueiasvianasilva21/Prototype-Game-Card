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
        CardCollection.Add(new Card(40, "Mago", 1500, "Mago lendário", "feiticeiro"));
        CardCollection.Add(new Card(50, "Minotauro", 800, "Minotauro menor", "minotauro"));
        CardCollection.Add(new Card(60, "Golem", 1500, "Golem de pedra", "golem"));

    }
    public List<Card> GetAllCards()
    {
        return CardCollection;
    }
}
