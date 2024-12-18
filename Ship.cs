using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15Task
{
    enum ShipColor
    {
        Red = 1,
        Magenta,
        Blue,
        Green,
        Yellow
    }

    struct Vector2
    {
        public int posX;
        public int posY;

        public Vector2(int x,int y)
        {
            posX = x;
            posY = y;
        }
    }

    internal class Ship
    {
        Vector2[] _location;
        int _size;
        // 공격을 받았는지에 대한 참거짓 여부 선언
        bool _isDamaged;
        // 배마다 색상 다르게 하기 위해 일단 선언
        int _shipNum;
        // 중복이 있는지 확인하기 위한 배열
        static int[,] _intShipArr = new int[10,10];

        // 상대방에게 어떤 배였는지 얘기하기 위해 문자열 선언
        string _shipName;

        // 배열이 넘으면 예외처리 필요
        public Vector2[] ShipLocation
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        public int ShipSize
        {
            get
            {
                return _size;
            }
        }

        public int ShipNum
        {
            get
            {
                return _shipNum;
            }
        }

        public string ShipName
        {
            get
            {
                return _shipName;
            }
        }

        public int[,] IntShipArr
        {
            get
            {
                return _intShipArr;
            }
            set
            {
                _intShipArr = value;
            }
        }

        public bool IsDamaged
        {
            get
            {
                return _isDamaged;
            }
            set
            {
                _isDamaged = value;
            }
        }

        public Ship(int input)
        {
            int size = SetShipSize(input);
            ShipLocation = new Vector2[size];
            IsDamaged = false;
        }

        // ship 사이즈를 결정하는 함수
        public int SetShipSize(int input)
        {
            switch(input)
            {
                case 1:
                    _size = 5;
                    _shipNum = 1;
                    _shipName = "항공모함";
                    break;
                case 2:
                    _size = 4;
                    _shipNum = 2;
                    _shipName = "전함";
                    break;
                case 3:
                    _size = 3;
                    _shipNum = 3;
                    _shipName = "구축함";
                    break;
                case 4:
                    _size = 3;
                    _shipNum = 4;
                    _shipName = "잠수함";
                    break;
                case 5:
                    _size = 2;
                    _shipNum = 5;
                    _shipName = "경비정";
                    break;
            }
            return _size;
        }

        // Ship을 가로로 놓을지 세로로 놓을지에 따라 Vector2 배열에 값을 저장하는 함수
        // 예외 처리 필요!!!! => 크기 때문에 인덱스 범위를 벗어나거나 서로 다른 모함이 겹쳤을 때
        public bool SetShip(int inputAxis)
        {
            bool isCantMakeShip = false;

            if (inputAxis == 1)
            {
                for (int i=0; i<_size; i++)
                {
                    if(i>=1)
                    {
                        ShipLocation[i].posX = ShipLocation[i - 1].posX + 1;
                        ShipLocation[i].posY = ShipLocation[0].posY;
                    }

                    if (ShipLocation[i].posX > 9)
                    {
                        ShipLocation[i].posX = 0;
                        isCantMakeShip = true;
                        break;
                    }

                    if (IntShipArr[ShipLocation[i].posY, ShipLocation[i].posX] == 1)
                    {
                        isCantMakeShip = true;
                        break;
                    }

                    // posX, posY 위치에 해당하는 GameBoard에 선언한 int 배열을 1로 변경
                    IntShipArr[ShipLocation[i].posY, ShipLocation[i].posX] = 1;
                }
            }
            if(inputAxis == 2)
            {
                for (int i=0; i<_size; i++)
                {
                    if (i>=1)
                    {
                        ShipLocation[i].posY = ShipLocation[i - 1].posY + 1;
                        ShipLocation[i].posX = ShipLocation[0].posX;
                    }

                    if (ShipLocation[i].posY > 9)
                    {
                        ShipLocation[i].posY = 0;
                        isCantMakeShip = true;
                        break;
                    }

                    if (IntShipArr[ShipLocation[i].posY, ShipLocation[i].posX] == 1)
                    {
                        isCantMakeShip = true;
                        break;
                    }
                    // posX, posY 위치에 해당하는 GameBoard에 선언한 int 배열을 1로 변경
                    IntShipArr[ShipLocation[i].posY, ShipLocation[i].posX] = 1;
                }
            }

            else if(inputAxis > 2 || inputAxis < 1)
            {
                isCantMakeShip = true;
            }
            return isCantMakeShip;
        }
    }
}
