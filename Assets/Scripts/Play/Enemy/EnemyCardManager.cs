using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.ComponentModel;

/// <summary>
/// Classe respons�vel por gerenciar as cartas do inimigo
/// Est� associada a todas as cartas no lado do inimigo
/// Exibe as cartas na tela
/// Chama o ZoomManager para exibir detalhes da carta no painel de zoom
/// Define a carta como atacada quando ela recebe um clique
/// </summary>
public class EnemyCardManager : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ataqueText;
    public TextMeshProUGUI descriptionText;
    public Image cardImage;
    public GameObject backImage;
    public bool canZoom = false;

    private Card card;

    public Card GetCard()
    {
        return card;
    }

    public void SetCard(Card newCard, bool isFaceUp = false)
    {
        card = newCard;
        UpdateCardDisplay();

        if (backImage != null)
        {
            backImage.SetActive(!isFaceUp);
        }

    }
    private void UpdateCardDisplay()// Exibe a carta na tela
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

    // Quando o mouse passa por cima, ativa o zoom e exibe a descri��o + ataque
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (card != null)
        {
            if(canZoom == true)
            {
                CardZoomManager.ShowCard(card.GetSprite(), card.Description, card.Attack);
            }
            
        }
    }

    // Quando o mouse sai, esconde o zoom
    public void OnPointerExit(PointerEventData eventData)
    {
        CardZoomManager.HideCard();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (card != null)
        {
            // Verifica se h� uma carta selecionada no campo do jogador
            PlayCardManager selectedPlayerCard = FindFirstObjectByType<PlayerFieldManager>().GetSelectedCard();
            if (selectedPlayerCard != null && selectedPlayerCard.GetCanAttack())
            {
                // Acessando o ataque da carta selecionada do jogador
                int attackDamage = selectedPlayerCard.GetCard().Attack;

                // Se o ataque do jogador for maior que o da carta inimiga, a carta do inimigo � removida
                if (attackDamage > card.Attack)
                {
                    Debug.Log(card.Name + " foi destru�da pelo ataque de " + attackDamage);
                    int damage = attackDamage - card.Attack;// a diferen�a entre os ataques � deduzida no hp do inimigo
                    GameManager gameManager = FindFirstObjectByType<GameManager>();
                    gameManager.EnemyTakeDamage(damage);
                    Destroy(gameObject); // Remove a carta do inimigo do campo
                    selectedPlayerCard.MarkAsAttacked();
                }
                else if (attackDamage == card.Attack)
                {
                    Destroy(gameObject);
                    selectedPlayerCard.DestroyCard();
                }
                else
                {
                    Debug.Log("A carta resistiu ao ataque");
                }
            }
            else
            {
                Debug.LogWarning("Nenhuma carta do jogador foi selecionada para o ataque.");
            }
        }
       
    }

    public void DestroyCard()
    {
        Destroy(gameObject);
    }



}
