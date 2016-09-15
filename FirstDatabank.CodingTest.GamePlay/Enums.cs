namespace FirstDatabank.CodingTest.GamePlay
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum MoveResult
    {
        Undefined = 0,
        Safe,
        Hit,
        Dead,
        Win
    }
}