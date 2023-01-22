using Bingo;

//if (args.Length > 0)
//{
//    foreach (var arg in args)
//    {
//        Console.WriteLine($"Argument={arg}");
//    }
//}
//else
//{
//    Console.WriteLine("No arguments");
//}

var bd = new BallDrawer();
var cards = new List<Card>();
string? answer;
var validAnswer = false;
int numBalls = 1;
Card userCard;
Card lowestBingo;
int lowestBingoIdx;

while (!validAnswer)
{
    var c = new Card();
    cards.Add(c);
    Console.WriteLine("Your card is:");
    Console.WriteLine(c.ToString());
    Console.WriteLine("Would you like to keep this card? (Y/N)");
    answer = Console.ReadLine();
    validAnswer = answer?.ToLower() == "y";
}
Console.WriteLine($"Your card is #{cards.Count}");
Console.WriteLine("Press enter to continue...");
Console.ReadLine();
validAnswer = false;
userCard = lowestBingo = cards[^1];
lowestBingoIdx = cards.Count;

while (!validAnswer)
{
    Console.WriteLine("How many balls would you like to draw? (1 - 75)");
    answer = Console.ReadLine();

    if (answer != null && int.TryParse(answer, out numBalls) && (numBalls < 1 || numBalls > 75))
    {
        Console.WriteLine("Invalid number of balls");
    }
    else
        validAnswer = true;
}
validAnswer = false;

bd.DrawBalls(numBalls);
Console.WriteLine("You drew: ");
Console.WriteLine(bd.ToString());
Console.WriteLine("\n");
foreach(Card c in cards)
{
    c.CheckNumbers(bd.Drawn);
}
Console.WriteLine(userCard.ToString());
if (userCard.HasBingo)
    Console.WriteLine($"BINGO! - You got Bingo in: {userCard.BingoIn}\n");
else
    Console.WriteLine("Sorry! You did not get bingo.");
Console.WriteLine("Press enter to see the other cards...");
Console.ReadLine();

for (int i = 0; i < cards.Count - 1; i++)
{
    if (cards[i].HasBingo)
    {
        Console.WriteLine($"Card {i} got bingo in {cards[i].BingoIn}");
        Console.WriteLine(cards[i].ToString());
        if (cards[i].BingoIn > 0 && cards[i].BingoIn < lowestBingo.BingoIn)
        {
            lowestBingo = cards[i];
            lowestBingoIdx = i;
        }
    }
}
Console.WriteLine("\n\n");

if (cards.Count == 1)
    Console.WriteLine("You were playing by yourself");
else if (lowestBingo != userCard)
    Console.WriteLine($"If you had kept card {lowestBingoIdx}, you would have had bingo in {lowestBingo.BingoIn} balls. Better luck next time!");
else if (userCard.HasBingo)
    Console.WriteLine("You got bingo first! Congrats!");
else
    Console.WriteLine("Nobody got bingo. Tough crowd! Try picking more balls next time.");