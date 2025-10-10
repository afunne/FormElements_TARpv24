using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormElements
{
    public partial class Form1 : Form
    {
        TreeView tree;
        Button btn;
        Label lbl;
        PictureBox pic;
        CheckBox c_btn1, c_btn2;
        RadioButton r_btn1, r_btn2;
        TabControl tabC;
        TabPage tabP1, tabP2, tabP3;
        ListBox lb;
        bool t = true;
        public Form1()
        {
            
            this.Height = 600;
            this.Width = 800;
            this.Text = "Vorm elementidega";
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;
            TreeNode tn = new TreeNode("Elemendid");
            tn.Nodes.Add(new TreeNode("Nupp"));
            tn.Nodes.Add(new TreeNode("Silt"));
            tn.Nodes.Add(new TreeNode("Pilt"));
            tn.Nodes.Add(new TreeNode("Märkeruut"));
            tn.Nodes.Add(new TreeNode("Radionupp"));
            tn.Nodes.Add(new TreeNode("Tekstkast-TextBox"));
            tn.Nodes.Add(new TreeNode("Kaart"));
            tn.Nodes.Add(new TreeNode("MessageBox"));
            tn.Nodes.Add(new TreeNode("ListBox")); 
            tn.Nodes.Add(new TreeNode("DataGridView"));
            tn.Nodes.Add(new TreeNode("MainMenu"));
            //nupp
            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Location = new Point(150, 30);
            btn.Height = 30;
            btn.Width = 100;
            btn.Click += Btn_Click;
            //pealkiri
            lbl = new Label();
            lbl.Text = "Elementide loomine c# abil";
            lbl.Font = new Font("Arial", 24);
            lbl.Size = new Size(400, 30);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;

            pic = new PictureBox();
            pic.Size = new Size(50, 50);
            pic.Location = new Point(150, 60);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Image = Image.FromFile(@"..\..\Images\close_box_red.png");           
            pic.DoubleClick += Pic_DoubleClick;

            

            tree.Nodes.Add(tn);
            this.Controls.Add(tree);
        }
        int click = 0;
        private void Pic_DoubleClick(object sender, EventArgs e)
        {   //Double_Click -> carusel (3-4 images) 1-2-3-4-1-2-3-4-... 
            string[] images = { "esimene.jpg", "teine.jpg", "kolmas.jpg" };
            string fail = images[click];
            pic.Image = Image.FromFile(@"..\..\Images\" + fail);
            click++;
            if (click==3) {click = 0;}
        }

        private void Lbl_MouseLeave(object sender, EventArgs e)
        {
            lbl.BackColor = Color.Transparent;           
            Form1 Form = new Form1();
            Form.Show();
            this.Hide();
            

        }

        private void Lbl_MouseHover(object sender, EventArgs e)
        {
            lbl.BackColor = Color.FromArgb(200, 10, 20);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            using(var soundPlayer=new SoundPlayer(@"..\..\Sound\mixkit.wav")) { soundPlayer.Play(); }
            
            CustomForm customMessage = new CustomForm(
           "Minu oma teade",
           "Tee oma valik",
           "Kivi!",
           "Käärid!",
           "Paber!",
           "Vali ise!"
           );
            customMessage.StartPosition = FormStartPosition.CenterParent;
            customMessage.ShowDialog();
        }
        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text=="Nupp")
            {
                this.Controls.Add(btn);
            }
            else if (e.Node.Text== "Silt")
            {
                this.Controls.Add(lbl);
            }
            else if (e.Node.Text=="Pilt")
            {
                this.Controls.Add(pic);
            }
            else if (e.Node.Text == "Märkeruut")
            {
                c_btn1 = new CheckBox();
                c_btn1.Text = "Vali mind";
                c_btn1.Size = new Size(c_btn1.Text.Length * 9, 20);
                c_btn1.Location = new Point(310, 420);
                c_btn1.CheckedChanged += C_btn1_CheckedChanged;
                c_btn2 = new CheckBox();
                c_btn2.Size = new Size(100, 100);
                c_btn2.Image = Image.FromFile(@"..\..\Images\about.png");
                c_btn2.Location = new Point(310, 450);
                this.Controls.Add(c_btn1);
                this.Controls.Add(c_btn2);
            }
            else if (e.Node.Text == "Radionupp")
            {
                r_btn1 = new RadioButton();
                r_btn1.Text = "Must teema";
                r_btn1.Location = new Point(200, 420);
                r_btn2 = new RadioButton();
                r_btn2.Text = "Valge teema";
                r_btn2.Location = new Point(200, 440);
                this.Controls.Add(r_btn1);
                this.Controls.Add(r_btn2);
                r_btn1.CheckedChanged += new EventHandler(r_btn_Checked);
                r_btn2.CheckedChanged += new EventHandler(r_btn_Checked);
            }
            else if (e.Node.Text== "MessageBox")
            {
                

                MessageBox.Show("MessageBox", "Kõige lihtsam aken");
                var answer = MessageBox.Show("Tahad InputBoxi näha?", "Aken koos nupudega", MessageBoxButtons.YesNo);
                if (answer == DialogResult.Yes)
                {
                    string text = Interaction.InputBox("Sisesta siia mingi tekst", "InputBox", "Mingi tekst");
                    if (MessageBox.Show("Kas tahad tekst saada Tekskastisse?", "Teksti salvestamine", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {   lbl.Text = text;
                        Controls.Add(lbl);}
                    else
                    {   lbl.Text = "Siis mina lisan oma teksti!";
                        Controls.Add(lbl);}
                }
                else
                {
                    MessageBox.Show("Veel MessageBox", "Kõige lihtsam aken");
                }
            }
            else if (e.Node.Text=="Kaart")
            {
                tabC = new TabControl();
                tabC.Location = new Point(450, 50);
                tabC.Size = new Size(300, 200);
                tabP1 = new TabPage("TTHK");
                WebBrowser wb = new WebBrowser();
                wb.Url=new Uri("https://www.tthk.ee/");
                tabP1.Controls.Add(wb);
                tabP2 = new TabPage("Teine");
//Teeme ise!
                tabP3 = new TabPage("+");
                tabP3.DoubleClick += TabP3_DoubleClick;
                tabC.Controls.Add(tabP1);
                tabC.Controls.Add(tabP2);
                tabC.Controls.Add(tabP3);
                this.Controls.Add(tabC);
                tabC.Selected += TabC_Selected;
                tabC.DoubleClick += TabC_DoubleClick;
            }
            else if(e.Node.Text == "ListBox")
            {
                lb = new ListBox();
                lb.Items.Add("Roheline");
                lb.Items.Add("Punane");
                lb.Items.Add("Sinine");
                lb.Items.Add("Hall");
                lb.Items.Add("Kollane");
                lb.Location = new Point(150,120);
                lb.SelectedIndexChanged += new EventHandler(ls_SelectedIndexChanged);
                this.Controls.Add(lb);
            }
            else if (e.Node.Text == "DataGridView")
            {
                DataSet ds = new DataSet("XML fail. Menüü");
                ds.ReadXml(@"..\..\Images\menu.xml");
                DataGridView dg = new DataGridView();
                dg.Width = 400;
                dg.Height = 160;
                dg.Location = new Point(150,250);
                dg.AutoGenerateColumns = true;
                dg.DataSource = ds;
                dg.DataMember = "food";
                this.Controls.Add(dg);
            }
            else if (e.Node.Text == "MainMenu")
            {
                MainMenu menu = new MainMenu();
                MenuItem menuFile = new MenuItem("File");
                menuFile.MenuItems.Add("Exit", new EventHandler(menuFile_Exit_Select));
                menu.MenuItems.Add(menuFile);
                this.Menu = menu;
                //
            }
        }

        private void menuFile_Exit_Select(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ls_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lb.SelectedItem.ToString())
            {
                case ("Sinine"): tree.BackColor = Color.Blue; break;
                case ("Kollane"): tree.BackColor = Color.Yellow; break;
                case ("Punane"): tree.BackColor = Color.Red; break;
                case ("Hall"): tree.BackColor = Color.Gray; break;
                case ("Roheline"): tree.BackColor = Color.Green; break;
            }
        }

        private void TabC_Selected(object sender, TabControlEventArgs e)
        {
            //this.tabC.TabPages.Clear();
            this.tabC.TabPages.Remove(tabC.SelectedTab);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void TabC_DoubleClick(object sender, EventArgs e)
        {
            string title = "tabP" + (tabC.TabCount - 1).ToString();
            TabPage tb = new TabPage(title);
            
            tabC.TabPages.Add(tb);
        }

        private void TabP3_DoubleClick(object sender, EventArgs e)
        {
            string title = "tabP" + (tabC.TabCount + 1).ToString();
            TabPage tb = new TabPage(title);
            tabC.TabPages.Add(tb);
        }

        private void r_btn_Checked(object sender, EventArgs e)
        {
            if (r_btn1.Checked)
            {   this.BackColor = Color.Black;
                r_btn2.ForeColor = Color.White;
                r_btn1.ForeColor = Color.White;}
            else if (r_btn2.Checked)
            {   this.BackColor = Color.White;
                r_btn2.ForeColor = Color.Black;
                r_btn1.ForeColor = Color.Black;}
        }
        private void C_btn1_CheckedChanged(object sender, EventArgs e)
        {           
            if (t)
            {
                this.Size = new Size(1000, 1000);
                pic.BorderStyle = BorderStyle.Fixed3D;
                c_btn1.Text = "Teeme väiksem";
                c_btn1.Font = new Font("Arial", 36, FontStyle.Bold);
                t = false;}
            else
            {
                this.Size = new Size(700, 500);
                c_btn1.Text = "Suurendame";
                c_btn1.Font = new Font("Arial", 36, FontStyle.Regular);
                t = true;}            
        }
    }
}
