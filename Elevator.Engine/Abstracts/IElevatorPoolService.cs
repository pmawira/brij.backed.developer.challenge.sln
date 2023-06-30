using Elevator.Engine.Models;

namespace Elevator.Engine.Definitions
{
    public interface IElevatorPoolService
    {
        void CreateElevatorPool(int numberOfElevators);

        void AddPeople(int numberOfPeopleBoarding, int floor);
        void RemovePeople(int numberOfPeopleToRemove, int floor, int elevatorNumber);
        
        void UpdateElevatorState(ElevatorCar elevator); 
        void Logs();

    }
}