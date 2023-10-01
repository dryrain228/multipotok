using System.Threading;
namespace multipotok
{
    public partial class Form1 : Form
    {
        private Thread thread1;
        private Thread thread2;
        private Thread thread3;

        public Form1()
        {
            InitializeComponent();
        }

        public void button1_Click(object sender, EventArgs e)
        {
            thread1 = new Thread(Work1);
            thread2 = new Thread(Work2);
            thread3 = new Thread(Work3);

            thread1.Start();
            thread2.Start();
            thread3.Start();
        }
        private void Work1()
        {
            int stp;
            int newval;
            Random rnd = new Random();

            for (int i = 0; i < 100; i++)
            {
                stp = this.progressBar1.Step * rnd.Next(-1, 2);
                newval = this.progressBar1.Value + stp;
                if (newval > this.progressBar1.Maximum)
                    newval = this.progressBar1.Maximum;
                else if (newval < this.progressBar1.Minimum)
                    newval = this.progressBar1.Minimum;


                this.Invoke((MethodInvoker)delegate
                {
                    this.progressBar1.Value = newval;
                });

                Thread.Sleep(100);
            }
        }

        private void Work2()
        {
            Random rnd = new Random();
            using (Graphics g = this.CreateGraphics())
            {
                for (int i = 0; i < 100; i++)
                {
                    int x = rnd.Next(0, this.ClientSize.Width - 50);
                    int y = rnd.Next(0, this.ClientSize.Height - 50);
                    int size = rnd.Next(20, 50);
                    Color color = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));


                    using (Pen pen = new Pen(color))
                    {
                        g.DrawRectangle(pen, x, y, size, size);
                    }

                    Thread.Sleep(100);
                }
            }
        }
        private void Work3()
        {
            // Код для потока 3, который пишет текст в TextBox1
            for (int i = 1; i < 100; i++)
            {
                string message = $"Поток 3: сообщение {i}";
                UpdateTextbox(message);

                Thread.Sleep(1000);
            }
        }
        private void UpdateTextbox(string message)
        {
            if (textBox1.InvokeRequired)
            {
                
                Action<string> update = new Action<string>(UpdateTextbox);
                textBox1.Invoke(update, new object[] { message });
            }
            else
            {
                
                textBox1.AppendText(message + Environment.NewLine);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*Thread trd = new Thread(new ThreadStart(this.Work1));
            trd.IsBackground = true;
            trd.Start();*/
        }


    }
}