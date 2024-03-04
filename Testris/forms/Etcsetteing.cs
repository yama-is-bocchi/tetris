using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testris.forms
{
    public partial class Etcsetteing : Form
    {
        public Etcsetteing()
        {
            InitializeComponent();
            init_form();
        }
        private string cur_bind;
        private List<string> btn_list = new List<string>() { "Move_right", "Move_left", "Rotate_right", "Rotate_left", "Soft_drop", "Hard_drop", "Hold" };//紫

        private void init_form()
        {
            Move_right.Text=Key_bind.move_right;
            Move_left.Text = Key_bind.move_left;
            Rotate_right.Text = Key_bind.rotate_right;
            Rotate_left.Text = Key_bind.rotate_left;
            Soft_drop.Text = Key_bind.soft_drop;
            Hard_drop.Text = Key_bind.hard_drop;
            Hold.Text = Key_bind.hold;
            Bgm_track.Value = (User_formation.bgm_volume/10);
            Effect_track.Value = (User_formation.effect_volume / 10);
        }



        private void MouseEnter_(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.LimeGreen;
            ((Button)sender).ForeColor = Color.Black;
        }

        private void MouseLeave_(object sender, EventArgs e)
        {
            ((Button)sender).ForeColor = Color.LimeGreen;
            ((Button)sender).BackColor = Color.Black;
        }

        private void Ok_btn_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
        }

        private void key_btn_MouseClick(object sender, MouseEventArgs e)
        {
            setting_panel.Visible = false;
            KeyPreview = true;
            Wait_label.Visible = true;
            cur_bind = ((Button)sender).Name;

        }

        private void Check_duplicate(ref string p_code, string p_keybind)
        {
            foreach (string btn_name in btn_list)
            {
                Control[] work = setting_panel.Controls.Find(btn_name, true);
                if (work[0].Text == p_code)
                {
                    Control[] temp = setting_panel.Controls.Find(p_keybind, true);
                    work[0].Text = temp[0].Text;
                }
            }

        }

        private void Etcsetteing_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (string btn_name in btn_list)
            {
                if (cur_bind == btn_name)
                {
                    string p_code = e.KeyCode.ToString();
                    Check_duplicate(ref p_code, cur_bind);
                    Control[] work = setting_panel.Controls.Find(cur_bind, true);
                    work[0].Text = e.KeyCode.ToString();
                }
            }
            KeyPreview = false;
            Write_key_bind();
            setting_panel.Visible = true;
            Wait_label.Visible=false;
        }

        //キーバインド上書き
        private void Write_key_bind()
        {
            using (StreamWriter sw = new StreamWriter(@"Resources\key_bind.csv", false,
                                                     Encoding.GetEncoding("utf-8")))
            {
                foreach (string line in btn_list)
                {
                    Control[] work = setting_panel.Controls.Find(line,true);
                    sw.WriteLine(work[0].Text);
                }
                sw.WriteLine((Bgm_track.Value*10));
                sw.WriteLine((Effect_track.Value * 10));
                sw.WriteLine(User_formation.score.ToString());
                sw.Close();
            }
        }

        private void track_Scroll(object sender, EventArgs e)
        {
            Write_key_bind();
        }
    }
}
