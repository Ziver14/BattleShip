using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public enum ShotStatus // статус выстрела
    {
        Miss,     // мимо
        Wounded,  // ранил
        Kill,     // убил
        EndBattle // конец боя
    }

    public enum CoordStatus // статус координат
    {
        None,     // пусто
        Ship,     // Корабль
        Shot,     // Выстрел
        Got       // Попал 
    }

    public enum ShipType // Типы кораблей
    {
        x4,x3, x2,x1
    }

    public enum Direction // Направление
    {
        Horizontal, Vertical
    }


    public class Model
    {
        //Поле кооринат своих кораблей (кораблей игрока)
        public CoordStatus[,] PlayerShips = new CoordStatus[10,10];
        public CoordStatus[,] EnemyShips = new CoordStatus[10,10];

        //Колличество клеток кораблей противника
        public int UndiscoverCells = 20;

        //Поле статуса последнего выстрела
        public ShotStatus LastShot;
        
        //Поле статус ранения
        public bool WoundedStatus;

        //Поле координат последнего выстрела
        public string? LastShootCord;

        //Статус первого выстрела
        bool FirstGot;

        //Конструктор. Инициализация полей модели.
        public Model() 
        {
            LastShot = ShotStatus.Miss;
            WoundedStatus = false;
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    PlayerShips[i,j] = CoordStatus.None;
                    EnemyShips[i, j] = CoordStatus.None;
                }
        }

        //Метод выстрела. Входящий параметр - координаты выстрела в виде строки из двух цифр.
        public ShotStatus Shot(string ShotCoord)
        {
            ShotStatus result = ShotStatus.Miss;
            int x, y; // координаты выстрела (х - строка, у - столбец)
            x = int.Parse(ShotCoord.Substring(0,1));
            y = int.Parse(ShotCoord.Substring(1));

            if (PlayerShips[x, y] == CoordStatus.None)
            { result = ShotStatus.Miss; }
            else
            {
                result = ShotStatus.Kill;
                if ((x!=9 && PlayerShips[x + 1, y] == CoordStatus.Ship)||
                    (y!=9 && PlayerShips[x, y + 1] == CoordStatus.Ship)||
                    (x!=0 && PlayerShips[x - 1, y] == CoordStatus.Ship)||
                    (y!=0 && PlayerShips[x, y - 1] == CoordStatus.Ship)||
                    (x!=8 && PlayerShips[x + 2, y] == CoordStatus.Ship)||
                    (y!=8 && PlayerShips[x, y + 2] == CoordStatus.Ship)||
                    (x!=1 && PlayerShips[x - 2, y] == CoordStatus.Ship)||
                    (y!=1 && PlayerShips[x, y - 2] == CoordStatus.Ship)||
                    (x!=7 && PlayerShips[x + 3, y] == CoordStatus.Ship)||
                    (y!=7 && PlayerShips[x, y + 3] == CoordStatus.Ship)||
                    (x!=2 && PlayerShips[x - 3, y] == CoordStatus.Ship)||
                    (y!=2 && PlayerShips[x, y - 3] == CoordStatus.Ship))
                    result = ShotStatus.Wounded;
                PlayerShips[x,y] = CoordStatus.Got;
                UndiscoverCells--;
                if(UndiscoverCells == 0)
                {
                    result = ShotStatus.EndBattle; 
                }

            }


            return result;
        }
        //Метод выстрела компьютера(генерация выстрела)
        public string ShotGen()
        {
            
            int x, y; //координаты выстрела
            Random rand = new Random();
            if(LastShot == ShotStatus.Kill) WoundedStatus = false;
            if((LastShot == ShotStatus.Kill || LastShot == ShotStatus.Miss) && !WoundedStatus)
            {
                x = rand.Next(0, 9);
                y = rand.Next(0, 9);
            }
            else
            {

                x = int.Parse(LastShootCord.Substring(0, 1));
                y = int.Parse(LastShootCord.Substring(1));
                if (LastShot == ShotStatus.Wounded||FirstGot)
                {
                    bool FirstGot = true;  //переменная для определения первого выстрела
                    if (x != 9 && EnemyShips[x + 1, y] == CoordStatus.Got) {x = x - 1; FirstGot = false; }
                    if (y != 9 && EnemyShips[x, y + 1] == CoordStatus.Got) {y = y - 1; FirstGot = false; }
                    if (x != 0 && EnemyShips[x - 1, y] == CoordStatus.Got) {x = x + 1; FirstGot = false; }
                    if (y != 0 && EnemyShips[x, y - 1] == CoordStatus.Got) {y = y + 1; FirstGot = false; }

                    if (FirstGot) 
                    {
                        int q = rand.Next(1, 4);
                        switch (q)
                        {
                            case 1: x++; break;
                            case 2: x--; break;
                            case 3: y++; break;
                            case 4: y--; break;
                        }
                    }
                }
                if(LastShot == ShotStatus.Miss && WoundedStatus)
                {
                    if (x < 8 && EnemyShips[x + 2, y] == CoordStatus.Got) x = x + 3;
                    else
                    if (y < 8 && EnemyShips[x, y + 2] == CoordStatus.Got) y = y + 3;
                    else
                    if (x < 1 && EnemyShips[x - 2, y] == CoordStatus.Got) x = x - 3;
                    else
                    if (y < 1 && EnemyShips[x, y - 2] == CoordStatus.Got) y = y - 3;
                    else
                    if (x < 7 && EnemyShips[x + 3, y] == CoordStatus.Got) x = x + 4;
                    else
                    if (y < 7 && EnemyShips[x, y + 3] == CoordStatus.Got) y = y + 4;
                    else
                    if (x < 2 && EnemyShips[x - 3, y] == CoordStatus.Got) x = x - 4;
                    else
                    if (y < 2 && EnemyShips[x, y - 3] == CoordStatus.Got) y = y - 4;
                    else
                    if (x < 9 && EnemyShips[x + 1, y] == CoordStatus.Got) x = x + 2;
                    else
                    if (y < 9 && EnemyShips[x, y + 1] == CoordStatus.Got) y = y + 2;
                    else
                    if (x < 0 && EnemyShips[x - 1, y] == CoordStatus.Got) x = x - 2;
                    else
                    if (y < 0 && EnemyShips[x, y - 1] == CoordStatus.Got) y = y - 2;
                }
            }
            //if (EnemyShips[x, y] != CoordStatus.None) ShotGen();




            string result = x.ToString() + y.ToString();            
            return result;

        }
            



    }
}
