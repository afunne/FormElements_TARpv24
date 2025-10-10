using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormElements
{
    public class CustomForm : System.Windows.Forms.Form
    {
        Label message = new Label();
        Button[] btn = new Button[4];       
        string[] texts = new string[4];
        public CustomForm()
        {

        }
        public CustomForm(string title, string body, string button1, string button2,string button3, string button4)
        {
            texts[0] = button1;
            texts[1] = button2;
            texts[2] = button3;
            texts[3] = button4;
            this.ClientSize = new System.Drawing.Size(490, 150);
            this.Text = title;
            int y=111;
            for (int i = 0; i < 4; i++)
            {
                btn[i] = new Button
                { 
                    Location = new System.Drawing.Point(y, 112),
                    Size = new System.Drawing.Size(75, 23),
                    Text = texts[i],
                    BackColor = Control.DefaultBackColor
                };
                btn[i].Click += CustomForm_Click;
                this.Controls.Add(btn[i]);
                y =y +100;
            }
            message.Location = new System.Drawing.Point(10, 10);
            message.Text = body;
            message.Font = Control.DefaultFont;
            message.AutoSize = true;
            this.BackColor = Color.White;
            this.ShowIcon = false;
            this.Controls.Add(message);

        }
        private void CustomForm_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            MessageBox.Show("Oli valitud " + btn.Text);
        }        
    }
}
