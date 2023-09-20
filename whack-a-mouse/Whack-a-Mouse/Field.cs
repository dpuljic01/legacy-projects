using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Whack_a_Mouse
{
    public abstract class Field
    {
        #region properties

        public bool Show;
        public Bitmap CurrentItem;

        private int x, y, width, height;

        public int X
        {
            get { return x; }
            set
            {
                if (value <= GameOptions.LeftEdge)
                    this.x = GameOptions.LeftEdge;
                else if (value >= GameOptions.RightEdge)
                    this.x = GameOptions.LeftEdge;
                else
                    this.x = value;
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                if (value < GameOptions.UpEdge)
                    y = GameOptions.UpEdge;
                else if (value >= GameOptions.DownEdge)
                    y = GameOptions.UpEdge;
                else
                    this.y = value;
            }
        }

        public int Width
        {
            get { return width; }
            set
            {
                if (value <= 0)
                    width = GameOptions.ItemWidth;
                else
                    width = value;
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                if (value <= 0)
                    height = GameOptions.ItemHeight;
                else
                    height = value;
            }
        }

        #endregion

        public Field(string itemImage, int posX, int posY)
        {
            CurrentItem = new Bitmap(itemImage);
            X = posX;
            Y = posY;
            Width = CurrentItem.Width;
            Height = CurrentItem.Height;
            Show = true;
        }

        #region Methods

        public virtual void SetX(int posX)//primjer implementirane metode unutar apstraktne klase, može i bez "virtual"
        {
            this.X = posX;
        }
        public virtual void SetY(int posY)
        {
            this.Y = posY;
        }

        public virtual void GotoMousePoint(Point mouse)
        {
            X = mouse.X - Width / 2;
            Y = mouse.Y - Height / 2;
        }

        //primjer metode bez implementacije..ona se vrši u izvedenoj klasi pomoću override
        public abstract void SetSize(int size);

        #endregion
    }
    public class Hole : Field
    {
        public Hole(string itemImage, int posX, int posY)
            :base(itemImage,posX,posY)
        { }

        public override void SetSize(int size)//override metode SetSize
        {
            float w = float.Parse(this.Width.ToString());
            float h = float.Parse(this.Height.ToString());
            float nw = ((w / 100) * size);
            float nh = ((h / 100) * size);

            this.Width = Convert.ToInt32(nw);
            this.Height = Convert.ToInt32(nh);
        }
    }
    public class Mouse : Field
    {
        public Mouse(string itemImage, int posX, int posY)
            : base(itemImage, posX, posY)
        { }

        #region Methods

        public override void SetSize(int size)
        {
            float w = float.Parse(this.Width.ToString());
            float h = float.Parse(this.Height.ToString());
            float nw = ((w / 100) * size);
            float nh = ((h / 100) * size);

            this.Width = Convert.ToInt32(nw);
            this.Height = Convert.ToInt32(nh);
        }

        public void MoveUp(int steps)
        {
            this.Y -= steps;
        }
        //public void MoveDown(int steps)
        //{
        //    this.Y += steps;
        //}

        public bool TouchingItem(Field b)
        {
            Field a = this;

            Rectangle A = new Rectangle(a.X, a.Y, a.Width, a.Height);
            Rectangle B = new Rectangle(b.X, b.Y, b.Width, b.Height);

            if (A.IntersectsWith(B))
                return true;
            else
                return false;
        }
        #endregion

    }
    public class Slipper : Mouse
    {
        public Slipper(string itemImage, int posX, int posY)
            : base(itemImage, posX, posY)
        { }
    }
}
