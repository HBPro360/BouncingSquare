using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BouncingSquare
{
    public partial class frmBouncingSquare : Form
    {
        Random rnd = new Random();
        Paddle paddle = null;

        List<Square> squares = new List<Square>();

        public frmBouncingSquare()
        {
            InitializeComponent();
            
            this.Load += FrmBouncingSquare_Load;
            this.KeyDown += FrmBouncingSquare_KeyDown;
            this.MouseMove += FrmBouncingSquare_MouseMove;
            Cursor.Hide();
          

        }

       

        private void FrmBouncingSquare_Load(object sender, EventArgs e)
        {
            paddle = new Paddle(this, rnd);
        }


        private void FrmBouncingSquare_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Application.Exit();
            }
            else if (e.KeyData == Keys.End)
            {
                if (squares.Count < 10)
                {
                    Square square = new Square(this, rnd, paddle);
                    squares.Add(square);
                    square.score += Square_score;
                }
            }
            #region experiment
            else if (e.KeyData == Keys.N)
            {
                Square square = new Square(this, rnd, paddle);

            }
            #endregion
            else if (e.KeyData == Keys.Left || e.KeyData == Keys.Right)
            {
                paddle.Key = e.KeyData;
            }
        }

        private void Square_score(object sender, ScoreEventArgs e)
        {
            int score = Convert.ToInt32(lblScore.Text);
            score += e.Points;
            lblScore.Text = score.ToString();

            if(e.Points < 0)
            {
                Square sq = (Square)sender;
                Guid id = sq.Id;
                //Guid id = ((Square)sender).Id;   //or this instead of previous 2 lines

                for (int i = 0; i < squares.Count; i++)
                {
                    if (squares[i].Id == id)
                    {
                        squares.RemoveAt(i);
                        break;
                    }
                }
            }
            
        }

        private void FrmBouncingSquare_MouseMove(object sender, MouseEventArgs e)
        {
            paddle.Location = e.Location.X;
        }

    }

}

