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
        CheckBox c_btn1, c_btn2, c_btn3, c_btn4;
        RadioButton r_btn1, r_btn2;
        TabControl tabC;
        TabPage tabP1, tabP2, tabP3;
        ListBox lb;
        bool t = true;
        bool soundEnabled = true;
        int click = 0;

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

            // Nupp
            btn = new Button();
            btn.Text = "Vajuta siia";
            btn.Location = new Point(150, 30);
            btn.Height = 30;
            btn.Width = 100;
            btn.Click += Btn_Click;

            // Silt
            lbl = new Label();
            lbl.Text = "Elementide loomine c# abil";
            lbl.Font = new Font("Arial", 24);
            lbl.Size = new Size(400, 30);
            lbl.Location = new Point(150, 0);
            lbl.MouseHover += Lbl_MouseHover;
            lbl.MouseLeave += Lbl_MouseLeave;

            // Pilt
            pic = new PictureBox();
            pic.Size = new Size(100, 100);
            pic.Location = new Point(150, 60);
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.Image = Image.FromFile(@"..\..\Images\close_box_red.png");
            pic.DoubleClick += Pic_DoubleClick;

            tree.Nodes.Add(tn);
            this.Controls.Add(tree);
        }

        private void Pic_DoubleClick(object sender, EventArgs e)
        {
            string[] images = { "esimene.jpg", "teine.jpg", "kolmas.jpg" };
            Random rnd = new Random();
            string fail = images[rnd.Next(images.Length)];
            pic.Image = Image.FromFile(@"..\..\Images\" + fail);
        }

        private void Lbl_MouseLeave(object sender, EventArgs e)
        {
            lbl.BackColor = Color.Transparent;
        }

        private void Lbl_MouseHover(object sender, EventArgs e)
        {
            lbl.BackColor = Color.FromArgb(200, 10, 20);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            if (soundEnabled)
            {
                using (var soundPlayer = new SoundPlayer(@"..\..\Sound\mixkit.wav"))
                {
                    soundPlayer.Play();
                }
            }

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
            if (e.Node.Text == "Nupp")
            {
                ToggleControl(btn);
            }
            else if (e.Node.Text == "Silt")
            {
                ToggleControl(lbl);
            }
            else if (e.Node.Text == "Pilt")
            {
                ToggleControl(pic);
            }
            else if (e.Node.Text == "Märkeruut")
            {
                ToggleCheckBoxes();
            }
            else if (e.Node.Text == "Radionupp")
            {
                ToggleRadioButtons();
            }
            else if (e.Node.Text == "MessageBox")
            {
                ShowCustomMessageBox();
            }
            else if (e.Node.Text == "Kaart")
            {
                ToggleTabs();
            }
            else if (e.Node.Text == "ListBox")
            {
                ToggleListBox();
            }
            else if (e.Node.Text == "DataGridView")
            {
                ToggleDataGrid();
            }
            else if (e.Node.Text == "MainMenu")
            {
                ToggleMenu();
            }
        }

        private void ToggleControl(Control ctrl)
        {
            if (Controls.Contains(ctrl))
                Controls.Remove(ctrl);
            else
            {
                Controls.Add(ctrl);
                ctrl.BringToFront();
            }
            Refresh();
        }

        private void ToggleCheckBoxes()
        {
            if (c_btn1 != null && Controls.Contains(c_btn1))
            {
                Controls.Remove(c_btn1);
                Controls.Remove(c_btn2);
                Controls.Remove(c_btn3);
                Controls.Remove(c_btn4);
                Refresh();
            }
            else
            {
                c_btn1 = new CheckBox() { Text = "Suurenda vormi", Location = new Point(310, 420) };
                c_btn1.CheckedChanged += C_btn1_CheckedChanged;

                c_btn2 = new CheckBox() { Text = "Näita pilti", Location = new Point(310, 450), Checked = true };
                c_btn2.CheckedChanged += (s, e) => pic.Visible = c_btn2.Checked;

                c_btn3 = new CheckBox() { Text = "Näita silti", Location = new Point(310, 480), Checked = true };
                c_btn3.CheckedChanged += (s, e) => lbl.Visible = c_btn3.Checked;

                c_btn4 = new CheckBox() { Text = "Luba heli", Location = new Point(310, 510), Checked = true };
                c_btn4.CheckedChanged += (s, e) => soundEnabled = c_btn4.Checked;

                this.Controls.AddRange(new Control[] { c_btn1, c_btn2, c_btn3, c_btn4 });
            }
        }

        private void ToggleRadioButtons()
        {
            if (r_btn1 != null && Controls.Contains(r_btn1))
            {
                Controls.Remove(r_btn1);
                Controls.Remove(r_btn2);
                Refresh();
            }
            else
            {
                r_btn1 = new RadioButton() { Text = "Tume teema", Location = new Point(200, 420) };
                r_btn2 = new RadioButton() { Text = "Hele teema", Location = new Point(200, 440) };
                r_btn1.CheckedChanged += r_btn_Checked;
                r_btn2.CheckedChanged += r_btn_Checked;
                this.Controls.Add(r_btn1);
                this.Controls.Add(r_btn2);
            }
        }

        private void ToggleListBox()
        {
            if (lb != null && Controls.Contains(lb))
            {
                Controls.Remove(lb);
                Refresh();
            }
            else
            {
                lb = new ListBox();
                lb.Items.AddRange(new string[] { "Roheline", "Punane", "Sinine", "Hall", "Kollane" });
                lb.Location = new Point(150, 120);
                lb.SelectedIndexChanged += ls_SelectedIndexChanged;
                this.Controls.Add(lb);
            }
        }

        private void ToggleTabs()
        {
            if (tabC != null && Controls.Contains(tabC))
            {
                Controls.Remove(tabC);
                Refresh();
            }
            else
            {
                tabC = new TabControl();
                tabC.Location = new Point(450, 50);
                tabC.Size = new Size(300, 200);

                tabP1 = new TabPage("TTHK");
                WebBrowser wb = new WebBrowser();
                wb.Url = new Uri("https://www.tthk.ee/");
                tabP1.Controls.Add(wb);

                tabP2 = new TabPage("Teine");
                Label info = new Label();
                info.Text = "See on teine kaart.";
                info.Location = new Point(20, 20);
                PictureBox tabPic = new PictureBox();
                tabPic.Image = Image.FromFile(@"..\..\Images\about.png");
                tabPic.Size = new Size(100, 100);
                tabPic.Location = new Point(20, 60);
                tabPic.SizeMode = PictureBoxSizeMode.StretchImage;
                tabP2.Controls.Add(info);
                tabP2.Controls.Add(tabPic);

                tabP3 = new TabPage("+");
                tabP3.DoubleClick += TabP3_DoubleClick;

                tabC.Controls.Add(tabP1);
                tabC.Controls.Add(tabP2);
                tabC.Controls.Add(tabP3);
                tabC.Selected += TabC_Selected;
                tabC.DoubleClick += TabC_DoubleClick;

                this.Controls.Add(tabC);
            }
        }

        private void ToggleDataGrid()
        {
            Control existingGrid = Controls.OfType<DataGridView>().FirstOrDefault();
            if (existingGrid != null)
            {
                Controls.Remove(existingGrid);
                Refresh();
            }
            else
            {
                DataSet ds = new DataSet("XML fail. Menüü");
                ds.ReadXml(@"..\..\Images\menu.xml");
                DataGridView dg = new DataGridView();
                dg.Width = 400; dg.Height = 160;
                dg.Location = new Point(150, 250);
                dg.AutoGenerateColumns = true;
                dg.DataSource = ds; dg.DataMember = "food";
                this.Controls.Add(dg);
            }
        }

        private void ToggleMenu()
        {
            if (this.Menu != null)
            {
                this.Menu = null;
            }
            else
            {
                MainMenu menu = new MainMenu();
                MenuItem menuFile = new MenuItem("File");
                menuFile.MenuItems.Add("Exit", new EventHandler(menuFile_Exit_Select));
                menuFile.MenuItems.Add("Restart", new EventHandler(menuFile_Restart_Select));
                menu.MenuItems.Add(menuFile);

                MenuItem menuHelp = new MenuItem("Help");
                menuHelp.MenuItems.Add("About", new EventHandler(menuHelp_About_Select));
                menu.MenuItems.Add(menuHelp);

                this.Menu = menu;
            }
        }

        private void menuFile_Exit_Select(object sender, EventArgs e)
        {
            this.Close();
        }

        private void menuFile_Restart_Select(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void menuHelp_About_Select(object sender, EventArgs e)
        {
            MessageBox.Show("See on näidisvorm, mille autor on Hussein.", "About");
        }

        private void ls_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (lb.SelectedItem.ToString())
            {
                case "Sinine": tree.BackColor = Color.Blue; break;
                case "Kollane": tree.BackColor = Color.Yellow; break;
                case "Punane": tree.BackColor = Color.Red; break;
                case "Hall": tree.BackColor = Color.Gray; break;
                case "Roheline": tree.BackColor = Color.Green; break;
            }
        }

        private void TabC_Selected(object sender, TabControlEventArgs e)
        {
            tabC.TabPages.Remove(tabC.SelectedTab);
        }

        private void TabC_DoubleClick(object sender, EventArgs e)
        {
            string title = "tabP" + (tabC.TabCount + 1).ToString();
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
            {
                this.BackColor = Color.Black;
                r_btn1.ForeColor = Color.White;
                r_btn2.ForeColor = Color.White;
            }
            else if (r_btn2.Checked)
            {
                this.BackColor = Color.White;
                r_btn1.ForeColor = Color.Black;
                r_btn2.ForeColor = Color.Black;
            }
        }

        private void C_btn1_CheckedChanged(object sender, EventArgs e)
        {
            if (t)
            {
                this.Size = new Size(1000, 1000);
                pic.BorderStyle = BorderStyle.Fixed3D;
                c_btn1.Text = "Tee väiksemaks";
                t = false;
            }
            else
            {
                this.Size = new Size(800, 600);
                c_btn1.Text = "Suurenda vormi";
                t = true;
            }
        }

        private void ShowCustomMessageBox()
        {
            MessageBox.Show("MessageBox", "Kõige lihtsam aken");
            var answer = MessageBox.Show("Tahad InputBoxi näha?", "Aken koos nupudega", MessageBoxButtons.YesNo);
            if (answer == DialogResult.Yes)
            {
                string text = Interaction.InputBox("Sisesta siia mingi tekst", "InputBox", "Mingi tekst");
                if (MessageBox.Show("Kas tahad tekst saada Tekskastisse?", "Teksti salvestamine", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    lbl.Text = text; if (!Controls.Contains(lbl)) Controls.Add(lbl);
                }
                else
                {
                    lbl.Text = "Siis mina lisan oma teksti!";
                    if (!Controls.Contains(lbl)) Controls.Add(lbl);
                }
            }
            else
            {
                MessageBox.Show("Veel MessageBox", "Kõige lihtsam aken");
            }
        }
    }
}
