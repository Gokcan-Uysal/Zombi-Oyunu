using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
namespace ilk_oyun
{
    internal class kursun
    {
        public string yon;
        public int kursunLeft;
        public int kursunTop;
        private int speed = 20;
        private PictureBox Kursun =new PictureBox();
        private Timer kursunTimer = new Timer();
        public void KursunYap(Form form)
        {
            Kursun.BackColor = Color.Magenta;
            Kursun.Size = new Size(5, 5);
            Kursun.Tag = "kursun";
            Kursun.Left = kursunLeft;
            Kursun.Top = kursunTop;
            Kursun.BringToFront();
            form.Controls.Add(Kursun);
            kursunTimer.Interval = speed;
            kursunTimer.Tick += new EventHandler(kursunTimerEvent);
            kursunTimer.Start();
        }
        private void kursunTimerEvent(object sender,EventArgs e)
        {
            if (yon == "left")
            {
                Kursun.Left -= speed;
            }
            if (yon == "rihgt")
            {
                Kursun.Left += speed;
            }
            if (yon == "up")
            {
                Kursun.Top -= speed;
            }
            if (yon == "down")
            {
                Kursun.Top += speed;
            }
            if(Kursun.Left < 10 || Kursun.Left > 860 || Kursun.Top < 10 || Kursun.Top > 600)
            {
                kursunTimer.Stop();
                kursunTimer.Dispose();
                Kursun.Dispose();
                Kursun = null;
            }
        }
    }
    
}
