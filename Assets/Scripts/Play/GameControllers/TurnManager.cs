using System.Collections;
using UnityEngine;


/// <summary>
/// Classe responsável por gerenciar os turnos 
/// Permite que o jogador realize suas jogadas no seu turno
/// Realiza o turno do inimigo
/// Possui um contador para saber em que turno o jogo está (Em jogos do genêro o número de turnos é utilizado para aplicar efeitos de cartas mágicas)
/// 
/// </summary>
public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }

    private bool playerDrewCard = false; 
    private bool enemyDrewCard = false;
    private bool isPlayerTurn = true; // o jogo começa com o turno do player
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

    public void DrawCard(bool isPlayer)// quando o jogador ou inimigo comprar carta define que ele já comprou
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


        Debug.Log("Turno trocado! Agora é turno do " + (isPlayerTurn ? "jogador" : "inimigo"));
        UpdateTurnCount();

        if (!isPlayerTurn) // Se for turno do inimigo
        {
            StartCoroutine(EnemyTurn());// Inimigo faz suas jogadas

        }
    }

   

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
    private IEnumerator EnemyTurn()
    {
        PlayerFieldManager playerFieldManager = FindFirstObjectByType<PlayerFieldManager>();
        EnemyHandManager enemyHandManager = FindFirstObjectByType<EnemyHandManager>();
        EnemyFieldManager enemyFieldManager = FindFirstObjectByType<EnemyFieldManager>();

        isPlayerTurn = false;

        // Inimigo compra uma carta
        enemyHandManager.DrawCardToEnemyHand();
        yield return new WaitForSeconds(1.0f); // Atraso de 1 segundo

        // Inimigo invoca uma carta
        enemyHandManager.SummonCard();
        yield return new WaitForSeconds(1.0f);

        // Inimigo ataca
        enemyFieldManager.AttackAllCards();
        yield return new WaitForSeconds(1.5f);

        // Reinicia a capacidade de ataque das cartas do jogador
        playerFieldManager.ResetAllCardsAttack();

        // Passa o turno para o jogador
        isPlayerTurn = true;
        UpdateTurnCount();
    }
}
