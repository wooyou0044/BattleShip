﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Day15Task
{
    internal class BattleShip
    {
        Player _player;
        Computer _computer;
        GameBoard _board;
        Random rand = new Random();

        public void Play()
        {
            // 플레이어 객체 생성
            _player = new Player();
            _computer = new Computer();

            SetPlayerShip();

            SetComputerShip();


            AttackShip();
        }

        void SetPlayerShip()
        {
            int input = 0;
            int inputNum = 0;
            ConsoleKeyInfo keyInput;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("▼▼▼▼▼BATTLE SHIP▼▼▼▼▼");
            Console.ResetColor();
            Console.WriteLine();

            // 내가 가지고 있는 배의 배열이 모두 다 입력되었을 때
            while (inputNum < _player.ShipsArr.Length)
            {
                Console.WriteLine("1. 항공모함 : 5칸");
                Console.WriteLine("2. 전함 : 4칸");
                Console.WriteLine("3. 구축함 : 3칸");
                Console.WriteLine("4. 잠수함 : 3칸");
                Console.WriteLine("5. 경비정 : 2칸");
                Console.WriteLine();
                Console.Write("배치할 함선 종류 입력 : ");
                int.TryParse(Console.ReadLine(), out input);
                // 0이하 5이상 이거나 이미 입력한 모함종류이면 재입력 요구
                while ((input <= 0) || (input > 5) || !(_player.SetPlayerShips(input)))
                {
                    Console.WriteLine("잘못 입력하셨습니다. 다시 입력해주세요.");
                    Console.Write("배치할 함선 종류 입력 : ");
                    int.TryParse(Console.ReadLine(), out input);
                }
                Console.WriteLine();

                // 입력받은 숫자 - 1에 따라 해당하는 사이즈 대입
                _player.ShipsArr[input - 1].SetShipSize(input);

                while (true)
                {
                    // 예외 처리 필요!!!! => 크기 때문에 인덱스 범위를 벗어나거나 서로 다른 모함이 겹쳤을 때
                    Console.Write("선택한 모함 X 좌표 설정(0 ~ 9 입력) : ");
                    int posX = int.Parse(Console.ReadLine());
                    Console.Write("선택한 모함 Y 좌표 설정(0 ~ 9 입력) : ");
                    int posY = int.Parse(Console.ReadLine());

                    // 받은 X, Y좌표로 해당 배열에 위치 대입
                    // 수정 필요 : ship의 위치배열이 [posX, posY]이여야 함
                    Vector2 shipPos = new Vector2(posX, posY);
                    _player.ShipsArr[input - 1].ShipLocation[0] = shipPos;

                    // 세로로 둘 것인지 가로로 둘 것인지 입력받기
                    Console.Write("선택한 모함 위치할 모양 (1. 가로, 2. 세로) : ");
                    int inputAxis = int.Parse(Console.ReadLine());
                    // 인덱스 범위 내에 있으면 탈출
                    if(!_player.ShipsArr[input - 1].SetShip(inputAxis))
                    {
                        break;
                    }
                    // 인덱스 범위 외에 있으면 다시 입력해달라고 출력하고 다시 반복
                    else
                    {
                        Console.WriteLine("인덱스의 범위를 벗어났습니다.");
                        Console.WriteLine("다시 입력해주세요");
                        Console.WriteLine();
                    }

                }

                Console.WriteLine();
                // 배열에 넣은것 성공하면 +1씩 증가
                inputNum++;
            }

            Console.WriteLine("모든 모함 세팅 완료했습니다.");
            Console.Write("게임을 시작하시겠습니까?(Y/N) ");
            keyInput = Console.ReadKey();
            Console.WriteLine();
            while (keyInput.Key != ConsoleKey.Y)
            {
                if (keyInput.Key == ConsoleKey.N)
                {
                    Console.WriteLine("게임을 종료합니다.");
                    break;
                }
                Console.WriteLine("다시 입력해주세요. 게임은 시작해야 합니다.");
                Console.Write("게임을 시작하시겠습니까?(Y/N) ");
                keyInput = Console.ReadKey();
                Console.WriteLine();
            }

            if (keyInput.Key == ConsoleKey.Y)
            {
                Console.WriteLine("게임을 시작합니다.");
                _board = new GameBoard(_player);
                _board.PlayerAttackComInfoBoard(true);
                _board.PrintGameBoard(false);
            }
        }

        void SetComputerShip()
        {
            // 컴퓨터가 랜덤으로 돌아가서 세팅하게 설정
            int setShipNum = 0;

            while (setShipNum < _computer.ComshipsArr.Length)
            {
                _computer.SetComputerShips(setShipNum + 1);
                // setShipNum + 1 사이즈에 맞는 배 크기 지정
                _computer.ComshipsArr[setShipNum].SetShipSize(setShipNum + 1);

                while (true)
                {
                    int posX = rand.Next(0, 10);
                    int posY = rand.Next(0, 10);

                    // 랜덤 X, Y좌표로 해당 배열에 위치 대입
                    Vector2 shipPos = new Vector2(posX, posY);
                    _computer.ComshipsArr[setShipNum].ShipLocation[0] = shipPos;

                    // 랜덤으로 세로로 둘 것인지 가로로 둘 것인지 설정
                    int inputAxis = rand.Next(1, 3);
                    // 인덱스 범위 내에 있으면 탈출
                    if (!_computer.ComshipsArr[setShipNum].SetShip(inputAxis))
                    {
                        break;
                    }
                    else
                    {
                        posX = rand.Next(0, 10);
                        posY = rand.Next(0, 10);

                        // 랜덤 X, Y좌표로 해당 배열에 위치 대입
                        shipPos = new Vector2(posX, posY);
                        _computer.ComshipsArr[setShipNum].ShipLocation[0] = shipPos;
                    }

                }
                // 배열에 넣은것 성공하면 +1씩 증가
                setShipNum++;
            }

            _board = new GameBoard(_computer);
            _board.PrintComputerGameBoard(false);

            Console.WriteLine("상대방인 컴퓨터도 세팅 완료했습니다.");
            Console.WriteLine();
        }

        void AttackShip()
        {
            int posX = 0;
            int posY = 0;
            Vector2 comShipPos;
            Vector2 playerShipPos;
            int playerAttackNum = 0;
            int comAttackNum = 0;

            bool isComAttack = false;
            bool isPlayerAttack = false;

            string shipPlayerName = "";
            string shipComName = "";

            // 플레이어가 지거나 컴퓨터가 졌을 때까지 반복
            while (comAttackNum < 17 && playerAttackNum < 17)
            {
                Console.Write("공격할 X 좌표 입력해주세요(0 ~ 9 입력) : ");
                int.TryParse(Console.ReadLine(), out posX);
                Console.Write("공격할 Y 좌표 입력해주세요(0 ~ 9 입력) : ");
                int.TryParse(Console.ReadLine(), out posY);
                Console.WriteLine();
                for (int i = 0; i < _computer.ComshipsArr.Length; i++)
                {
                    for (int j = 0; j < _computer.ComshipsArr[i].ShipSize; j++)
                    {
                        comShipPos = _computer.ComshipsArr[i].ShipLocation[j];
                        // 입력한 X,Y 좌표와 컴퓨터 배들 중 하나의 위치와 같다면
                        if (comShipPos.posX == posX && comShipPos.posY == posY)
                        {
                            // GameBoard로 좌표를 보내서 그 좌표에 있는 배를 □로 만들고 배열에서는 0으로 바꿈
                            // 내 쪽 위에 있는 보드판에 맞췄다는 표시로 빨간색 핀 ●을 꽂아 표시
                            _board.ComputerBoard[posY, posX] = 0;
                            _computer.ComshipsArr[j].IsDamaged = true;
                            shipComName = _computer.ComshipsArr[i].ShipName;
                            isPlayerAttack = true;
                            playerAttackNum++;
                            break;
                        }
                        // 컴퓨터 배들 중 하나의 위치를 맞추지 못했다면 내 쪽에 있는 보드판에 못 맞췄다는 표시로 하얀색 핀을 꽂아 표시
                        else
                        {
                            _board.BoardAttackInfo[posY, posX] = 1;
                        }
                    }
                    if (isPlayerAttack)
                    {
                        break;
                    }
                }

                // 컴퓨터가 플레이어 배를 랜덤으로 돌린 x,y 좌표로 공격
                posX = rand.Next(0, 10);
                posY = rand.Next(0, 10);

                // 랜덤으로 돌린 숫자가 전에 공격한 숫자라면 다시 랜덤 돌리기

                for(int i=0; i<_player.ShipsArr.Length;i++)
                {
                    for(int j=0; j < _player.ShipsArr[i].ShipSize; j++)
                    {
                        playerShipPos = _player.ShipsArr[i].ShipLocation[j];
                        if(playerShipPos.posX == posX && playerShipPos.posY == posY)
                        {
                            _board.BoardArr[posY, posX] = 0;
                            _player.ShipsArr[j].IsDamaged = true;
                            shipPlayerName = _player.ShipsArr[i].ShipName;
                            isComAttack = true;
                            comAttackNum++;
                            break;
                        }
                    }
                    if(isPlayerAttack)
                    {
                        break;
                    }
                }

                _board.PlayerAttackComInfoBoard(true);
                _board.PrintGameBoard(true);
                Console.WriteLine();
                Console.WriteLine();
                _board.PrintComputerGameBoard(true);
                if (isComAttack)
                {
                    Console.WriteLine($"상대방에게 내 배의 한 쪽이 공격받았습니다. 종류 : {shipPlayerName}");
                    Console.WriteLine($"X 좌표 : {posX}\tY 좌표 : {posY}");
                    isComAttack = false;
                }
                if (isPlayerAttack)
                {
                    Console.WriteLine($"상대방 배의 한 쪽을 맞췄습니다. 종류 : {shipComName}");
                    isPlayerAttack = false;
                }
            }

            if(comAttackNum == 17)
            {
                comAttackNum = 0;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("컴퓨터가 승리했습니다.");
                Console.ResetColor();
            }

            if(playerAttackNum == 17)
            {
                playerAttackNum = 0;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("♥♥♥♥♥♥♥♥♥♥♥♥♥");
                Console.WriteLine("플레이어가 승리했습니다.");
                Console.WriteLine("♥♥♥♥♥♥♥♥♥♥♥♥♥");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
}
