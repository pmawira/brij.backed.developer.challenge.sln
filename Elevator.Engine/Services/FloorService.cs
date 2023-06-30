using Elevator.Engine.Definitions;
using Elevator.Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Services
{


    public class FloorService : IFloorService
    {
        private readonly int _numberOfFloors;
        private readonly List<Floor> _floorList;      
        public void AddPeople(int count, Floor floor)
        {
            floor.NumberOfPeopleWaiting = count;
        }

        public void CreateFloors(int numberOfFloors)
        {
            for (int i = 0; i < numberOfFloors; i++)
            {
                var floor = new Floor();

                _floorList.Add(floor);
            }
        }
    }
}
