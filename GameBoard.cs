using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15Task
{
    class GameBoard
    {
        string[,] _boardArr;
        int _width;
        int _height;
        Player _player;
        // 임시
        Computer _computer;

        public GameBoard(Player player)
        {
            _width = 10;
            _height = 10;
            _boardArr = new string[_height, _width];
            _player = player;
        }

        // 임시
        public GameBoard(Computer computer)
        {
            _width = 10;
            _height = 10;
            _boardArr = new string[_height, _width];
            _computer = computer;
        }

        // 게임 보드 출력하는 함수
        public void PrintGameBoard(bool isGameStart)
        {
            string space = "　";
            string ship = "■";
            int boardPosX = 0;
            int boardPosY = 0;
            int colorNum = 0;

            for(int i=0; i<_player.ShipsArr.Length; i++)
            {
                for (int j = 0; j < _player.ShipsArr[i].ShipSize; j++)
                {
                    boardPosX = _player.ShipsArr[i].ShipLocation[j].posX;
                    boardPosY = _player.ShipsArr[i].ShipLocation[j].posY;
                    _boardArr[boardPosY, boardPosX] = ship;

                    colorNum = _player.ShipsArr[i].ShipNum;
                }
            }

            if(isGameStart)
            {
                Console.Clear();
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("★★★★★★★Player 보드★★★★★★★");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("   0　 1　 2　 3　 4　 5　 6　 7　 8　 9");

            for (int i=0; i<_boardArr.GetLength(0) * 2; i++)
            {
                for(int j=0; j<_boardArr.GetLength(1); j++)
                {
                    if (i % 2 == 0)
                    {
                        if(j == 0)
                        {
                            Console.Write(i / 2);
                            Console.Write("|");
                        }
                        if(_boardArr[i / 2, j] != ship)
                        {
                            _boardArr[i / 2, j] = space;
                            Console.Write(_boardArr[i / 2, j]);
                        }
                        else
                        {
                            // 배들 마다 색상 다르게 하기
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(_boardArr[i / 2, j]);
                            Console.ResetColor();
                        }
                        //Console.Write(_boardArr[i / 2, j]);
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
        public void PrintComputerGameBoard()
        {
            string space = "　";
            string ship = "■";
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
                    _boardArr[boardPosY, boardPosX] = ship;
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("★★★★★★★Computer 보드★★★★★★★");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("   0　 1　 2　 3　 4　 5　 6　 7　 8　 9");

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
                        if (_boardArr[i / 2, j] != ship)
                        {
                            _boardArr[i / 2, j] = space;
                            Console.Write(_boardArr[i / 2, j]);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(_boardArr[i / 2, j]);
                            Console.ResetColor();
                        }
                        //Console.Write(_boardArr[i / 2, j]);
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

        public void ReturnShipColor(ShipColor color)
        {
            switch(color)
            {
                case ShipColor.Red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case ShipColor.Magenta:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case ShipColor.Blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case ShipColor.Green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case ShipColor.Yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }
    }
}
