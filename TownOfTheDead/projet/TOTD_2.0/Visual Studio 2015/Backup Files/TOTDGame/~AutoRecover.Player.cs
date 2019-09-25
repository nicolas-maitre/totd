using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOTDGame;

namespace TOTD
{
    class Player
    {
        //Propriétés
        private int positionX;
        private int positionY;
        private int direction;
        private int pointsDeVie;
        private int attaque;
        private Etat etat;
        private int coolDownSoin;
        private int coolDownTir;
        private int angle;
        //Methodes
        public void Déplacer(Direction xdirection)
        {
            if (etat != Etat.Mort || etat != Etat.MortUP)
            {
                
            }
        }

        public void Tirer()
        {
            
        }

        public void Tourner(Direction xdirection)
        {
            switch (xdirection)
            {
                case Direction.h:
                    angle = 0;
                    break;
                case Direction.dh:
                    angle = 45;
                    break;
                case Direction.d:
                    angle = 90;
                    break;
                case Direction.db:
                    angle = 135;
                    break;
                case Direction.b:
                    angle = 180;
                    break;
                case Direction.gb:
                    angle = 225;
                    break;
                case Direction.g:
                    angle = 270;
                    break;
                case Direction.gh:
                    angle = 315;
                    break;
            }
        }

        public void Acheter()
        {

        }

        public void Soigner()
        {

        }

        public void Touché()
        {

        }
        
    }

}
