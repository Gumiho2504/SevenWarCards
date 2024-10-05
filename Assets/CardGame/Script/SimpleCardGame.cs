using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

enum result
{
    playerWin,aiWin,none
}
public class PokerGame : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject playerHand;
    public GameObject aiHand;
    public GameObject deskPlace;

    public Sprite[] elementSprites;
    public Color[] rankColors;
    public Text playerScoreText;
    public Text aiScoreText;
    public Text gameStatusText;
    public Text playerHandTypeText;
    public Text aiHandTypeText;
    public Text coinText;
    public Text betText;
    public Text playerCoinText;
    public Text aiCoinText;

    private List<GameCard> deck = new List<GameCard>();
    private List<GameCard> playerCards = new List<GameCard>();
    private List<GameCard> aiCards = new List<GameCard>();

    private int playerScore = 0;
    private int aiScore = 0;
    private int currentRound = 0;
    public int coin = 1000;
    public int maxBet = 10;
    public int minBet = 1;
    private int bet = 1;
    private int playercoin = 0;
    private int aiCoin = 0;

    public Button readyButton;
    public Button playerButton;
    public Button aiButton;

    private result gameResult;
    private result chosePlayerWin;
    private result choseAiWin;

    private string[] elements = { "Fire", "Water", "Earth", "Wind" };

    private string[] ranks = { "A","10", "J", "Q", "K" };

    IEnumerator Start()
    {
        UpdateTextUI();
        betText.text = bet.ToString() + "$";
        playerScore = 0;
        aiScore = 0;
        GenerateDeck();
        gameStatusText.text = "cards shuffling .....!";
        readyButton.onClick.AddListener(ReadyButton);
        yield return new WaitForSeconds(1f);
        playerButton.interactable = true;
        aiButton.interactable = true;
        gameStatusText.text = "chose player or ai and click ready .....!";
        readyButton.interactable = false;
       
    }

    public void ReadyButton()
    {
        readyButton.interactable = false;
        playerButton.interactable = false;
        aiButton.interactable = false;
        StartCoroutine(GamePerformance());
    }

    IEnumerator GamePerformance()
    {
        UpdateScoreUI();
        //GenerateDeck();
        ShuffleDeck();
        gameStatusText.text = "dealing cards .....!";
        yield return new WaitForSeconds(0.5f);

        DealCards();
    }

    private void UpdateTextUI()
    {
        coinText.text = coin.ToString() + "$";
      
        playerCoinText.text = playercoin.ToString() + "$";
        aiCoinText.text = aiCoin.ToString() + "$";
    }

    public void ChangeBet(string name)
    {
        switch (name)
        {
            case "+":
                bet += 1;
                if (bet > maxBet) {
                    bet = minBet;
                }
               

                
                break;
            default:
                bet -= 1;
                if (bet < minBet)
                {
                    bet = maxBet;
                }
                

          
                
                break;
        }
        betText.text = bet.ToString() + "$";
    }

    public void GussingButton()
    {

        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.gameObject.name;
       // print("click = " + name);
        if (coin >= bet)
        {
            readyButton.interactable = true;
            gameStatusText.text = "click ready to deal cards.....!";
            coin -= bet;
            switch (name)
            {
                case "player":
                    chosePlayerWin = result.playerWin;
                    playercoin += bet;
                    break;
                default:
                    aiCoin += bet;
                    chosePlayerWin = result.aiWin;
                    break;
            }
        }else if(coin < bet)
        {
            gameStatusText.text = "bet amount bigger than coin.....!";
        }
        else
        {
            gameStatusText.text = "you have no coin.....!";
        }
        UpdateTextUI();
    }

    void GenerateDeck()
    {
        foreach (string element in elements)
        {
            foreach (string rank in ranks)
            {
                
                GameObject newCard = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
                newCard.transform.SetParent(deskPlace.transform, false); 

                
                GameCard card = newCard.GetComponent<GameCard>();
                card.element = element;
                card.rank = rank;

              
                Image elementImage = newCard.transform.Find("ElementImage").GetComponent<Image>();
                Text rankText = newCard.transform.Find("RankText").GetComponent<Text>(); 

         
                elementImage.sprite = GetElementSprite(element);
                rankText.text = rank;
                rankText.color = GetRankColor(element);

                deck.Add(card);


                Vector3 randomPosition = new Vector3(
                    Random.Range(-300f, 300f),
                    Random.Range(-300f, 300f),
                    0);


                LeanTween.moveLocal(newCard, randomPosition, 0.5f).setEase(LeanTweenType.easeOutQuad)
                    .setOnComplete(() =>
                    {
                        LeanTween.moveLocal(newCard, Vector3.zero, 0.7f).setEase(LeanTweenType.easeInOutCubic);
                    });
            }
        }
    }

  



    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            GameCard temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }



    IEnumerator FlipCard(GameObject card)
    {
        LeanTween.scaleX(card, 0f, 0.3f).setEase(LeanTweenType.easeInOutQuad);
        yield return new WaitForSeconds(0.3f);

        card.transform.Find("CardBack").gameObject.SetActive(false); 
      
        LeanTween.scaleX(card, 1f, 0.3f).setEase(LeanTweenType.easeInOutQuad);
    }


    void DealCards()
    {
        StartCoroutine(DealCardsCoroutine());
    }


    IEnumerator DealCardsCoroutine()
    {
        playerCards.Clear();
        aiCards.Clear();

        for (int i = 0; i < 5; i++) 
        {
            currentRound++;

           
            GameObject playerCard = deck[i].gameObject;
            playerCard.SetActive(true);
            playerCard.transform.SetParent(playerHand.transform, false);
            LeanTween.moveLocal(playerCard,
            new Vector3(playerHand.transform.localPosition.x-200 + i *100, playerHand.transform.localPosition.y*0, playerHand.transform.localPosition.z)
                , 0.5f).setFrom(new Vector3(0f,-playerHand.transform.localPosition.y, 0f)).setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() =>
                {
                    playerCard.transform.SetParent(playerHand.transform, false);
                })
                ;

            playerCards.Add(deck[i]);

            StartCoroutine(FlipCard(playerCard));

            yield return new WaitForSeconds(0.5f);

           
            GameObject aiCard = deck[i + 5].gameObject;
            aiCard.SetActive(true);
            aiCard.transform.SetParent(aiHand.transform, false);
            
            LeanTween.moveLocal(aiCard,
            new Vector3(aiHand.transform.localPosition.x - 200 + i * 100, aiHand.transform.localPosition.y * 0, aiHand.transform.localPosition.z)
                , 0.5f).setFrom(new Vector3(0f, -aiHand.transform.localPosition.y, 0f)).setEase(LeanTweenType.easeInOutQuad)
                .setOnComplete(() =>
                {
                    aiCard.transform.SetParent(aiHand.transform, false);
                })
                ;
            aiCards.Add(deck[i + 5]);

            
            yield return  FlipCard(aiCard);

            yield return new WaitForSeconds(0.5f); 
        }

        
       yield return DetermineRoundWinner();

    }

    IEnumerator DetermineRoundWinner()
    {
        int playerHandValue = EvaluateHand(playerCards, out string playerHandType);
        int aiHandValue = EvaluateHand(aiCards, out string aiHandType);

        playerHandTypeText.text = "Player Hand: " + playerHandType;
        aiHandTypeText.text = "AI Hand: " + aiHandType;

        if (playerHandValue > aiHandValue)
        {
            playerScore++;
            gameResult = result.playerWin;
            gameStatusText.text = "Player wins round!";
        }
        else if (aiHandValue > playerHandValue)
        {
            aiScore++;
            gameResult = result.aiWin;
            gameStatusText.text = "AI wins round!";
        }
        else // Same hand type, compare card values
        {
            // Tie-breaker: Compare high cards within the same hand type
            int tieBreakerResult = CompareSameHandType(playerCards, aiCards);
            if (tieBreakerResult > 0)
            {
                playerScore++;
                gameResult = result.playerWin;
                gameStatusText.text = "Player wins round (by high card)!";
            }
            else if (tieBreakerResult < 0)
            {
                aiScore++;
                gameResult = result.aiWin;
                gameStatusText.text = "AI wins round (by high card)!";
            }
            else
            {
                gameStatusText.text = "Round is a tie!";
                gameResult = result.none;
            }
        }

        yield return new WaitForSeconds(2f);

        if (chosePlayerWin == gameResult)
        {
            coin += playercoin * 2;
            gameStatusText.text = $"Player wins + {playercoin}$";
        }
        else if (choseAiWin == gameResult)
        {
            coin += aiCoin * 2;
            gameStatusText.text = $"AI wins + {aiCoin}$";
        }
        else if (gameResult == result.none)
        {
            coin += playercoin + aiCoin;
            gameStatusText.text = "Drawn round!";
        }
        else
        {
            gameStatusText.text = $"Lose round - {playercoin + aiCoin}$";
        }

        UpdateTextUI();
        UpdateScoreUI();

        yield return new WaitForSeconds(3f);
        ResetGameState();
        yield return new WaitForSeconds(1f);
        playerButton.interactable = true;
        aiButton.interactable = true;
        gameStatusText.text = "chose player or ai and click ready .....!";
    }

    void ResetGameState()
    {
        gameStatusText.text = "New round starting...!";
        playercoin = aiCoin = 0;
        UpdateTextUI();
        readyButton.interactable = false;

        // Clear hands
        deck.Clear();
        playerCards.Clear();
        aiCards.Clear();

        // Destroy old cards from UI
        ClearHandUI(playerHand.transform);
        ClearHandUI(deskPlace.transform);
        ClearHandUI(aiHand.transform);

        // Reset text and shuffle
        playerHandTypeText.text = "Player Hand: ---";
        aiHandTypeText.text = "AI Hand: ---";
        gameStatusText.text = "Shuffling cards...!";
        GenerateDeck();
    }

    void ClearHandUI(Transform handTransform)
    {
        for (int i = 0; i < handTransform.childCount; i++)
        {
            Destroy(handTransform.GetChild(i).gameObject);
        }
    }

    int CompareSameHandType(List<GameCard> playerHand, List<GameCard> aiHand)
    {
        // Sort hands by card value
        playerHand.Sort((x, y) => GetCardValue(x.rank).CompareTo(GetCardValue(y.rank)));
        aiHand.Sort((x, y) => GetCardValue(x.rank).CompareTo(GetCardValue(y.rank)));

        // Compare cards starting from the highest value
        for (int i = playerHand.Count - 1; i >= 0; i--)
        {
            int playerCardValue = GetCardValue(playerHand[i].rank);
            int aiCardValue = GetCardValue(aiHand[i].rank);

            if (playerCardValue > aiCardValue)
            {
                return 1; // Player wins
            }
            else if (playerCardValue < aiCardValue)
            {
                return -1; // AI wins
            }
        }

        return 0; // Perfect tie
    }

    int EvaluateHand(List<GameCard> hand, out string handType)
    {
        hand.Sort((x, y) => GetCardValue(x.rank).CompareTo(GetCardValue(y.rank)));  // Sorting for easier rank evaluation

        if (IsRoyalFlush(hand))
        {
            handType = "Royal Flush";
            return 10;
        }
        else if (IsStraightFlush(hand))
        {
            handType = "Straight Flush";
            return 9;
        }
        else if (IsFourOfAKind(hand))
        {
            handType = "Four of a Kind";
            return 8;
        }
        else if (IsFullHouse(hand))
        {
            handType = "Full House";
            return 7;
        }
        else if (IsFlush(hand))
        {
            handType = "Flush";
            return 6;
        }
        else if (IsStraight(hand))
        {
            handType = "Straight";
            return 5;
        }
        else if (IsThreeOfAKind(hand))
        {
            handType = "Three of a Kind";
            return 4;
        }
        else if (IsTwoPair(hand))
        {
            handType = "Two Pair";
            return 3;
        }
        else if (IsOnePair(hand))
        {
            handType = "One Pair";
            return 2;
        }

        // High Card
        handType = "High Card";
        return 0; // Highest card in sorted hand
    }

    // Poker hand evaluation helpers
    bool IsRoyalFlush(List<GameCard> hand)
    {
        return IsFlush(hand) && IsStraight(hand) && GetCardValue(hand[0].rank) == 10;
    }

    bool IsStraightFlush(List<GameCard> hand)
    {
        return IsFlush(hand) && IsStraight(hand);
    }

    bool IsFourOfAKind(List<GameCard> hand)
    {
        Dictionary<string, int> rankCount = GetRankCount(hand);
        return rankCount.ContainsValue(4);
    }

    bool IsFullHouse(List<GameCard> hand)
    {
        Dictionary<string, int> rankCount = GetRankCount(hand);
        return rankCount.ContainsValue(3) && rankCount.ContainsValue(2);
    }

    bool IsFlush(List<GameCard> hand)
    {
        string suit = hand[0].element;
        return hand.All(card => card.element == suit);
    }

    bool IsStraight(List<GameCard> hand)
    {
        hand.Sort((x, y) => GetCardValue(x.rank).CompareTo(GetCardValue(y.rank)));

        for (int i = 0; i < hand.Count - 1; i++)
        {
            if (GetCardValue(hand[i].rank) + 1 != GetCardValue(hand[i + 1].rank))
            {
                if (i == 0 && hand[i].rank == "A" && GetCardValue(hand[1].rank) == 2)
                    continue;
                return false;
            }
        }

        return true;
    }

    bool IsThreeOfAKind(List<GameCard> hand)
    {
        Dictionary<string, int> rankCount = GetRankCount(hand);
        return rankCount.ContainsValue(3);
    }

    bool IsTwoPair(List<GameCard> hand)
    {
        Dictionary<string, int> rankCount = GetRankCount(hand);
        return rankCount.Values.Count(value => value == 2) == 2;
    }

    bool IsOnePair(List<GameCard> hand)
    {
        Dictionary<string, int> rankCount = GetRankCount(hand);
        return rankCount.ContainsValue(2);
    }

    Dictionary<string, int> GetRankCount(List<GameCard> hand)
    {
        Dictionary<string, int> rankCount = new Dictionary<string, int>();

        foreach (GameCard card in hand)
        {
            if (!rankCount.ContainsKey(card.rank))
            {
                rankCount[card.rank] = 1;
            }
            else
            {
                rankCount[card.rank]++;
            }
        }

        return rankCount;
    }

    int GetCardValue(string rank)
    {
        switch (rank)
        {
            case "A": return 14;
            case "K": return 13;
            case "Q": return 12;
            case "J": return 11;
            case "10": return 10;
            case "9": return 9;
            case "8": return 8;
            case "7": return 7;
            case "6": return 6;
            case "5": return 5;
            case "4": return 4;
            case "3": return 3;
            case "2": return 2;
            default: return 0;
        }
    }





    void UpdateScoreUI()
    {
        playerScoreText.text = "Player Score: " + playerScore;
        aiScoreText.text = "AI Score: " + aiScore;
    }


    Sprite GetElementSprite(string element)
    {
        switch (element)
        {
            case "Fire":

                return elementSprites[0];

            case "Water":

                return elementSprites[1];

            case "Earth":

                return elementSprites[2];

            case "Wind":

                return elementSprites[3];

            default:

                return null;

        }
    }

    Color GetRankColor(string element)
    {
        switch (element)
        {
            case "Fire":

                return rankColors[0];

            case "Water":

                return rankColors[1];

            case "Earth":

                return rankColors[2];

            case "Wind":

                return rankColors[3];
            default:
                return Color.white;
        }
    }
} 
