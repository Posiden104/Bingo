namespace Bingo
{
    public class Cell
    {
        public int Num { get; set; }
        public bool Picked { get; set; }

        public Cell(int num, bool picked = false)
        {
            Num = num;
            Picked = picked;
        }

        public override string ToString()
        {
            if (Num == -1)
                return $"Free";
            else if (Picked)
                return $"({Num:D2})";
            return $" {Num:D2} ";
        }
    }
}
