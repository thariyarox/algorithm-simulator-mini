using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        double [] dataArray;
        Label[] myLabels;
        int length;
        int timer1count = 0;
        int timer2count = 0;
        int j = 1;
        int currentIndex = 1;       
        int upElementInitialX;
        int upElementInitialY;
        int downElementInitialX;
        int downElementInitialY;
        int movingLength = 0;



        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myLabels = new Label[] { label1, label2, label3, label4, label5, label6, label7, label8 };
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            this.lbStatus.Text = "Sorting";
            length = 8;            
            dataArray = new double[length];
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {
                dataArray[i] = rnd.Next(0, 100);             
                myLabels[i].Text = dataArray[i].ToString();
                myLabels[i].BackColor = Color.White;
            }
           

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timer1count == 0) //initially in a round
            {
                if (currentIndex == 0)
                {
                    j++;
                    if (j == length)//sorting complete. Terminate program
                    {
                        this.lbStatus.Text = "Sorting Complete";
                        myLabels[currentIndex].BackColor = Color.White;

                        for (int i = 0; i < length; i++)
                        {

                            myLabels[i].BackColor = Color.LightGreen;
                        }
                        timer1.Stop();
                    }
                    else
                    {
                        currentIndex = j;
                    }
                }

                if (currentIndex > 0)
                {
                    myLabels[currentIndex].BackColor = Color.Yellow;
                    myLabels[currentIndex - 1].BackColor = Color.Yellow;
                }
            }
            else if (timer1count == 3)
            {
                if (dataArray[currentIndex] < dataArray[currentIndex - 1])
                {
                    myLabels[currentIndex].BackColor = Color.Red;
                    //now we have to swap these two elements
                    timer2count = 0;
                    timer2.Start();
                    timer1.Stop();
                }
                else
                {
                    //myLabels[currentIndex - 1].BackColor = Color.Red;
                    //self call
                    myLabels[currentIndex].BackColor = Color.White;
                    myLabels[currentIndex - 1].BackColor = Color.White;
                    timer1count = -1;
                    currentIndex = 0;
                }
            }



            timer1count++;
        }

        //This timer will swap two elements
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer2count == 0)
            {
                upElementInitialX = myLabels[currentIndex-1].Location.X;
                upElementInitialY = myLabels[currentIndex - 1].Location.Y;
                downElementInitialX = myLabels[currentIndex].Location.X;
                downElementInitialY = myLabels[currentIndex].Location.Y;

                movingLength = 50 + (downElementInitialY - upElementInitialY) + 50;
            }
            if (timer2count < 50) //move upelement to right
            {
                
                myLabels[currentIndex-1].Location = new Point(myLabels[currentIndex-1].Location.X+1,myLabels[currentIndex-1].Location.Y);
            }
            else if (timer2count < (50+ downElementInitialY - upElementInitialY))
            {
                myLabels[currentIndex - 1].Location = new Point(myLabels[currentIndex - 1].Location.X, myLabels[currentIndex - 1].Location.Y + 1);
                myLabels[currentIndex].Location = new Point(myLabels[currentIndex].Location.X, myLabels[currentIndex].Location.Y - 1);
            }
            else if (timer2count < movingLength)
            {
                myLabels[currentIndex - 1].Location = new Point(myLabels[currentIndex - 1].Location.X - 1, myLabels[currentIndex - 1].Location.Y);
            }
            else if (timer2count == movingLength + 2)
            {
                //-------------------------------------------------
                Label temp = myLabels[currentIndex];                
                myLabels[currentIndex] = myLabels[currentIndex - 1];
                myLabels[currentIndex - 1] = temp;
                //-------------------------------------------------
                double tempdata = dataArray[currentIndex];
                dataArray[currentIndex] = dataArray[currentIndex - 1];
                dataArray[currentIndex - 1] = tempdata;

                //-------------------------------------------------
                myLabels[currentIndex].BackColor = Color.White;
                myLabels[currentIndex - 1].BackColor = Color.White;
                //call timer 1
                timer1count = 0;
                currentIndex--;
                timer1.Start();
                timer2.Stop();
            }




            timer2count++;
        }
    }
}
