using UnityEngine;

/// <summary>
/// Aqui é criado o Deck inicial
/// Mesmo que o jogador remova todas as cartas do Deck ele sempre será essas três cartas
/// </summary>
public class StarterDeck : MonoBehaviour
{
   
    private void Start()
    {
        // Adicionando algumas cartas para teste
        if (Deck.Instance.GetLength() == 0)
        {
            Deck.Instance.AddCard(new Card(1, "Dragão", 1000, "Um dragão feroz!", "dragao_flamejante"));
            Deck.Instance.AddCard(new Card(2, "Feiticeiro", 800, "Um mago das trevas!", "feiticeiro_fogo"));
            Deck.Instance.AddCard(new Card(3, "Golem", 1200, "Criatura feita de pedra!", "golem_fogo"));
        }

    }
}
