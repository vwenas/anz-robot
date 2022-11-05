using AnzRobotLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnzRobotCore
{
    public interface IRobotRunner
    {
        List<string> RunCommands(string text, bool showCommands = false);


    }

    public class RobotRunner : IRobotRunner
    {
        
        
        public List<string> RunCommands(string text, bool showCommands = false)
        {
            const string placeCommand = "PLACE";
            const string moveCommand = "MOVE";
            const string leftCommand = "LEFT";
            const string rightCommand = "RIGHT";
            const string reportCommand = "REPORT";


            var validKeywords = new List<string> { placeCommand, moveCommand, leftCommand, rightCommand, reportCommand };
            
            var carriageReturns = new [] { "\r", "\n", "\r\n" };

            var output = new List<string>();

            // clean data and convert to upper case
            text = text.ToUpper();

            var commands = text
                .Split(carriageReturns, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToList();

            // get only the commands that starts with valid keywords
            var validCommands = commands
                .Where(c => validKeywords.Contains(c.Split(" ").First()))
                .ToList();

            var gameMap = new GameMap(5, 5);
            var robot = new Robot(gameMap);

            bool isRobotPlaced = false;
            
            foreach (var command in validCommands)
            {
                var commandList = command.Split(" ").ToList();

                var commandKeyword = commandList.First();

                if (commandKeyword == placeCommand)
                {
                    var commandDetails = commandList.Last();

                    // try to parse location and direction
                    var locationStr = commandDetails.Split(",").ToList();
                    var x = Convert.ToInt32(locationStr[0]);
                    var y = Convert.ToInt32(locationStr[1]);
                    var direction = Enum.Parse<Direction>(locationStr[2], true);

                    robot.Place(x, y, direction);

                    output.Add(command);

                    isRobotPlaced = true;
                    continue;
                }

                // don't execute anything until first PLACE command is executed
                if (!isRobotPlaced) continue;

                switch (commandKeyword)
                {
                    case moveCommand:
                        robot.Move();
                        output.Add(command);
                        break;

                    case leftCommand:
                        robot.Left();
                        output.Add(command);
                        break;

                    case rightCommand:
                        robot.Right();
                        output.Add(command);
                        break;

                    case reportCommand:
                        output.Add(command);
                        output.Add($"Output: {robot.Report()}");
                        break;
                }

            }

            return output;
        }

        
    }
}
