using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu._051_100
{
    public class Problem_054 {
        public static int Solve() {
            int playerOneWins = 0;

            foreach ((Hand hand1, Hand hand2) in ReadPokerHands())
                if (hand1 > hand2)
                    playerOneWins++;

            return playerOneWins;
        }

        private static IEnumerable<(Hand, Hand)> ReadPokerHands() {
            string file = @"C:\Temp\p054_poker.txt";
            foreach (string line in File.ReadLines(file)) {
                if (!string.IsNullOrWhiteSpace(line)) {
                    (Hand hand1, Hand hand2) = ParseHands(line);
                    yield return (hand1, hand2);
                }
            }
        }

        private static (Hand, Hand) ParseHands(string line) {
            string str1 = line.Substring(0, 14);
            string str2 = line.Substring(15, 14);
            Hand hand1 = Hand.Parse(str1);
            Hand hand2 = Hand.Parse(str2);
            return (hand1, hand2);
        }

        class Card {
            public int Value;
            public char Sign;

            public static Dictionary<char, int> Figures = new Dictionary<char, int> {
                { 'T', 10 },
                { 'J', 11 },
                { 'Q', 12 },
                { 'K', 13 },
                { 'A', 14 },
            };

            public static Dictionary<int, char> NumbersToFigures = new Dictionary<int, char> {
                { 10, 'T' },
                { 11, 'J' },
                { 12, 'Q' },
                { 13, 'K' },
                { 14, 'A' },
            };

            public static Card Parse(string text) {
                Card result = new Card();

                bool isNumber = int.TryParse(text[0].ToString(), out int n);
                if (isNumber)
                    result.Value = n;
                else
                    result.Value = Card.Figures[text[0]];

                result.Sign = text[1];
                return result;
            }

            public override string ToString() {
                string result = "";

                if (NumbersToFigures.ContainsKey(Value))
                    result += NumbersToFigures[Value];
                else
                    result += Value.ToString();

                result += Sign;
                return result;
            }
        }

        class CardComparer : IComparer<Card> {
            public int Compare(Card x, Card y) {
                return x.Value.CompareTo(y.Value);
            }
        }

        class Hand {
            public List<Card> Cards = new List<Card>();
            static CardComparer _cardComparer = new CardComparer();

            public Hand(IEnumerable<Card> cards) {
                Cards = cards.ToList();
                Cards.Sort(_cardComparer);
            }

            public static Hand Parse(string text) {
                string[] cardTexts = text.Split(' ');
                Hand hand = new Hand(cardTexts.Select(t => Card.Parse(t)));
                return hand;
            }

            public static bool operator >(Hand hand1, Hand hand2) {
                HandScore score1 = HandScoreComputer.ComputeScore(hand1);
                HandScore score2 = HandScoreComputer.ComputeScore(hand2);
                return score1.CompareTo(score2) > 0;
            }

            public static bool operator <(Hand hand1, Hand hand2) {
                return (hand2 > hand1);
            }

            public override string ToString() {
                return string.Join(" ", Cards);
            }
        }

        class HandScore : IComparable
        {
            public HandRank Rank;
            public List<int> OtherCardValues;

            public int CompareTo(object otherObj) {
                HandScore other = otherObj as HandScore;
                int rankDiff = (int)this.Rank - (int)other.Rank;
                if (rankDiff != 0)
                    return rankDiff;

                // compare starting with biggest card (the last one)
                Debug.Assert(this.OtherCardValues.Count == other.OtherCardValues.Count);
                for (int i = OtherCardValues.Count - 1; i >= 0; i--) {
                    int diff = this.OtherCardValues[i] - other.OtherCardValues[i];
                    if (diff != 0) return diff;
                }

                Debug.Assert(false); // problem statement says there's always a winner
                return 0;
            }
        }

        enum HandRank
        {
            HighCard = 100,
            OnePair = 200,
            TwoPairs = 300,
            ThreeOfAKind = 400,
            Straight = 500,
            Flush = 600,
            FullHouse = 700,
            FourOfAKind = 800,
            StraightFlush = 900,
            RoyalFlush = 1000
        }

        class HandScoreComputer
        {
            public static HandScore ComputeScore(Hand hand) {
                HandScore result = new HandScore();
                List<int> cardValues = hand.Cards.Select(c => c.Value).ToList();
                List<int> otherCardValues;
                bool isFlush = IsFlush(hand, out List<int> flushOtherCardValues);
                bool isStraight = IsStraight(cardValues, out List<int> straightOtherCardValues);

                if (isFlush && IsRoyal(cardValues)) {
                    result.Rank = HandRank.RoyalFlush;
                    otherCardValues = flushOtherCardValues;
                }
                else if (isFlush && isStraight) {
                    result.Rank = HandRank.StraightFlush;
                    otherCardValues = flushOtherCardValues;
                }
                else if (IsFourOfAKind(cardValues, out otherCardValues)) {
                    result.Rank = HandRank.FourOfAKind;
                }
                else if (IsFullHouse(cardValues, out otherCardValues)) {
                    result.Rank = HandRank.FullHouse;
                }
                else if (isFlush) {
                    result.Rank = HandRank.Flush;
                    otherCardValues = flushOtherCardValues;
                }
                else if (isStraight) {
                    result.Rank = HandRank.Straight;
                    otherCardValues = straightOtherCardValues;
                }
                else if (IsThreeOfAKind(cardValues, out List<int> other)) {
                    result.Rank = HandRank.ThreeOfAKind;
                    otherCardValues = other;
                }
                else if (IsTwoPairs(cardValues, out List<int> other2)) {
                    result.Rank = HandRank.TwoPairs;
                    otherCardValues = other2;
                }
                else if (IsOnePair(cardValues, out otherCardValues)) {
                    result.Rank = HandRank.OnePair;
                }
                else result.Rank = HandRank.HighCard;

                Debug.Assert(otherCardValues != null);
                result.OtherCardValues = otherCardValues;
                return result;
            }

            private static bool IsOnePair(List<int> v, out List<int> other) {
                (int, int) mostSignifIdx = (-1, -1);
                if (v[0] == v[1]) mostSignifIdx = (0, 1);
                if (v[1] == v[2]) mostSignifIdx = (1, 2);
                if (v[2] == v[3]) mostSignifIdx = (2, 3);
                if (v[3] == v[4]) mostSignifIdx = (3, 4);

                if (mostSignifIdx.Item1 != -1 && mostSignifIdx.Item2 != -1) {
                    other = new List<int>();
                    other.AddRange(v.Where((n, i) => i != mostSignifIdx.Item1 && i != mostSignifIdx.Item2));
                    other.Add(v[mostSignifIdx.Item1]);

                    return true;
                };

                other = v;
                return false;
            }   
            

            private static bool IsTwoPairs(List<int> v, out List<int> other) {
                // First four elements are pairs
                if (v[0] == v[1] && v[2] == v[3]) {
                    other = new List<int> { v[4], Math.Min(v[1], v[2]), Math.Max(v[1], v[2]) };
                    return true;
                }

                // Last four elements are pairs
                if (v[1] == v[2] && v[3] == v[4]) {
                    other = new List<int> { v[0], Math.Min(v[2], v[3]), Math.Max(v[2], v[3]) };
                    return true;
                }

                // First two and last two are pairs
                if (v[0] == v[1] && v[3] == v[4]) {
                    other = new List<int> { v[2], Math.Min(v[1], v[3]), Math.Max(v[1], v[3]) };
                    return true;
                }

                other = v;
                return false;
            }

            private static bool IsThreeOfAKind(List<int> v, out List<int> otherCardValues) {
                // First three are equal
                if (v[0] == v[1] && v[1] == v[2]) {
                    otherCardValues = new List<int> { v[3], v[4], v[0] };
                    return true;
                }
                // Middle three are equal
                else if (v[1] == v[2] && v[2] == v[3]) {
                    otherCardValues = new List<int> { v[0], v[4], v[3] };
                    return true;
                }
                // Last three are equal
                else if (v[2] == v[3] && v[3] == v[4]) {
                    otherCardValues = new List<int> { v[0], v[1], v[4] };
                    return true;
                }

                otherCardValues = v;
                return false;
            }

            private static bool IsFullHouse(List<int> v, out List<int> otherCardValues) {
                // First three are equal
                if (v[0] == v[1] && v[1] == v[2] && v[3] == v[4]) {
                    otherCardValues = new List<int> { v[3], v[0] };
                    return true;
                }   
                // Last three are equal
                else if (v[0] == v[1] && v[2] == v[3] && v[3] == v[4]) {
                    otherCardValues = new List<int> { v[0], v[3] };
                    return true;
                }

                otherCardValues = v;
                return false;
            }

            private static bool IsFourOfAKind(List<int> v, out List<int> otherValues) {
                if (v[1] == v[2] && v[2] == v[3]) {
                    if (v[0] == v[1]) {
                        otherValues = new List<int> { v[4], v[0] };
                        return true;
                    }
                    else if (v[3] == v[4]) {
                        otherValues = new List<int> { v[0], v[4] };
                        return true;
                    }
                }

                otherValues = v;

                return false;
            }

            private static bool IsStraight(List<int> v, out List<int> straightOtherCardValues) {
                straightOtherCardValues = v;
                return v[0] == v[1] - 1 &&
                    v[1] == v[2] - 1 &&
                    v[2] == v[3] - 1 &&
                    v[3] == v[4] - 1;
            }

            private static bool IsRoyal(List<int> v) {
                return v[0] == Card.Figures['T'] &&
                    v[1] == Card.Figures['J'] &&
                    v[2] == Card.Figures['Q'] &&
                    v[3] == Card.Figures['K'] &&
                    v[4] == Card.Figures['A'];
            }

            private static bool IsFlush(Hand hand, out List<int> cardValues) {
                int distinctSigns = hand.Cards.Select(c => c.Sign).Distinct().Count();
                cardValues = hand.Cards.Select(c => c.Value).ToList();
                return distinctSigns == 1;
            }
        }
    }
}
