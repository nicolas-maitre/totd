using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOTD
{
    enum Direction {g,d,h,b,gh,gb,dh,db};
    enum Etat {Vivant,Blessé,Mort,BlesséUp,MortUP};
    class GameManager
    {
        Player player;

        public void GestionDirection()
        {

        }

        public void GestionAngle()
        {

        }

        public static int PositionXWindowToWorld(int PosWindow)
        {
            int PosWorld=0;
            return PosWorld;
        }
        public int PositionYWorldToWindow(int PosWorld)
        {
            int PosWindow = 0;
            return PosWindow;
        }
    }
}
