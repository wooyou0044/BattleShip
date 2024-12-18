using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15Task
{
    class GameBoard
    {
        static int[,] _boardArr;
        int _width;
        int _height;
        Player _player;
        // 임시
        Computer _computer;
        int[,] _computerBoard;

        // 컴퓨터 배 공격한 위치 표시하기 위한 board 하나 더 필요해서 board 하나 선언
        int[,] _boardAttackArr;

        public int[,] BoardArr
        {
            get
            {
                return _boardArr;
            }
            set
            {
                _boardArr = value;
            }
        }

        // 임시
        public int[,] ComputerBoard
        {
            get
            {
                return _computerBoard;
            }
            set
            {
                _computerBoard = value;
            }
        }

        public int[,] BoardAttackInfo
        {
            get
            {
                return _boardAttackArr;
            }
            set
            {
                _boardAttackArr = value;
            }
        }

        public GameBoard(Player player)
        {
            _width = 10;
            _height = 10;
            _boardArr = new int[_height, _width];
            _player = player;
        }

        // 임시
        public GameBoard(Computer computer)
        {
            _width = 10;
            _height = 10;
            _computerBoard = new int[_height, _width];
            _computer = computer;
        }

        // 게임 보드 출력하는 함수
        public void PrintGameBoard(bool isMadeOnce)
        {
            if (!isMadeOnce)
            {
                int boardPosX = 0;
                int boardPosY = 0;

                for (int i = 0; i < _player.ShipsArr.Length; i++)
                {
                    for (int j = 0; j < _player.ShipsArr[i].ShipSize; j++)
                    {
                        boardPosX = _player.ShipsArr[i].ShipLocation[j].posX;
                        boardPosY = _player.ShipsArr[i].ShipLocation[j].posY;
                        _boardArr[boardPosY, boardPosX] = _player.ShipsArr[i].ShipNum;
                    }
                }
            }

            Console.WriteLine("   0　 1　 2　 3　 4　 5　 6　 7　 8　 9");
            Console.WriteLine("  ―　―　―　―　―　―　―　―　―　 ―");

            for (int i = 0; i < _boardArr.GetLength(0) * 2; i++)
            {
                for (int j = 0; j < _boardArr.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j == 0)
                        {
                            Console.Write(i / 2);
                            Console.Write("|");
                        }

                        if (_boardArr[i / 2, j] == 0)
                        {
                            Console.Write("　");
                        }
                        else
                        {
                            ReturnShipColor(_boardArr[i / 2, j]);
                            Console.Write("■");
                            Console.ResetColor();
                        }
                        Console.Write("｜");
                    }
                    else if (i % 2 != 0)
                    {
                        Console.Write("  ―");
                    }
                }
                Console.WriteLine();
            }
        }

        // 임시로 잘 세팅되었는지 컴퓨터 보드도 뽑아보기
        public void PrintComputerGameBoard(bool isMadeOnce)
        {
            if(!isMadeOnce)
            {
                int boardPosX = 0;
                int boardPosY = 0;

                Console.WriteLine();
                Console.WriteLine();

                for (int i = 0; i < _computer.ComshipsArr.Length; i++)
                {
                    for (int j = 0; j < _computer.ComshipsArr[i].ShipSize; j++)
                    {
                        boardPosX = _computer.ComshipsArr[i].ShipLocation[j].posX;
                        boardPosY = _computer.ComshipsArr[i].ShipLocation[j].posY;
                        _computerBoard[boardPosY, boardPosX] = _computer.ComshipsArr[i].ShipNum;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("★★★★★★★Computer 보드★★★★★★★");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("   0　 1　 2　 3　 4　 5　 6　 7　 8　 9");
            Console.WriteLine("  ―　―　―　―　―　―　―　―　―　 ―");

            for (int i = 0; i < _computerBoard.GetLength(0) * 2; i++)
            {
                for (int j = 0; j < _computerBoard.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j == 0)
                        {
                            Console.Write(i / 2);
                            Console.Write("|");
                        }

                        if (_computerBoard[i / 2, j] == 0)
                        {
                            Console.Write("　");
                        }
                        else
                        {
                            ReturnShipColor(_computerBoard[i / 2, j]);
                            Console.Write("■");
                            Console.ResetColor();
                        }
                        Console.Write("｜");
                    }
                    else if (i % 2 != 0)
                    {
                        Console.Write("  ―");
                    }
                }
                Console.WriteLine();
            }
        }

        public void ReturnShipColor(int color)
        {
            switch(color)
            {
                case (int)ShipColor.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case (int)ShipColor.Magenta:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case (int)ShipColor.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case (int)ShipColor.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case (int)ShipColor.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }

        // player 쪽에 새로운 아무것도 없는 보드판 필요 => 컴퓨터 배를 못 맞췄을 때 어떤 위치에 쏘았는지 확인하기 위한 보드판
        // 내 보드판 보다 먼저 나와야 함
        public void PlayerAttackComInfoBoard(bool isGameStart)
        {
            _boardAttackArr = new int[_height, _width];
            if (isGameStart)
            {
                Console.Clear();
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("★★★★★★★Player 보드★★★★★★★");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("   0　 1　 2　 3　 4　 5　 6　 7　 8　 9");
            Console.WriteLine("  ―　―　―　―　―　―　―　―　―　 ―");

            for (int i = 0; i < _boardAttackArr.GetLength(0) * 2; i++)
            {
                for (int j = 0; j < _boardAttackArr.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        if (j == 0)
                        {
                            Console.Write(i / 2);
                            Console.Write("|");
                        }
                        if (_boardAttackArr[i / 2, j] == 0)
                        {
                            Console.Write("　");
                        }
                        Console.Write("｜");
                    }
                    else if (i % 2 != 0)
                    {
                        Console.Write("  ―");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
