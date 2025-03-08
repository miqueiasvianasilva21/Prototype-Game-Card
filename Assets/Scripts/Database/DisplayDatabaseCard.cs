using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Vinculada a todas os prefabs de CardDatabase
/// Respons�vel por exibir uma �nica carta do banco de dados na interface do usu�rio.
/// Atualiza os textos, imagem e bot�o de adicionar ao deck.
/// </summary>

public class DisplayDatabaseCard : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ataqueText;
    public TextMeshProUGUI descriptionText;
    public Image cardImage;
    public Button addButton;

    private Card card;

    public void SetCard(Card newCard)
    {
        card = newCard;
        UpdateCardDisplay();

        if (addButton != null)
        {
            addButton.onClick.RemoveAllListeners();
            addButton.onClick.AddListener(AddCardToDeck);
        }
        else
        {
            Debug.LogError("Bot�o de adicionar n�o foi encontrado no prefab do banco de dados!");
        }
    }

    private void UpdateCardDisplay()
    {
        if (card == null)
        {
            Debug.LogError("Card n�o foi definido!");
            return;
        }

        nameText.text = card.Name;
        ataqueText.text = card.Attack.ToString();
        descriptionText.text = card.Description;
        cardImage.sprite = card.GetSprite();
    }

    private void AddCardToDeck()
    {
        if (card != null)
        {
            if (Deck.Instance.GetLength() < 18)// O deck s� pode ter 18 cartas
            {
                Deck.Instance.AddCard(card);
                Debug.Log("Carta adicionada ao deck: " + card.Name);
                Debug.Log("Cartas no deck = " + Deck.Instance.GetLength());
            }
            
        }
    }
}
