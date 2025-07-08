using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        class ChangeListType
        {
            public int changeRow;
            public int changeCol;
        }
        const int numOfCells = 400;
        Color deadColor = Color.DarkGray, aliveColor = Color.Yellow;
        List<(int, int)> changeList = new List<(int, int)>();
        List<Label> labelList = new List<Label>();
        int numColsRows;
        int rcRow, rcCol;

        private void Form1_Load(object sender, EventArgs e)
        {
            numColsRows = (int)Math.Sqrt(numOfCells);
            for (int row = 0; row < numColsRows; row++)
            {
                for (int col = 0; col < numColsRows; col++)
                {
                    Label lblLife = new Label();
                    lblLife.BackColor = deadColor;
                    lblLife.Size = new Size(20, 20);
                    lblLife.AutoSize = false;
                    lblLife.Name = "lbllife-" + row + "-" + col;
                    lblLife.Margin = new Padding(2);
                    lblLife.Font = new Font("Arial", 5);
                    lblLife.Text = row + "-" + col;
                    lblLife.Click += LblLife_Click;
                    FlowLayoutPanel1.Controls.Add(lblLife);
                    labelList.Add(lblLife);
                    labelList[Getindex(row, col)].BackColor = deadColor;
                }
            }

        }

        private void LblLife_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            int col, row;
            string[] splitStr = (sender as Label).Name.Split('-');
            row = int.Parse(splitStr[1]);
            col = int.Parse(splitStr[2]);
            if (me.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (labelList[Getindex(row, col)].BackColor == deadColor)
                {
                    labelList[Getindex(row, col)].BackColor = aliveColor;
                }
                else
                {
                    labelList[Getindex(row, col)].BackColor = deadColor;
                }
            }
            else //right click
            {

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            changeList.Clear();
            int neighbourIndex;
            numColsRows = (int)Math.Sqrt(numOfCells);
            for (int row = 0; row < numColsRows; row++)
            {
                for (int col = 0; col < numColsRows; col++)
                {
                   int aliveNeighbors = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i == 0 && j == 0)
                            {
                                continue;
                            }
                            int x = row + i;
                            int y = col + j;
                            if (x > 0 && x < numColsRows && y > 0 && y < numColsRows)
                            {
                                neighbourIndex = Getindex(x, y);
                                if (labelList[neighbourIndex].BackColor == aliveColor)
                                {
                                    aliveNeighbors++;
                                }
                            }
                            
                        }
                    } 
                    int currentIndex = Getindex(row, col);
                    bool alive = labelList[currentIndex].BackColor == aliveColor;
                    if ((alive && (aliveNeighbors < 2 || aliveNeighbors > 3)) || (!alive && aliveNeighbors == 3))
                    {
                        changeList.Add((row, col));
                    }
                }

            }
            foreach ((int row, int col) in changeList)
            {
                int index = Getindex(row, col);
                if (labelList[index].BackColor == aliveColor)
                {
                    labelList[index].BackColor = deadColor;
                }
                else
                {
                    labelList[index].BackColor = aliveColor;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private int Getindex(int row, int col)
        {
            return row * (int)Math.Sqrt(numOfCells) + col;
        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
