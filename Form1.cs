using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using System.Diagnostics;

namespace FormElements
{
    public partial class Form1 : Form
    {
        // Controls
        private Button btn;
        private Button saveMainBtn;
        private Label lbl;
        private PictureBox pic;
        private ListBox lb;
        private Button btnCustomColor;
        private CheckBox c_btn1, c_btn2, c_btn3, c_btn4, c_btn5;
        private RadioButton r_btn1, r_btn2, r_btn3;
        private TabControl tabC;
        private TabPage tabP1, tabP2, tabP3;

        // Bounce variables
        private Timer bounceTimer;
        private int dx = 5, dy = 5;

        // Dragging variables
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        // Sound variables
        private SoundPlayer[] tracks;
        private int selectedTrack = 0;

        // Random for picture selection
        private Random rnd = new Random();

        public Form1()
        {
            InitializeForm();
            InitializeControls();
            InitializeEvents();

            bounceTimer = new Timer { Interval = 30 };
            bounceTimer.Tick += BounceTimer_Tick;

            InitializeMenu();
            InitializeSoundTracks();

            r_btn2.Checked = true; // Default theme
        }

        private void InitializeForm()
        {
            this.Text = "Vorm elementidega - Ühe ekraani versioon";
            this.Size = new Size(900, 600);
        }

        private void InitializeControls()
        {
            lbl = new Label
            {
                Text = "Elementide loomine C# abil",
                Font = new Font("Arial", 20, FontStyle.Bold),
                Size = new Size(500, 40),
                Location = new Point(20, 10),
                AutoSize = false
            };

            btn = new Button
            {
                Text = "Ava YouTube link",
                Size = new Size(180, 35),
                Location = new Point(20, 60)
            };

            // Save button on main form
            saveMainBtn = new Button
            {
                Text = "Save Current Sheet",
                Size = new Size(150, 35),
                Location = new Point(20, 320)
            };
            saveMainBtn.Click += SaveMainBtn_Click;

            // ListBox for predefined colors
            lb = new ListBox
            {
                Size = new Size(150, 90),
                Location = new Point(20, 110)
            };
            lb.Items.AddRange(new string[] { "Roheline", "Punane", "Sinine", "Hall", "Kollane" });
            lb.SelectedIndexChanged += Lb_SelectedIndexChanged;

            // Small custom color button (WHY R U MISSPLACES WEK,FERPM,FPGERGFREPFPGE)
            btnCustomColor = new Button
            {
                Text = "...",
                Size = new Size(30, 30),
                Location = new Point(lb.Right + 5, lb.Top)
            };
            btnCustomColor.Click += BtnCustomColor_Click;

            pic = new PictureBox
            {
                Size = new Size(140, 140),
                Location = new Point(650, 60),
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            try
            {
                pic.Image = Image.FromFile(@"..\\..\\Images\\close_box_red.png");
            }
            catch { }

            c_btn1 = new CheckBox
            {
                Text = "Pildi ääris (3D)", Location = new Point(200, 110)
            };
            c_btn2 = new CheckBox
            {
                Text = "Näita pilti", Location = new Point(200, 140), Checked = true
            };
            c_btn3 = new CheckBox
            {
                Text = "Näita silti", Location = new Point(200, 170), Checked = true
            };
            c_btn4 = new CheckBox
            {
                Text = "Luba heli", Location = new Point(200, 200), Checked = true
            };
            c_btn5 = new CheckBox
            {
                Text = "Pildi bounce (liigu aknas)", Location = new Point(200, 230)
            };

            r_btn1 = new RadioButton
            {
                Text = "Tume teema", Location = new Point(20, 220)
            };
            r_btn2 = new RadioButton
            {
                Text = "Hele teema", Location = new Point(20, 250)
            };
            r_btn3 = new RadioButton
            {
                Text = "Sinine teema", Location = new Point(20, 280)
            };

            tabC = new TabControl
            {
                Location = new Point(360, 220), Size = new Size(480, 320)
            };

            // Initial tab with RichTextBox
            tabP1 = new TabPage("Märkmik");
            Panel panel1 = new Panel
            {
                Dock = DockStyle.Fill
            };
            RichTextBox sheet1 = new RichTextBox
            {
                Dock = DockStyle.Fill, Text = "Siia saad kirjutada..."
            };
            panel1.Controls.Add(sheet1);
            tabP1.Controls.Add(panel1);

            tabP2 = new TabPage("Info");
            Label info = new Label
            {
                Text = "See on informatsiooni kaart.", Location = new Point(10, 10), AutoSize = true
            };
            PictureBox tabPic = new PictureBox
            { 
               Size = new Size(100, 100),Location = new Point(10, 40), SizeMode = PictureBoxSizeMode.StretchImage
            };
            try
            {
                tabPic.Image = Image.FromFile(@"..\\..\\Images\\about.png");
            }
            catch { }

            tabP2.Controls.Add(info);
            tabP2.Controls.Add(tabPic);

            tabP3 = new TabPage("+");
            tabP3.DoubleClick += TabP3_DoubleClick;

            tabC.TabPages.AddRange(new TabPage[]
            { tabP1, tabP2, tabP3 });

            this.Controls.AddRange(new Control[]
            { lbl, btn, pic, lb, btnCustomColor, c_btn1, c_btn2, c_btn3, c_btn4, c_btn5, r_btn1, r_btn2, r_btn3, tabC, saveMainBtn });
        }

        private void InitializeEvents()
        {
            btn.Click += Btn_Click;
            btn.MouseDown += Btn_MouseDown;
            btn.MouseMove += Btn_MouseMove;
            btn.MouseUp += Btn_MouseUp;

            pic.DoubleClick += Pic_DoubleClick;

            c_btn1.CheckedChanged += C_btn1_CheckedChanged;
            c_btn2.CheckedChanged += (s, e) => pic.Visible = c_btn2.Checked;
            c_btn3.CheckedChanged += (s, e) => lbl.Visible = c_btn3.Checked;
            c_btn5.CheckedChanged += C_btn5_CheckedChanged;

            c_btn4.CheckedChanged += (s, e) =>
            {
                if (!c_btn4.Checked)
                    foreach (var track in tracks) track?.Stop();
                else PlaySelectedTrack();
            };

            r_btn1.CheckedChanged += ThemeRadio_CheckedChanged;
            r_btn2.CheckedChanged += ThemeRadio_CheckedChanged;
            r_btn3.CheckedChanged += ThemeRadio_CheckedChanged;

            tabC.Selected += TabC_Selected;
        }

        private void InitializeMenu()
        {
            MainMenu menu = new MainMenu();
            MenuItem fileMenu = new MenuItem("File");
            fileMenu.MenuItems.Add("Restart", (s, e) => Application.Restart());
            fileMenu.MenuItems.Add("Exit", (s, e) => this.Close());
            menu.MenuItems.Add(fileMenu);

            MenuItem settingsMenu = new MenuItem("Settings");
            MenuItem soundMenu = new MenuItem("Sound Settings");
            soundMenu.MenuItems.Add("Track 1", (s, e) => { selectedTrack = 0; PlaySelectedTrack(); });
            soundMenu.MenuItems.Add("Track 2", (s, e) => { selectedTrack = 1; PlaySelectedTrack(); });
            soundMenu.MenuItems.Add("Track 3", (s, e) => { selectedTrack = 2; PlaySelectedTrack(); });
            settingsMenu.MenuItems.Add(soundMenu);
            menu.MenuItems.Add(settingsMenu);

            MenuItem helpMenu = new MenuItem("Help");
            helpMenu.MenuItems.Add("About", (s, e) => MessageBox.Show("See on näidisvorm, mille autor on Hussein.", "About"));
            menu.MenuItems.Add(helpMenu);

            this.Menu = menu;
        }

        private void InitializeSoundTracks()
        {
            tracks = new SoundPlayer[3];
            for (int i = 0; i < 3; i++)
            {
                try { tracks[i] = new SoundPlayer($"..\\..\\Sound\\track{i + 1}.wav"); } catch { tracks[i] = null; }
            }
        }

        public void PlaySelectedTrack()
        {
            foreach (var track in tracks) track?.Stop();
            if (c_btn4.Checked && tracks[selectedTrack] != null)
                tracks[selectedTrack].PlayLooping();
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = "https://www.youtube.com/watch?v=CdSYXpC2sd8", UseShellExecute = true });
        }

        private void Btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                dragCursorPoint = Cursor.Position;
                dragFormPoint = btn.Location;
            }
        }

        private void Btn_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                btn.Location = Point.Add(dragFormPoint, new Size(diff));
            }
        }

        private void Btn_MouseUp(object sender, MouseEventArgs e) => dragging = false;

        private void Pic_DoubleClick(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                    ofd.Title = "Vali pilt";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            pic.Image = Image.FromFile(ofd.FileName);
                        } catch { MessageBox.Show("Pilti ei saanud avada.", "Error"); }
                    }
                }
            }
            else
            {
                string[] images = { "esimene.jpg", "teine.jpg", "kolmas.jpg" };
                string selectedImage = images[rnd.Next(images.Length)];
                string path = $@"..\..\Images\{selectedImage}";

                if (System.IO.File.Exists(path))
                    pic.Image = Image.FromFile(path);
                else
                    MessageBox.Show($"Pilt '{selectedImage}' ei leitud!", "Error");
            }
        }

        private void Lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = lb.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selected)) return;

            switch (selected)
            {
                case "Roheline":
                    this.BackColor = Color.LightGreen;
                    break;
                case "Punane":
                    this.BackColor = Color.LightCoral;
                    break;
                case "Sinine":
                    this.BackColor = Color.LightBlue;
                    break;
                case "Hall":
                    this.BackColor = Color.LightGray;
                    break;
                case "Kollane":
                    this.BackColor = Color.LightYellow;
                    break;
                default:
                    if (selected.StartsWith("RGB(") && selected.EndsWith(")"))
                    {
                        string inner = selected.Substring(4, selected.Length - 5);
                        string[] parts = inner.Split(',');
                        if (parts.Length == 3 &&
                            int.TryParse(parts[0], out int r) &&
                            int.TryParse(parts[1], out int g) &&
                            int.TryParse(parts[2], out int b))
                        {
                            this.BackColor = Color.FromArgb(r, g, b);
                        }
                    }
                    break;
            }
        }

        private void BtnCustomColor_Click(object sender, EventArgs e)
        {
            using (ColorDialog cd = new ColorDialog())
            {
                cd.FullOpen = true;
                cd.Color = this.BackColor;

                if (cd.ShowDialog() == DialogResult.OK)
                {
                    this.BackColor = cd.Color;
                    string rgbText = $"RGB({cd.Color.R},{cd.Color.G},{cd.Color.B})";
                    if (!lb.Items.Contains(rgbText)) lb.Items.Add(rgbText);
                }
            }
        }

        private void C_btn1_CheckedChanged(object sender, EventArgs e)
        {
            if (c_btn1.Checked)
            {
                pic.BorderStyle = BorderStyle.Fixed3D;
            }
            else
            {
                pic.BorderStyle = BorderStyle.None;
            }
        }
        private void C_btn5_CheckedChanged(object sender, EventArgs e) => bounceTimer.Enabled = c_btn5.Checked;

        private void BounceTimer_Tick(object sender, EventArgs e)
        {
            pic.Left += dx;
            pic.Top += dy;

            if (pic.Right >= this.ClientSize.Width || pic.Left <= 0)
                dx = -dx;
            if (pic.Bottom >= this.ClientSize.Height || pic.Top <= 0)
                dy = -dy;
        }

        private void TabC_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabP3)
                AddNewTab(); 
        }
        private void TabP3_DoubleClick(object sender, EventArgs e) => AddNewTab();

        private void AddNewTab()
        {
            TabPage newTab = new TabPage("Sheet " + (tabC.TabCount));
            Panel panel = new Panel { Dock = DockStyle.Fill };
            RichTextBox sheet = new RichTextBox
            { 
                Dock = DockStyle.Fill, Text = "Uus leht..." 
            };
            panel.Controls.Add(sheet);
            newTab.Controls.Add(panel);
            tabC.TabPages.Insert(tabC.TabCount - 1, newTab);
        }

        private void SaveMainBtn_Click(object sender, EventArgs e)
        {
            TabPage currentTab = tabC.SelectedTab;
            if (currentTab == null)
                return;

            RichTextBox sheet = null;
            foreach(Control c in currentTab.Controls)
            {
                if (c is Panel panel)
                {
                    foreach (Control pc in panel.Controls)
                        if (pc is RichTextBox rtb)
                        {
                            sheet = rtb;
                            break;
                        }
                }
                else if (c is RichTextBox rtb)
                {
                    sheet = rtb;
                    break;
                }
            }

            if (sheet == null)
            {
                MessageBox.Show("No sheet found in this tab.", "Error");
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                sfd.Title = "Save your sheet";
                sfd.FileName = "Sheet.txt";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try {
                        System.IO.File.WriteAllText(sfd.FileName, sheet.Text);
                        MessageBox.Show("File saved successfully!", "Saved");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error saving file: " + ex.Message, "Error"); 
                    }
                }
            }
        }

        private void ThemeRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (r_btn1.Checked)
                ApplyTheme("Dark");

            else if (r_btn2.Checked)
                ApplyTheme("Light");

            else if (r_btn3.Checked)
                ApplyTheme("Blue");
        }

        private void ApplyTheme(string theme)
        {
            switch (theme)
            {
                case "Dark":
                    this.BackColor = Color.FromArgb(30, 30, 30); SetForeColor(Color.White);
                    break;
                case "Light":
                    this.BackColor = Color.White; SetForeColor(Color.Black);
                    break;
                case "Blue":
                    this.BackColor = Color.LightBlue; SetForeColor(Color.DarkBlue);
                    break;
            }
        }

        private void SetForeColor(Color fore)
        {
            foreach (Control ctrl in this.Controls)
            {
                if (!(ctrl is PictureBox))
                    ctrl.ForeColor = fore;
                if (ctrl is TabControl tabC)
                {
                    foreach (TabPage tab in tabC.TabPages)
                    {
                        foreach (Control tctrl in tab.Controls)
                        {
                            if (!(tctrl is PictureBox)) tctrl.ForeColor = fore;
                        }
                    }
                }
            }
        }
    }
}
