using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15Task
{
    class Computer
    {
        Ship[] _comShips;

        public Ship[] ComshipsArr
        {
            get
            {
                return _comShips;
            }
            set
            {
                _comShips = value;
            }
        }

        public Computer()
        {
            _comShips = new Ship[5];
        }

        public void SetComputerShips(int shipArrIndexInput)
        {
            _comShips[shipArrIndexInput - 1] = new Ship(shipArrIndexInput);
        }
    }
}
