using Elevator.Engine.Models;

namespace Elevator.Engine.Definitions
{
    public interface IFloorService
    {
        void AddPeople(int count, Floor floor);
        void CreateFloors(int floors);
    }
}