using UnityEngine;


/// <summary>
/// Classe respons�vel por gerenciar os turnos 
/// Permite que o jogador realize suas jogadas no seu turno
/// Realiza o turno do inimigo
/// Possui um contador para saber em que turno o jogo est� (Em jogos do gen�ro o n�mero de turnos � utilizado para aplicar efeitos de cartas m�gicas)
/// 
/// </summary>
public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    private bool playerDrewCard = false; 
    private bool enemyDrewCard = false;
    private bool isPlayerTurn = true; // o jogo come�a com o turno do player
    private int turnCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetTurnCount()
    {
        return turnCount;
    }
    public void UpdateTurnCount()
    {
        turnCount++;
    }
    public bool CanDrawCard(bool isPlayer)
    {
        if (isPlayer)
            return !playerDrewCard;
        else
            return !enemyDrewCard;
    }

    public void DrawCard(bool isPlayer)// quando o jogador ou inimigo comprar carta define que ele j� comprou
    {
        if (isPlayer)
            playerDrewCard = true;
        else
            enemyDrewCard = true;
    }

    public void EndTurn()
    {
        isPlayerTurn = !isPlayerTurn;

        // Reseta a compra de carta no novo turno
        playerDrewCard = false;
        enemyDrewCard = false;


        Debug.Log("Turno trocado! Agora � turno do " + (isPlayerTurn ? "jogador" : "inimigo"));
        UpdateTurnCount();

        if (!isPlayerTurn) // Se for turno do inimigo
        {
            EnemyTurn();// Inimigo faz suas jogadas
           
        }
    }

   

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
    public void EnemyTurn() // Turno do inimigo
    {
        PlayerFieldManager playerFieldManager = FindFirstObjectByType<PlayerFieldManager>();
        EnemyHandManager enemyHandManager = FindFirstObjectByType<EnemyHandManager>();
        enemyHandManager.DrawCardToEnemyHand(); 
        enemyHandManager.SummonCard();
        EnemyFieldManager enemyFieldManager = FindFirstObjectByType<EnemyFieldManager>();
        enemyFieldManager.AttackAllCards();
        playerFieldManager.ResetAllCardsAttack();
        isPlayerTurn = true;
        UpdateTurnCount();
    }
}
