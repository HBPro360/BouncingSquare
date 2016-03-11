using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BouncingSquare
{
    public class Paddle
    {

        #region Private Members

        private Form _form = null;
        private PictureBox _box = null;
        private Random _rnd = null;


        #endregion

        #region Public Properties

        public Keys Key
        {
            set
            {
                if (value == Keys.Left)
                {
                    //Gets the current location of the Pabble
                    Point location = _box.Location;
                    //Modifying the local variable X
                    location.X -= 100;
                    //Testing if the Paddle should be moved
                    if (location.X >= 0) //"0" is the leftmost side of X
                    {
                        //Moves the Paddle
                        _box.Location = location;
                    }
                    else
                    {//Paddle stops at 0 and stays at the current location
                        location.X = 0; 
                        _box.Location = location;
                    }
                    
                }
                else if (value == Keys.Right)
                {
                    Point location = _box.Location; //current state of the paddle
                    location.X += 100; //speed in which the paddle moves right
                    if (location.X <= _form.Width - _box.Width - 10) //window - box - movement 
                        //testing if paddle should move
                    {
                        _box.Location = location; //Moves the paddle
                    }
                    else
                    {
                        location.X = _form.Width - _box.Width - 20; //Paddles stops at rightmost boundary of the form
                        _box.Location = location;
                    }
                }
            }
        }

        public PictureBox Box
        {
            get { return _box; }
        }

        public int Location
        {
            set //sets the X location of the Paddle
            {
                Point location = _box.Location;
                location.X = value;
                _box.Location = location;
            }
        }

        #endregion

        #region  Private Methods 



        #endregion

        #region  Public Methods 



        #endregion

        #region  Public Events 



        #endregion

        #region  Public Event Handlers 



        #endregion

        #region Construction 

        public Paddle(Form frm, Random rnd)
        {//gives properties of new paddle
            _box = new PictureBox(); //creates new box
            _form = frm;
            _rnd = rnd;
            Size size = new Size(100, 10);
            _box.Size = size; //defines the bounds of the new box
            int x = (_form.Width / 2) - (_box.Width/2); //determines where the new box is placed on the X axis
            int y = _form.Height - _box.Height * 5; //determines placement on Y axis - top is 0
            Point location = new Point(x, y);
            _box.Location = location;
            _box.BackColor = Color.Gold; //defines color of new box

            _form.Controls.Add(_box);
        }

        #endregion

    }
}
