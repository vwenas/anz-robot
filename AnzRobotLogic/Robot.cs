namespace AnzRobotLogic
{
    public enum Direction { NORTH = 0, EAST = 1, SOUTH = 2, WEST = 3 }
    public class Robot
    {
        public GameMap GameMap { get; }

        public Location Location { get; private set; }
        public Direction Direction { get; private set; }

        public bool IsPlaced => Location != null;

        public Robot (GameMap gameMap)
        {
            GameMap = gameMap;

        }

        // place robot on a plane
        public void Place(int x, int y, Direction direction)
        {
            // cancel invalid placement
            if (!GameMap.IsValidLocation(x, y)) 
                return; 

            Location = new Location(x, y);
            Direction = direction;
        }

        public void Move()
        {
            var newX = Location.X;
            var newY = Location.Y;

            switch (Direction)
            {
                case Direction.NORTH:
                    newY++;
                    break;
                case Direction.EAST:
                    newX++;
                    break;
                case Direction.SOUTH:
                    newY--;
                    break;
                case Direction.WEST:
                    newX--;
                    break;

            }

            // cancel out invalid move
            if (!GameMap.IsValidLocation(newX, newY)) return; 


            Location.Update(newX, newY);

        }

        public void Left()
        {
            var newDirection = Direction - 1;
            
            if (newDirection < Direction.NORTH) 
                newDirection = Direction.WEST;

            Direction = newDirection;
            
        }

        public void Right()
        {
            var newDirection = Direction + 1;

            if (newDirection > Direction.WEST)
                newDirection = Direction.NORTH;

            Direction = newDirection;

        }

        public string Report()
        {
            return $"{Location.X},{Location.Y},{Direction}";
        }

        
    }

    public class Location
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        

        public Location(int x, int y)
        {
            Update(x, y);
        }

        public void Update(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class GameMap
    {
        public int MaxSizeX { get; private set; }
       
        public int MaxSizeY { get; private set; }

        public GameMap(int sizeX, int sizeY)
        {
            MaxSizeX = sizeX;
            MaxSizeY = sizeY;
        }

        public bool IsValidLocation(int x, int y)
        {
            return 0 <= x && x <= MaxSizeX && 
                0 <=  y && y <= MaxSizeY;
        }
    }

    
}