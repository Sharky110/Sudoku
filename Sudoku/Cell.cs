namespace Sudoku
{
    public class Cell
    {
        public int value;
        public int vertPosition;
        public int horPosition;
        public int cubePosition;
        public int id;
        public Cell(int id, int hor, int vert, int cube)
        {
            this.id = id;
            vertPosition = vert;
            horPosition = hor;
            cubePosition = cube;
        }
    }
}
