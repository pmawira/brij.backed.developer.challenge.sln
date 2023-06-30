using Elevator.Engine.Definitions;
using Elevator.Engine.Enums;
using Elevator.Engine.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Services
{
    public class ElevatorPoolService : IElevatorPoolService
    {
        private List<ElevatorCar> _availableElevators;

        public void UpdateElevatorState(ElevatorCar elevator)
        {            //
            var elevatorToUpdate = _availableElevators.FirstOrDefault(e => e.Number == elevator.Number);
            if (elevatorToUpdate == null)
            {
                return;
            }

            var index = _availableElevators.IndexOf(elevatorToUpdate);
            _availableElevators[index] = elevator;
        }


        public void CreateElevatorPool(int _numberOfElevators)
        {
            var elevators = new List<ElevatorCar>();

            for (int i = 0; i < _numberOfElevators; i++)
            {
                elevators.Add(new ElevatorCar(20, ElevatorState.Idle, 1, 0)
                { Number = i + 1 });
            }

            _availableElevators = elevators;

        }

        private ElevatorCar GetNearestElevator(int requestedFloor, int numberOfPeopleWaiting)
        {
            // Get all active elevators
            var elevators = _availableElevators.Where(e => e.State != ElevatorState.Faulty && e.MaximumCapacity > e.NumberOfPeopleOnBoard + numberOfPeopleWaiting).ToList();
            //ToDo:
            // Check if all the elevator are on the floor 1. if yes bring the first one

          
            //ToDO:
            // check if they are all on the top most floor, if yes bring the any of them
         

            ElevatorCar nearestElevator = null; ;
            int shortestDistance = int.MaxValue;

            if (nearestElevator == null)
            {
                Console.WriteLine("All elevators are currently busy. Please try again later.");
                return null;
            }
            //check for the nearest available elevator;
            foreach (ElevatorCar elevator in elevators)
            {
                // check if there is any elevator at requestedFloor

                if (elevator.CurrentFloor.Number == requestedFloor)
                {
                    nearestElevator = elevator;
                    break;
                }

                var distance = Math.Abs(elevator.CurrentFloor.Number - requestedFloor);
                if (distance < shortestDistance)
                {
                    nearestElevator = elevator;
                    shortestDistance = distance;
                }

            }
            return nearestElevator;
        }
        private void SendToFloor(int targetFloor, ElevatorCar elevator)
        {
            elevator.CurrentFloor.Number = targetFloor;

            // Simulate the movement of the elevator

            if (elevator.CurrentFloor.Number == targetFloor)
            {
                elevator.State = ElevatorState.Loading;
            }

            if (targetFloor < elevator.CurrentFloor.Number)
                elevator.Direction = Direction.Down;
            else
                elevator.Direction = Direction.Up;

            elevator.State = ElevatorState.Moving;
        }

        public void AddPeople(int numberOfPeopleBoarding, int floor)
        {
            var elevator = GetNearestElevator(floor, numberOfPeopleBoarding);
            SendToFloor(floor, elevator);
            
            if (elevator.NumberOfPeopleOnBoard + numberOfPeopleBoarding > elevator.MaximumCapacity)
            {
                Console.WriteLine("Elevator is at maximum capacity.");
                return;
            }

            elevator.NumberOfPeopleOnBoard += numberOfPeopleBoarding;
            elevator.CurrentFloor.Number = floor;
            Console.WriteLine($"{numberOfPeopleBoarding} people boarded the elevator {elevator.Number}. Current number of people: {elevator.NumberOfPeopleOnBoard}.");
        }
        public void RemovePeople(int numberOfPeopleToRemove, int floor, int elevatorNumber)
        {
            var elevator = _availableElevators.FirstOrDefault(e => e.Number == elevatorNumber);

            if (elevator == null)
            {
                Console.WriteLine($"The elevator {elevatorNumber} is not in the system, try another one");
                return;
            }
            if (floor != elevator.CurrentFloor.Number)
            {
                Console.WriteLine($"Elevator {elevator.Number} is in motion, cannot remove people");
                return;
            }

            if (elevator.NumberOfPeopleOnBoard - numberOfPeopleToRemove < 0)
            {
                Console.WriteLine("Elevator has no people.");
                return;
            }

            elevator.NumberOfPeopleOnBoard -= numberOfPeopleToRemove;

            Console.WriteLine($"{numberOfPeopleToRemove} people alighted from the elevator. Current number of people: {elevator.NumberOfPeopleOnBoard}.");
        }
        public void Log(ElevatorCar elevator)
        {
            Console.WriteLine($"Current floor: {elevator.CurrentFloor}");
            Console.WriteLine($"State: {elevator.State}");
            Console.WriteLine($"Current direction: {elevator.Direction}");
            Console.WriteLine($"Number of people: {elevator.NumberOfPeopleOnBoard}");
            Console.WriteLine();
        }
        /// <summary>
        /// get elevator status
        /// </summary>
        public void Logs()
        {
            for (int i = 0; i < _availableElevators.Count; i++)
            {
                ElevatorCar elevator = _availableElevators[i];
                Console.WriteLine($"Elevator {i + 1}:");
                Console.WriteLine($"Current floor: {elevator.CurrentFloor}");
                Console.WriteLine($"State: {elevator.State}");
                Console.WriteLine($"Current direction: {elevator.Direction}");
                Console.WriteLine($"Number of people: {elevator.NumberOfPeopleOnBoard}");
                Console.WriteLine();

            }
        }

    }
}
