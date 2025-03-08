using UnityEngine;

/// <summary>
/// Controla a exibição de todas as cartas do banco de dados na interface.
/// Instancia as cartas no painel correspondente e as exibe corretamente.
/// </summary>

public class DatabaseUIManager : MonoBehaviour
{
    public GameObject cardPrefab; // Prefab da carta para o banco de dados
    public Transform cardDatabasePanel;  // Painel onde as cartas do banco serão exibidas
    public CardDatabase cardDatabase; // Referência ao banco de dados

    private void Start()
    {
        DisplayCardDatabase();
    }

    private void DisplayCardDatabase()
    {
        foreach (Transform child in cardDatabasePanel)
        {
            Destroy(child.gameObject);
        }

        foreach (Card card in cardDatabase.GetAllCards())
        {
            GameObject cardUI = Instantiate(cardPrefab, cardDatabasePanel);
            DisplayDatabaseCard displayDatabaseCard = cardUI.GetComponent<DisplayDatabaseCard>();
            if (displayDatabaseCard != null)
            {
                displayDatabaseCard.SetCard(card);
            }
        }
    }
}
