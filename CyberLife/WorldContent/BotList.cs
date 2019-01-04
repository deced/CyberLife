using CyberLife.Simple2DWorld;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberLife.WorldContent
{
    public class BotList
    {
        List<Point> _botPoints;
        List<BotLifeForm> _bots;

        public List<Point> BotPoints { get => _botPoints; set => _botPoints = value; }
        public List<BotLifeForm> Bots { get => _bots; set => _bots = value; }
        public int Count { get { return BotPoints.Count; } }
        public BotLifeForm this[Point point] 
        {
            
            get { try { return Bots[BotPoints.IndexOf(point)]; } catch { return null; } }
        }


        public void Add(BotLifeForm bot)
        {
            BotPoints.Add(bot.Point);
            Bots.Add(bot);
        }
        public void Remove(Point point)
        {
            int i = BotPoints.IndexOf(point);
            Bots.Remove(Bots[BotPoints.IndexOf(point)]);
            BotPoints.Remove(point);
        }



        /// <summary>
        /// Определяет,свободна ли выбранная клетка,и если нет,то возвращает форму жизни в данной клетке
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="botOnPlace"></param>
        /// <returns></returns>
        public bool IsPlaceEmpty(int x,int y,out BotLifeForm botOnPlace)
        {
            Point point = new Point(x, y);
            if (BotPoints.Contains(point))
            {
                botOnPlace = Bots[BotPoints.IndexOf(point)];
                return true;
            }
            else
            {
                botOnPlace = null;
                return false;
            }
        }



        /// <summary>
        /// Определяет,есть ли вокруг данной клетки свободное пространство,возвращает bool и передаёт координаты клетки в ref параметрах,
        /// в противном случае возвращает false и исходные координаты центральной клетки
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public bool IsAroundEmpty(ref int X, ref int Y,MapSize Size)
        {
            BotLifeForm bot = null;
            int workX = X;
            int workY = Y;
            workY++;
            workX--;
            for (int i = 1; i < 4; i++)
            {
                if (workY > Size.Height - 1)
                    workY = Size.Height - 1;
                if (workY < 0)
                    workY = 0;
                if (workX > Size.Width - 1)
                    workX = 0;
                if (workX < 0)
                    workX = Size.Width - 1;
                if (IsPlaceEmpty(workX, workY, out bot))
                {
                    X = workX;
                    Y = workY;
                    return true;
                }
                workY--;
            }
            workX++;
            workY--;
            for (int i = 1; i < 4; i++)
            {
                workY++;
                if (i == 2)
                {
                    continue;
                }
                if (workY > Size.Height - 1)
                    workY = Size.Height - 1;
                if (workY < 0)
                    workY = 0;
                if (workX > Size.Width - 1)
                    workX = 0;
                if (workX < 0)
                    workX = Size.Width - 1;
                if (IsPlaceEmpty(workX, workY, out bot))
                {
                    X = workX;
                    Y = workY;
                    return true;
                }
            }
            workX++;
            for (int i = 1; i < 4; i++)
            {
                if (workY > Size.Height - 1)
                    workY = Size.Height - 1;
                if (workY < 0)
                    workY = 0;
                if (workX > Size.Width - 1)
                    workX = 0;
                if (workX < 0)
                    workX = Size.Width - 1;
                if (IsPlaceEmpty(workX, workY, out bot))
                {
                    X = workX;
                    Y = workY;
                    return true;
                }
                workY--;
            }
            return false;
        }



        public IEnumerator GetEnumerator()
        {
           return Bots.GetEnumerator();
        }

        public IEnumerable<BotLifeForm> GetEnumerable()
        {
            foreach(BotLifeForm bot in Bots)
            {
                yield return bot;
            }
        }

        public List<BotLifeForm> ToList()
        {
           Bots= Bots.ToList();
          BotPoints=  BotPoints.ToList();
            return Bots.ToList();
        }

        internal IEnumerable<object> DeadBots(Func<object, bool> p)
        {
            return Bots.Where(p);
        }


        public bool ContainsPoint(Point point)
        {
            if (BotPoints.Contains(point))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public BotList()
        {
            BotPoints = new List<Point> { };
            Bots = new List<BotLifeForm> { };
        }

        public BotList(List<BotLifeForm> bots)
        {
            foreach(BotLifeForm bot in bots)
            {
                Add(bot);
            }
        }
    }
}
