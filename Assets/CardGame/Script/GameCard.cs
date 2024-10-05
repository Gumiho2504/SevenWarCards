// Renamed Card class to avoid conflict
using UnityEngine;
[System.Serializable]
public class GameCard : MonoBehaviour
{
    public string element; // Fire, Water, Earth, Wind
    public string rank; // A, 2, 3, ..., K
}
// Check if the hand is a Royal Flush (A, K, Q, J, 10, all the same suit)
//bool IsRoyalFlush(List<GameCard> hand)
//{
//    if (IsFlush(hand) && IsStraight(hand) && GetCardValue(hand[0].rank) == 10)
//    {
//        return true;
//    }
//    return false;
//}

//// Check if the hand is a Straight Flush (five consecutive cards of the same suit)
//bool IsStraightFlush(List<GameCard> hand)
//{
//    return IsFlush(hand) && IsStraight(hand);
//}

//// Check if the hand is Four of a Kind (four cards of the same rank)
//bool IsFourOfAKind(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);
//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 4)
//            return true;
//    }
//    return false;
//}

//// Check if the hand is a Full House (three cards of one rank and two of another)
//bool IsFullHouse(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);
//    bool hasThreeOfAKind = false;
//    bool hasPair = false;

//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 3)
//            hasThreeOfAKind = true;
//        if (rank == 2)
//            hasPair = true;
//    }

//    return hasThreeOfAKind && hasPair;
//}

//// Check if the hand is a Flush (all five cards of the same suit)
//bool IsFlush(List<GameCard> hand)
//{
//    string suit = hand[0].element;
//    foreach (GameCard card in hand)
//    {
//        if (card.element != suit)
//            return false;
//    }
//    return true;
//}

//// Check if the hand is a Straight (five consecutive cards of any suit)
//bool IsStraight(List<GameCard> hand)
//{
//    hand.Sort((x, y) => GetCardValue(x.rank).CompareTo(GetCardValue(y.rank)));

//    for (int i = 0; i < hand.Count - 1; i++)
//    {
//        if (GetCardValue(hand[i].rank) + 1 != GetCardValue(hand[i + 1].rank))
//        {
//            // Handle Ace-low straight (A-2-3-4-5)
//            if (i == 0 && hand[i].rank == "A" && GetCardValue(hand[1].rank) == 2)
//                continue;
//            return false;
//        }
//    }
//    return true;
//}

//// Check if the hand is Three of a Kind (three cards of the same rank)
//bool IsThreeOfAKind(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);
//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 3)
//            return true;
//    }
//    return false;
//}

//// Check if the hand is Two Pair (two different pairs of cards)
//bool IsTwoPair(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);
//    int pairCount = 0;

//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 2)
//            pairCount++;
//    }

//    return pairCount == 2;
//}

//// Check if the hand is One Pair (one pair of cards of the same rank)
//bool IsOnePair(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);
//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 2)
//            return true;
//    }
//    return false;
//}

//// Helper function to count the occurrences of each rank in the hand
//Dictionary<string, int> GetRankCount(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = new Dictionary<string, int>();

//    foreach (GameCard card in hand)
//    {
//        if (!rankCount.ContainsKey(card.rank))
//        {
//            rankCount[card.rank] = 1;
//        }
//        else
//        {
//            rankCount[card.rank]++;
//        }
//    }

//    return rankCount;
//}



//IEnumerator DetermineRoundWinner()
//{
//    int playerHandValue = EvaluateHand(playerCards, out string playerHandType);
//    int aiHandValue = EvaluateHand(aiCards, out string aiHandType);

//    playerHandTypeText.text = "Player Hand: " + playerHandType;
//    aiHandTypeText.text = "AI Hand: " + aiHandType;

//    if (playerHandValue > aiHandValue)
//    {
//        playerScore++;

//        gameResult = result.playerWin;
//        gameStatusText.text = "Player wins round!";
//    }
//    else if (aiHandValue > playerHandValue)
//    {
//        aiScore++;
//        gameStatusText.text = "AI wins round!";
//        gameResult = result.aiWin;
//    }
//    else
//    {
//        gameStatusText.text = "Round is a tie!";
//        gameResult = result.none;
//    }

//    yield return new WaitForSeconds(2f);

//    if (chosePlayerWin == gameResult)
//    {
//        coin += playercoin * 2;
//        gameStatusText.text = $"wins round + {playercoin}";
//    }
//    else if (choseAiWin == gameResult)
//    {
//        coin += aiCoin * 2;
//        gameStatusText.text = $"wins round + {aiCoin}";
//    }
//    else if (gameResult == result.none)
//    {
//        coin += playercoin + aiCoin;
//        gameStatusText.text = $"drawn round ";

//    }
//    else
//    {

//        gameStatusText.text = $"lose round - {playercoin + aiCoin}";
//    }
//    UpdateTextUI();
//    UpdateScoreUI();

//    // reset game
//    yield return new WaitForSeconds(2f);

//    gameStatusText.text = "new round .....!";
//    playercoin = aiCoin = 0;
//    UpdateTextUI();
//    readyButton.interactable = false;

//    deck.Clear();
//    playerCards.Clear();
//    aiCards.Clear();

//    for (int i = 0; i < playerHand.transform.childCount; i++)
//    {
//        Destroy(playerHand.transform.GetChild(i).gameObject);
//    }

//    for (int i = 0; i < deskPlace.transform.childCount; i++)
//    {
//        Destroy(deskPlace.transform.GetChild(i).gameObject);
//    }

//    for (int i = 0; i < aiHand.transform.childCount; i++)
//    {
//        Destroy(aiHand.transform.GetChild(i).gameObject);
//    }

//    yield return new WaitForSeconds(2f);
//    playerHandTypeText.text = "Player Hand: ---";
//    aiHandTypeText.text = "AI Hand: ---";
//    gameStatusText.text = "cards shuffling .....!";
//    GenerateDeck();
//    yield return new WaitForSeconds(1f);
//    gameStatusText.text = "chose player or ai and click ready .....!";

//}



//int EvaluateHand(List<GameCard> hand, out string handType)
//{

//    hand.Sort((x, y) => GetCardValue(x.rank).CompareTo(GetCardValue(y.rank)));


//    if (IsRoyalFlush(hand))
//    {
//        handType = "Royal Flush";
//        return 10;
//    }
//    if (IsStraightFlush(hand))
//    {
//        handType = "Straight Flush";
//        return 9;
//    }
//    if (IsFourOfAKind(hand))
//    {
//        handType = "Four of a Kind";
//        return 8;
//    }
//    if (IsFullHouse(hand))
//    {
//        handType = "Full House";
//        return 7;
//    }
//    if (IsFlush(hand))
//    {
//        handType = "Flush";
//        return 6;
//    }
//    if (IsStraight(hand))
//    {
//        handType = "Straight";
//        return 5;
//    }
//    if (IsThreeOfAKind(hand))
//    {
//        handType = "Three of a Kind";
//        return 4;
//    }
//    if (IsTwoPair(hand))
//    {
//        handType = "Two Pair";
//        return 3;
//    }
//    if (IsOnePair(hand))
//    {
//        handType = "One Pair";
//        return 2;
//    }

//    handType = "High Card";
//    return GetCardValue(hand[4].rank);
//}


//bool IsRoyalFlush(List<GameCard> hand)
//{
//    if (IsFlush(hand) && IsStraight(hand) && GetCardValue(hand[0].rank) == 10)
//    {
//        return true;
//    }
//    return false;
//}


//bool IsStraightFlush(List<GameCard> hand)
//{
//    return IsFlush(hand) && IsStraight(hand);
//}


//bool IsFourOfAKind(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);
//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 4)
//            return true;
//    }
//    return false;
//}


//bool IsFullHouse(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);

//    bool hasThreeOfAKind = false;
//    bool hasPair = false;

//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 3)
//            hasThreeOfAKind = true;
//        if (rank == 2)
//            hasPair = true;
//    }

//    return hasThreeOfAKind && hasPair;
//}


//bool IsFlush(List<GameCard> hand)
//{
//    string suit = hand[0].element;

//    foreach (GameCard card in hand)
//    {
//        if (card.element != suit)
//            return false;
//    }
//    return true;
//}


//bool IsStraight(List<GameCard> hand)
//{
//    hand.Sort((x, y) => GetCardValue(x.rank).CompareTo(GetCardValue(y.rank)));

//    for (int i = 0; i < hand.Count - 1; i++)
//    {
//        if (GetCardValue(hand[i].rank) + 1 != GetCardValue(hand[i + 1].rank))
//        {

//            if (i == 0 && hand[i].rank == "A" && GetCardValue(hand[1].rank) == 2)
//                continue;
//            return false;
//        }
//    }

//    return true;

//}


//bool IsThreeOfAKind(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);
//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 3)
//            return true;
//    }

//    return false;

//}


//bool IsTwoPair(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);
//    int pairCount = 0;

//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 2)
//            pairCount++;
//    }


//    return pairCount == 2;

//}


//bool IsOnePair(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = GetRankCount(hand);
//    foreach (var rank in rankCount.Values)
//    {
//        if (rank == 2)
//            return true;
//    }

//    return false;

//}


//Dictionary<string, int> GetRankCount(List<GameCard> hand)
//{
//    Dictionary<string, int> rankCount = new Dictionary<string, int>();

//    foreach (GameCard card in hand)
//    {
//        if (!rankCount.ContainsKey(card.rank))
//        {
//            rankCount[card.rank] = 1;
//        }
//        else
//        {
//            rankCount[card.rank]++;
//        }
//    }

//    return rankCount;

//}



//int GetCardValue(string rank)
//{
//    switch (rank)
//    {
//        case "A": return 14;
//        case "K": return 13;
//        case "Q": return 12;
//        case "J": return 11;
//        case "10": return 10;
//        case "9": return 9;
//        case "8": return 8;
//        case "7": return 7;
//        case "6": return 6;
//        case "5": return 5;
//        case "4": return 4;
//        case "3": return 3;
//        case "2": return 2;
//        default: return 0;
//    }
//}