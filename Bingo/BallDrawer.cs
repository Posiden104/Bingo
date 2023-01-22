using System.Text;

namespace Bingo
{
    public class BallDrawer
    {
        public List<int> Drawn;

        private List<int> Cage;
        private Random rnd;

        public BallDrawer()
        {
            Cage = new(Balls.AllBalls);
            Drawn = new();
            rnd = new();
        }

        public void Reset()
        {
            Cage = new(Balls.AllBalls);
            Drawn = new();
            rnd = new();
        }

        public bool DrawBalls(int numberOfDraws)
        {
            for(int i = 0; i < numberOfDraws; i++)
            {
                if (Cage.Count == 0)
                    return false;
                var ball = Cage[rnd.Next(0, Cage.Count)];
                Cage.Remove(ball);
                Drawn.Add(ball);
            }

            return true;
        }

        public override string ToString()
        {
            StringBuilder s = new();
            foreach (var ball in Drawn)
            {
                int ballMod = (ball - 1) / 15;
                string ballLetter;
                if (ballMod == 0)
                    ballLetter = "B";
                else if (ballMod == 1)
                    ballLetter = "I";
                else if (ballMod == 2)
                    ballLetter = "N";
                else if (ballMod == 3)
                    ballLetter = "G";
                else
                    ballLetter = "O";
                s.Append($"{ballLetter}{ball}, ");
            }
            return s.ToString()[..(s.Length - 2)];
        }
    }
}
