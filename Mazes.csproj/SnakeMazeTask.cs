namespace Mazes
{
    public static class SnakeMazeTask
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
            while (true)
            {
                Move(robot, width - 3, Direction.Right);
                Move(robot, 2, Direction.Down);
                Move(robot, width - 3, Direction.Left);
                if (robot.Finished)
                    break;
                Move(robot, 2, Direction.Down);
            }
        }
    }
}