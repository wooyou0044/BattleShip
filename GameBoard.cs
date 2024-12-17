using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15Task
{
    class GameBoard
    {
        int[,] _boardArr;
        int _width;
        int _height;
        Player _player;
        // 임시
        Computer _computer;

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
            _boardArr = new int[_height, _width];
            _computer = computer;
        }

        // 게임 보드 출력하는 함수
        public void PrintGameBoard(bool isGameStart)
        {
            int boardPosX = 0;
            int boardPosY = 0;

            for(int i=0; i<_player.ShipsArr.Length; i++)
            {
                for (int j = 0; j < _player.ShipsArr[i].ShipSize; j++)
                {
                    boardPosX = _player.ShipsArr[i].ShipLocation[j].posX;
                    boardPosY = _player.ShipsArr[i].ShipLocation[j].posY;
                    _boardArr[boardPosY, boardPosX] = _player.ShipsArr[i].ShipNum;
                }
            }

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
                        if(_boardArr[i / 2, j] == 0)
                        {
                            Console.Write("　");
                        }
                        else
                        {
                            // 배들 마다 색상 다르게 하기
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
        public void PrintComputerGameBoard()
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
                    _boardArr[boardPosY, boardPosX] = _computer.ComshipsArr[i].ShipNum;
                }
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("★★★★★★★Computer 보드★★★★★★★");
            Console.ResetColor();
            Console.WriteLine();
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
    }
}
