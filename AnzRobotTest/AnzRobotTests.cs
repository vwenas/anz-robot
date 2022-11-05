using AnzRobotLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AnzRobotTest
{
    [TestClass]
    public class AnzRobotTests
    {
        private Robot SetupRobot()
        {
            // assuming this is constant at 5 x 5, otherwise we can input this as parameter
            const int gameMapSizeX = 5;
            const int gameMapSizeY = 5;

            var gameMap = new GameMap(gameMapSizeX, gameMapSizeY);

            return new Robot(gameMap);
        }

        private void PlaceRobotAtCentre(Robot robot, Direction direction)
        {
            var xLocation = 3;
            var yLocation = 3;

            robot.Place(xLocation, yLocation, direction);

        }

        [TestMethod]
        public void RobotTestPlaceCorrectly()
        {
            var robot = SetupRobot();
            var xLocation = 3;
            var yLocation = 3;
            var direction = Direction.EAST;

            robot.Place(xLocation, yLocation, direction);

            Assert.AreEqual(xLocation, robot.Location.X);
            Assert.AreEqual(yLocation, robot.Location.Y);
            Assert.AreEqual(direction, robot.Direction);
        }

        [TestMethod]
        public void RobotTestIgnoreInvalidPlacement()
        {
            var robot = SetupRobot();
            var xLocation = 8;
            var yLocation = 3;
            var direction = Direction.EAST;

            robot.Place(xLocation, yLocation, direction);

            Assert.AreEqual(false, robot.IsPlaced);
            
        }

        [TestMethod]
        public void RobotTestMoveNorthCorrectly()
        {
            var robot = SetupRobot();
            PlaceRobotAtCentre(robot, Direction.NORTH);
            var expected = robot.Location.Y + 1;

            robot.Move();
            
            Assert.AreEqual(expected, robot.Location.Y);
            

        }

        [TestMethod]
        public void RobotTestMoveEastCorrectly()
        {
            var robot = SetupRobot();
            PlaceRobotAtCentre(robot, Direction.EAST);
            var expected = robot.Location.X + 1;

            robot.Move();
            
            Assert.AreEqual(expected, robot.Location.X);

        }

        [TestMethod]
        public void RobotTestMoveSouthCorrectly()
        {
            var robot = SetupRobot();
            PlaceRobotAtCentre(robot, Direction.SOUTH);
            var expected = robot.Location.Y - 1;

            robot.Move();
            
            Assert.AreEqual(expected, robot.Location.Y);

        }

        [TestMethod]
        public void RobotTestMoveWestCorrectly()
        {
            var robot = SetupRobot();
            PlaceRobotAtCentre(robot, Direction.WEST);
            var expected = robot.Location.X - 1;

            robot.Move();

            Assert.AreEqual(expected, robot.Location.X);

        }

        [TestMethod]
        public void RobotTestIgnoreInvalidMove()
        {
            var robot = SetupRobot();
            robot.Place(0, 0, Direction.WEST);
            robot.Move();

            Assert.AreEqual(0, robot.Location.X);
        }

        [TestMethod]
        public void RobotTestTurnRightCorrectly()
        {
            var robot = SetupRobot();
            PlaceRobotAtCentre(robot, Direction.NORTH);

            var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();

            foreach (var direction in directions)
            {
                Assert.AreEqual(direction, robot.Direction);
                robot.Right();
            }

            
            // robot comes full circle to starting direction
            Assert.AreEqual(Direction.NORTH, robot.Direction);


        }

        [TestMethod]
        public void RobotTestTurnLeftCorrectly()
        {
            var robot = SetupRobot();
            PlaceRobotAtCentre(robot, Direction.WEST);

            var directions = Enum.GetValues(typeof(Direction)).Cast<Direction>().OrderByDescending(x => x).ToList();

            foreach (var direction in directions)
            {
                Assert.AreEqual(direction, robot.Direction);
                robot.Left();
            }


            // robot comes full circle to starting direction
            Assert.AreEqual(Direction.WEST, robot.Direction);

        }

        [TestMethod]
        public void RobotTestReportCorrectly()
        {
            var robot = SetupRobot();
            PlaceRobotAtCentre(robot, Direction.NORTH);

            var result = robot.Report();
            var expected = $"3,3,NORTH";

            Assert.AreEqual(expected, result);
        }
    }
}