using Elevator.Engine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator.Engine.Models
{


    public class ElevatorCar
    {
        public int NumberOfPeopleOnBoard { get;  set; }
        public Floor CurrentFloor { get; set; }

        /// <summary>
        /// Maximum weight expressed as number of people
        /// </summary>
        public int MaximumCapacity { get;  set; }
        public ElevatorState State { get; set; }
        public Direction Direction { get; set; }
        public int Number { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maximumCapacity"></param>
        /// <param name="state"></param>
        /// <param name="floor"></param>
        /// <param name="numberOfPeople"></param>
        public ElevatorCar(int maximumCapacity, ElevatorState state, int floor, int numberOfPeople)
        {
            // Assuming elevator default floor is 1
            CurrentFloor.Number = floor;
            State = state;
            Direction = Direction.Up;//Assume floor 1 is the first
            NumberOfPeopleOnBoard = numberOfPeople;
            MaximumCapacity = maximumCapacity;
        }       
    }
}
