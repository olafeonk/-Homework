namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void Move(Robot robot, int count, Direction direction)
        {
            for (var i = 0; i < count; i++)
            {
                robot.MoveTo(direction);
            }
        }

        public static void MoveSnake(Robot robot, int count, Direction firstDirection, Direction secondDirection)
        {
            while(true)
            {
                Move(robot, count, firstDirection);
                if (robot.Finished)
                {
                    return;
                }
                robot.MoveTo(secondDirection);
            }
        }

        public static void MoveOut(Robot robot, int width, int height)
        {
            if (height >= width)
            {
                MoveSnake(robot, (height - 3) / (width - 2), Direction.Down, Direction.Right);
            }
            else
            {
                MoveSnake(robot, (width - 3) / (height - 2), Direction.Right, Direction.Down);
            }
        }
    }
}