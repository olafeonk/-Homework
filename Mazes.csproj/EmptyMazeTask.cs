namespace Mazes
{
    public static class EmptyMazeTask
    {
        public static void Move(Robot robot, int count, Direction direction)
        {
            for (var i = 0; i < count; i++)
            {
                robot.MoveTo(direction);
            }
        }

        public static void MoveOut(Robot robot, int width, int height)
        {
            Move(robot, width - 3, Direction.Right);
            Move(robot, height - 3, Direction.Down);
        }
    }
}