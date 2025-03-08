using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;




/// <summary>
/// Est� anexado a todas as cartas do jogador no modo Play. Exibe as cartas na tela, acessa o ZoomManager para exib�-las no painel de Zoom
/// Ativa a fun��o de invocar carta ao clicar em invocar, define a carta como atacante ao clicar em atacar
/// Define se uma carta j� atacou no turno, define que a carta pode atacar novamente
/// </summary>
public class PlayCardManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ataqueText;
    public TextMeshProUGUI descriptionText;
    public Image cardImage;
    public GameObject invokeButton;
    public GameObject backImage;
    public GameObject buttonText;
    public bool canZoom = false;
    private bool canAttack = true;
    

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

        if (invokeButton != null)
        {
            invokeButton.SetActive(isFaceUp);
            if (buttonText != null)
            {
                buttonText.SetActive(isFaceUp);
            }

            // Adiciona evento de invoca��o ao bot�o
            invokeButton.GetComponent<Button>().onClick.RemoveAllListeners();
            invokeButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                FindFirstObjectByType<PlayerHandManager>().PlayCardToField(gameObject);
            });
        }
    }
    public void SetAttackMode()
    {
        if (invokeButton != null && buttonText != null)
        {
            // Muda o texto do bot�o para "Atacar"
            buttonText.GetComponent<TextMeshProUGUI>().text = "Atacar";
            

            // Remove os eventos anteriores e adiciona a nova a��o
            invokeButton.GetComponent<Button>().onClick.RemoveAllListeners();
            
                invokeButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    SelectAsAttacker();
                });
            
           
        }
    }
    

    public void SelectAsAttacker() // Ao clicar no bot�o atacar a carta � selecionada como atacante
    {
        TurnManager turnManager = FindFirstObjectByType<TurnManager>();
        if (!canAttack)
        {
            Debug.Log("Essa carta j� atacou neste turno!");
            return;
        }
        else if (turnManager.GetTurnCount() == 0)// Verifica se � o primeiro turno, se for a carta n�o pode atacar
        {
            MarkAsAttacked();
        }
        else if (transform.parent == FindFirstObjectByType<PlayerFieldManager>().fieldPanel)
        {
            FindFirstObjectByType<PlayerFieldManager>().SelectCardToAttack(this); // A carta � definida como atacante aqui
            Debug.Log(card.Name + " foi selecionada para atacar!");
            DirectAttack(this.GetCard().Attack);// Se n�o houver cartas no campo inimigo, realiza um ataque direto ao oponente

        }
    }

    public void MarkAsAttacked()
    {
        canAttack = false; // Define que a carta n�o pode atacar
    }

    public void ResetAttack()
    {
        canAttack = true; // No in�cio do turno, todas cartas podem atacar novamente
    }
    public bool GetCanAttack()
    {
        return canAttack;
    }

    public void DirectAttack(int damage)
    {
        EnemyFieldManager enemyFieldManager = FindFirstObjectByType<EnemyFieldManager>();
        if (enemyFieldManager.fieldPanel.childCount<1) //  Verifica se no campo do oponente n�o h� cartas 
        {
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            gameManager.EnemyTakeDamage(damage);// Chama a fun��o do GameManager de realizar dano no HP inimigo
            this.MarkAsAttacked();
        }
        else
        {
            Debug.Log("H� inimigos no campo");
        }
    }




    private void UpdateCardDisplay() // Exibe a carta na tela atrav�s do CardPlayPrefab
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
            CardZoomManager.ShowCard(card.GetSprite(), card.Description, card.Attack);
        }
    }

    // Quando o mouse sai, esconde o zoom
    public void OnPointerExit(PointerEventData eventData)
    {
        CardZoomManager.HideCard();
    }
    
    

    public void DestroyCard()
    {
        PlayerFieldManager playerFieldManager = FindFirstObjectByType<PlayerFieldManager>();
        if (playerFieldManager != null)
        {
            playerFieldManager.RemoveCardFromField(this); // Remove da lista antes de destruir
        }

        Destroy(gameObject);
    }

}
