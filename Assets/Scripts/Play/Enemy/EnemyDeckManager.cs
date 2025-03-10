using UnityEngine;
using System.Collections.Generic;


/// <summary>
///  Classe que gerencia o deck do inimigo
///  Instancia um deck
///  Exibe o deck do inimigo na tela
///  Define a função de comprar uma carta
/// </summary>
public class EnemyDeckManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab da carta
    public Transform enemyDeckPanel; // Painel do deck do inimigo

    private void Start() // inicialização do deck inimigo
    {
        StarterEnemyDeck starterEnemyDeck = FindFirstObjectByType<StarterEnemyDeck>();
        starterEnemyDeck.StartEnemyDeck();
        DisplayEnemyDeck();

    }

    public void DisplayEnemyDeck() // Exibe as cartas no painel de deck do inimigo
    {
        // Limpar o painel antes de exibir as cartas novamente
        foreach (Transform child in enemyDeckPanel)
        {
            Destroy(child.gameObject);
        }

        List<Card> enemyCards = EnemyDeck.Instance.GetCards(); // Obtém as cartas do Singleton do inimigo
        foreach (Card card in enemyCards)
        {
            GameObject cardUI = Instantiate(cardPrefab, enemyDeckPanel);
            EnemyCardManager displayCard = cardUI.GetComponent<EnemyCardManager>();
            if (displayCard != null)
            {
                displayCard.SetCard(card, false); // false significa que as cartas estão viradas pra baixo
            }
        }
    }

    public Card DrawCard()// Essa função é chamada no EnemyHandManager para comprar uma carta
    {
        if (TurnManager.Instance.CanDrawCard(false) && enemyDeckPanel.childCount>0) // false significa que é o turno do inimigo
        {
            
            Card drawnCard = EnemyDeck.Instance.GetCards()[0];
            EnemyDeck.Instance.RemoveCard(drawnCard);
            DisplayEnemyDeck();
            TurnManager.Instance.DrawCard(false); // Marca que o inimigo já comprou
            return drawnCard;
        }
        else
        {
            Debug.Log("O inimigo já comprou uma carta neste turno!");
            return null;
        }
    }

}
