using UnityEngine;

/// <summary>
/// Aqui � criado o Deck inicial
/// Mesmo que o jogador remova todas as cartas do Deck ele sempre ser� essas tr�s cartas
/// </summary>
public class StarterDeck : MonoBehaviour
{
   
    private void Start()
    {
        // Adicionando algumas cartas para teste
        if (Deck.Instance.GetLength() == 0)
        {
            Deck.Instance.AddCard(new Card(1, "Drag�o", 1000, "Um drag�o feroz!", "dragao_flamejante"));
            Deck.Instance.AddCard(new Card(2, "Feiticeiro", 800, "Um mago das trevas!", "feiticeiro_fogo"));
            Deck.Instance.AddCard(new Card(3, "Golem", 1200, "Criatura feita de pedra!", "golem_fogo"));
        }

    }
}
