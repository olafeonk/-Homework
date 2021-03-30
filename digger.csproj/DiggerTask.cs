using System.Collections.Generic;
using System.Windows.Forms;
using static Digger.Game;
using Point = System.Drawing.Point;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player;
        }
    }

    public class Player : ICreature
    {
        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public CreatureCommand Act(int x, int y)
        {
            var displacement = new CreatureCommand();
            switch (KeyPressed)
            {
                case Keys.Up when y > 0:
                    displacement.DeltaY--;
                    break;
                case Keys.Down when y + 1 < MapHeight:
                    displacement.DeltaY++;
                    break;
                case Keys.Left when x > 0:
                    displacement.DeltaX--;
                    break;
                case Keys.Right when x + 1 < MapWidth:
                    displacement.DeltaX++;
                    break;
            }
            var nextCell = Map[x + displacement.DeltaX, y + displacement.DeltaY];
            if (nextCell is Gold)
            {
                Scores += 10;
            }
            return nextCell is Sack ? new CreatureCommand() : displacement;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }
    }

    public class Sack : ICreature
    {
        private int fallHeight; 
        public string GetImageFileName()
        {
            return "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public CreatureCommand Act(int x, int y)
        {
            var displacement = new CreatureCommand();
            
            if (y + displacement.DeltaY + 1 < MapHeight 
                   && (Map[x, y + displacement.DeltaY + 1] is null 
                       || (Map[x, y + displacement.DeltaY + 1] is Player 
                           || Map[x, y + displacement.DeltaY + 1] is Monster)
                           && fallHeight > 0))
            {
                displacement.DeltaY++;
                fallHeight++;
            }
            else if (fallHeight <= 1)
            {
                fallHeight = 0;
            }
            else
            {
                displacement.TransformTo = new Gold();
            }
            return displacement;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {    
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Player || conflictedObject is Monster;
        }
    }

    public class Monster : ICreature
    {
        private readonly int[] dx = {0, 0, 1, -1};
        private readonly int[] dy = {1, -1, 0, 0};

        private static Point GetPlayerPosition()
        {
            var playerPosition = new Point(-1, -1);
            for (var i = 0; i < MapWidth; i++)
            {
                for (var j = 0; j < MapHeight; j++)
                {
                    if (Map[i, j] is Player)
                    {
                        playerPosition.X = i;
                        playerPosition.Y = j;
                        return playerPosition;
                    }
                }
            }
            return playerPosition;
        }

        private static bool IsPossibleMove(int x, int y)
        {
            return x >= 0 && y >= 0 && x < MapWidth && y < MapHeight &&
                   (Map[x, y] is Player || Map[x, y] is null || Map[x, y] is Gold);
        }
        
        private int GetShortestDistanceBetweenVertices(Point startVertex, Point endVertex)
        {
            var queue = new Queue<Point>();
            queue.Enqueue(startVertex);
            var used = new bool[MapWidth, MapHeight];
            var distance = new int[MapWidth, MapHeight];
            used[startVertex.X, startVertex.Y] = true;
            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                for (var i = 0; i < 4; i++)
                {
                    var newX = vertex.X + dx[i];
                    var newY = vertex.Y + dy[i];
                    if (IsPossibleMove(newX, newY) && !used[newX, newY])
                    {
                        used[newX, newY] = true;
                        queue.Enqueue(new Point(newX, newY));
                        distance[newX, newY] = distance[vertex.X, vertex.Y] + 1;
                    }
                }
            }

            if (!used[endVertex.X, endVertex.Y])
            {
                return -1;
            }

            return distance[endVertex.X, endVertex.Y];
        }

        public CreatureCommand Act(int x, int y)
        {
            var playerPosition = GetPlayerPosition();
            var minimalDistance = 100;
            var type = -1;
            if (playerPosition != new Point(-1, -1))
            {
                for (var i = 0; i < 4; i++)
                {
                    var newMove = new Point(x + dx[i], y + dy[i]);
                    if (IsPossibleMove(newMove.X, newMove.Y))
                    {
                        var distance = GetShortestDistanceBetweenVertices(newMove, playerPosition);
                        if (distance < minimalDistance && distance != -1)
                        {
                            minimalDistance = distance;
                            type = i;
                        }
                    }
                }
                return type == -1 ? new CreatureCommand() : new CreatureCommand {DeltaX = dx[type], DeltaY = dy[type]};
            }
            return new CreatureCommand();
        }
        
        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }
        
        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }
    }
}
