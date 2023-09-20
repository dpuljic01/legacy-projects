using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Whack_a_Mouse
{
    public class Sensing
    {
        public Point Mouse;
        public bool MouseDown;
        private string key;

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public Sensing()
        {
            MouseDown = false;
            Mouse = new Point(0, 0);
        }
        
        //preopterećena metoda
        public bool KeyPressed(Keys key)
        {
            if (Key == key.ToString())
            {
                return true;
            }
            else
                return false;
        }
        public bool KeyPressed(string key)
        {
            if (Key == key)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    } 
}
