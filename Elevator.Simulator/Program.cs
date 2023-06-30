using Elevator.Engine;
using Elevator.Engine.Definitions;
using Elevator.Engine.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Elevator.Simulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Configure DI
            //Create service collection
            var service = new ServiceCollection();

            //register services
            service.AddSingleton<IElevatorPoolService, ElevatorPoolService>();
            service.AddScoped<IFloorService, FloorService>();
            service.AddTransient<IElevatorMonitorService, ElevatorMonitorService>();

            //build service provider
            var serviceProvider = service.BuildServiceProvider();

            var poolService = serviceProvider.GetService<IElevatorPoolService>();
            var floorService = serviceProvider.GetService<IFloorService>();
            var monitorService = serviceProvider?.GetService<IElevatorMonitorService>();


            int numberOfElevators = 0;
            int numberOfFloors = 0;
            int maximumCapacity = 0;
            string? operationOption = string.Empty;

            Console.WriteLine("Enter the number of Elevators:");
            int.TryParse(Console.ReadLine(), out numberOfElevators);

            Console.WriteLine("Please enter the number of floors:");
            int.TryParse(Console.ReadLine(), out numberOfFloors);
            Console.WriteLine("Please Enter maximum capacity");
            int.TryParse(Console.ReadLine(), out maximumCapacity);
            Console.WriteLine("Select operation in the options:");
            Console.WriteLine("1: Call elevator");
            Console.WriteLine("2: Remove people");
            Console.WriteLine("3: Show Elevator status");


            operationOption = Console.ReadLine();

            poolService.CreateElevatorPool(numberOfElevators);
            floorService.CreateFloors(numberOfFloors);

            if (operationOption == "1")
            {
                Console.WriteLine("Enter requested floor and number of people waiting, separated by comma respectively");
            }
            else if (operationOption == "2")
            {
                Console.WriteLine("Enter requested floor, number of people waiting and elevator number, separated by comma respectively");
            }
            else
            {
                monitorService.LogAll();
            }
            var inputs = Console.ReadLine()?.Split(",").ToArray();
            if (operationOption == "1")
            {
                //
                poolService.AddPeople(int.Parse(inputs[1]), int.Parse(inputs[0]));
            }
            else
            {
                //
                poolService.RemovePeople(int.Parse(inputs[1]), int.Parse(inputs[0]), int.Parse(inputs[2]));
            }
        }
    }
}