using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15Task
{
    class Player
    {
        Ship[] _ships;
        

        public Ship[] ShipsArr
        {
            get
            {
                return _ships;
            }

            set
            {
                _ships = value;
            }
        }

        public Player()
        {
            // 5개 종류 개수만큼 배열 크기 생성
            _ships = new Ship[5];
        }

        public bool SetPlayerShips(int shipArrIndexInput)
        {
            if (_ships[shipArrIndexInput - 1] != null)
            {
                Console.WriteLine("이미 입력한 모함 종류입니다.");
                return false;
            }
            else
            {
                _ships[shipArrIndexInput - 1] = new Ship(shipArrIndexInput);
                return true;
            }
        }

    }
}
