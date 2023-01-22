using System.Text;

namespace Bingo
{
    public class Card
    {
        public const int CardHeight = 5;

        public List<Cell> B;
        public List<Cell> I;
        public List<Cell> N;
        public List<Cell> G;
        public List<Cell> O;
        public int BingoIn { get; private set; }
        public bool HasBingo { get; private set; }

        public Card()
        {
            B = GenerateColumn(Balls.BList);
            I = GenerateColumn(Balls.IList);
            N = GenerateColumn(Balls.NList, true);
            G = GenerateColumn(Balls.GList);
            O = GenerateColumn(Balls.OList);
        }

        private List<Cell> GenerateColumn(List<int> lst, bool hasFreeSpace = false)
        {
            var col = new List<Cell>();
            Random rnd = new();
            List<int> numberList = new(lst);

            for (int i = 0; i < CardHeight; i++)
            {
                var idx = rnd.Next(0, numberList.Count);
                var space = numberList[idx];
                numberList.Remove(space);
                col.Add(new Cell(space, false));
            }

            if (hasFreeSpace)
                col[2] = new Cell(-1, true);
            return col;
        }

        public void CheckNumbers(List<int> drawnNumbers)
        {
            if (HasBingo) return;
            foreach (var ball in drawnNumbers)
            {
                int ballMod = (ball - 1) / 15;
                var column = B;
                if (ballMod == 1)
                    column = I;
                else if (ballMod == 2)
                    column = N;
                else if (ballMod == 3)
                    column = G;
                else if (ballMod == 4)
                    column = O;
                var cell = column.FirstOrDefault(c => c.Num == ball);

                if (cell != null)
                    cell.Picked = true;
                BingoIn++;
                if (CheckBingo())
                {
                    HasBingo = true;
                    return;
                }
            }
        }

        public bool CheckBingo()
        {
            // Row bingo
            for(int i = 0; i < CardHeight; i++)
            {
                if(B[i].Picked && I[i].Picked && N[i].Picked && G[i].Picked && O[i].Picked)
                    return true;
            }

            // Column bingo
            if (B.All(c => c.Picked) || I.All(c => c.Picked) || N.All(c => c.Picked) || G.All(c => c.Picked) || O.All(c => c.Picked))
                return true;

            // Diagonal bingo
            if (B[0].Picked && I[1].Picked && N[2].Picked && G[3].Picked && O[4].Picked)
                return true;
            if (B[4].Picked && I[3].Picked && N[2].Picked && G[1].Picked && O[0].Picked)
                return true;

            return false;
        }

        public override string ToString()
        {
            StringBuilder s = new();
            s.Append("| B  | I  | N  | G  | O  |\n");
            s.Append("|----|----|----|----|----|\n");

            for(int i = 0; i < CardHeight; i++)
            {
                s.Append($"|{B[i].ToString()}");
                s.Append($"|{I[i].ToString()}");
                s.Append($"|{N[i].ToString()}");
                s.Append($"|{G[i].ToString()}");
                s.Append($"|{O[i].ToString()}");
                s.Append("|\n");
            }

            return s.ToString();
        }
    }
}
