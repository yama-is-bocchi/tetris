using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Testris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            methods.Read_key_bind_file();
        }
        //フィールド
        private int[,] Field = new int[20, 10];
        private Point Cur_point = new Point();
        private int timer=3;
        private int Rotate_num;
        private int Control_count;
        private bool Exist_hold;
        private bool Exist_use_hold;
        private bool Game_over;

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern int mciSendString(String command, StringBuilder buffer, int bufferSize, IntPtr hwndCallback);//wave再生用変数
        Methods methods = new Methods();

        //スタートボタン
        private void start_btn_MouseClick(object sender, MouseEventArgs e)
        {
            string bgm_name = "game";
            Bgm_sound_st(ref bgm_name);
            bgm_name ="countdown";
            effect_sound_st(ref bgm_name);
            reset_btn.Visible = false;
            setting_btn.Visible=false;
            st_label.Visible = true;
            start_btn.Visible = false;
            st_wait_timer.Start();
        }

        //フィールド初期化
        private void init_field()
        {
            //データ読み取り
            Read_data_information(ref Field);

            //変更
            Change_field();

            Score_label.Text = User_formation.score.ToString();

            //乱数cur.next,secound,thirdブロック生成
            Random random = new Random();
            int Cur_rand = random.Next(7) + 1;
            Rotate_num = 0;
            int[,] Cur_form = methods.Insert_form_array(ref Cur_rand, Rotate_num);

            Generate_cur_block(ref Cur_rand, Cur_form);
            //予約ブロック生成
            Generate_reserver();
            //透過ブロック生成
            Generate_opa_block();

            Check_score();

            soft_drop_timer.Start();
        }

        private void init_object()
        {
            Field = new int[20, 10];
            start_btn.Visible = true;
            for (int i=0;i<Control_count;i++)
            {
                Main_panel.Controls.RemoveAt(Main_panel.Controls.Count-1);
            }
        }


        //CSVファイル読み取り
        private void Read_data_information(ref int[,] p_array)
        {
            StreamReader sr = new StreamReader(@"Resources\fielddata.csv");//フィールドデータ読み取り
            {
                while (!sr.EndOfStream)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        string line = sr.ReadLine() + "";

                        for (int j = 0; j < 10; j++)
                        {
                            if (Convert.ToInt16(line.Substring(j, 1)) > 0)
                            {
                                p_array[i, j] = Convert.ToInt16(line.Substring(j, 1));
                            }
                            else
                            {
                                p_array[i, j] = 0;
                            }
                        }
                    }
                }
                sr.Close();
            }

        }

        //フィールドデータを書き込む
        private void Write_field_data()
        {
            using (StreamWriter sw = new StreamWriter(@"Resources\fielddata.csv", false,
                                                      Encoding.GetEncoding("utf-8")))
            {
                string line="";
                for (int i=0;i<20;i++)
                {
                    for (int j=0;j<10;j++)
                    {
                        if (Field[i, j] >= 0)
                        {
                            line += Field[i, j].ToString();
                        }
                        else
                        {
                            line += "0";
                        }
                        
                    }
                    sw.WriteLine(line);
                    line = "";
                }
                sw.Close();
            }
        }

        private void Write_score()
        {
            using (StreamWriter sw = new StreamWriter(@"Resources\key_bind.csv", false,
                                                     Encoding.GetEncoding("utf-8")))
            {
                sw.WriteLine(Key_bind.move_right);
                sw.WriteLine(Key_bind.move_left);
                sw.WriteLine(Key_bind.rotate_right);
                sw.WriteLine(Key_bind.rotate_left);
                sw.WriteLine(Key_bind.soft_drop);
                sw.WriteLine(Key_bind.hard_drop);
                sw.WriteLine(Key_bind.hold);
                sw.WriteLine(User_formation.bgm_volume);
                sw.WriteLine(User_formation.effect_volume);
                sw.WriteLine(User_formation.score);
                sw.Close();
            }
        }

        //フィールドを変更する
        private void Change_field()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Field[i, j] > 0)//ブロックを生成
                    {
                        Panel work = new Panel();
                        work.BringToFront();
                        work.Size = new Size(Common.size, Common.size);
                        work.BackColor = ColorTranslator.FromHtml(Common.readOnlycolor[Field[i, j] - 1]);
                        work.Location = new Point((j * Common.size) + Common.x_start_pos, (i * Common.size) + Common.y_start_pos);
                        work.Name = "obstacle" + i.ToString() + j.ToString();
                        work.BorderStyle = BorderStyle.Fixed3D;
                        Main_panel.Controls.Add(work);
                        Control_count++;
                        work.SuspendLayout();
                        work.Show();
                    }
                }

            }
        }





        //カレントブロックを生成する
        private void Generate_cur_block(ref int p_rand, int[,] p_form)
        {
            int count = 0;
            Cur_point = new Point(3, 0);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (p_form[i, j] > 0)
                    {
                        Panel work = new Panel();
                        work.BringToFront();
                        work.Size = new Size(Common.size, Common.size);
                        work.BackColor = ColorTranslator.FromHtml(Common.readOnlycolor[p_rand - 1]);
                        work.Location = new Point(Common.x_start_pos + ((Common.size * 3) + (Common.size * j)), Common.y_start_pos + (Common.size * i));
                        work.Name = "cur" + count.ToString();
                        work.Tag = p_form[i, j].ToString();
                        work.BorderStyle = BorderStyle.Fixed3D;
                        Main_panel.Controls.Add(work);
                        Control_count++;
                        work.SuspendLayout();
                        work.Show();
                        Field[i, j + 3] = p_form[i, j] * -1;
                        count++;
                    }
                }
            }
        }

        private void Generate_reserver()
        {
            Random rand = new Random();
            int rnd = rand.Next(7) + 1;
            int p_rotate = 0;
            int[,] form = new int[4, 4];
            int count = 0;
            form = methods.Insert_form_array(ref rnd, p_rotate);


            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (form[i, j] > 0)
                    {
                        Panel work = new Panel();
                        work.BringToFront();
                        work.Size = new Size(Common.reserver_size, Common.reserver_size);
                        work.BackColor = ColorTranslator.FromHtml(Common.readOnlycolor[rnd - 1]);
                        work.Location = new Point(Common.x_reseve_start_pos + ((Common.reserver_size * 3) + (Common.reserver_size * j)), Common.y_reseve1_start_pos + (Common.reserver_size * i));
                        work.Name = "reserve1" + count.ToString();
                        work.Tag = rnd.ToString();
                        work.BorderStyle = BorderStyle.Fixed3D;
                        Main_panel.Controls.Add(work);
                        Control_count++;
                        work.SuspendLayout();
                        work.Show();
                        count++;
                    }
                }
            }
            count = 0;
            rnd = rand.Next(7) + 1;
            form = methods.Insert_form_array(ref rnd, p_rotate);
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (form[i, j] > 0)
                    {
                        Panel work = new Panel();
                        work.BringToFront();
                        work.Size = new Size(Common.reserver_size, Common.reserver_size);
                        work.BackColor = ColorTranslator.FromHtml(Common.readOnlycolor[rnd - 1]);
                        work.Location = new Point(Common.x_reseve_start_pos + ((Common.reserver_size * 3) + (Common.reserver_size * j)), Common.y_reseve2_start_pos + (Common.reserver_size * i));
                        work.Name = "reserve2" + count.ToString();
                        work.Tag = rnd.ToString();
                        work.BorderStyle = BorderStyle.Fixed3D;
                        Main_panel.Controls.Add(work);
                        Control_count++;
                        work.SuspendLayout();
                        work.Show();
                        count++;
                    }
                }
            }
            Generate_third_block();

        }

        private void Generate_third_block()
        {
            Random rand = new Random();
            int count = 0;
            int rnd = rand.Next(7) + 1;
            int p_rotate = 0;
            int[,] form = methods.Insert_form_array(ref rnd, p_rotate);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (form[i, j] > 0)
                    {
                        Panel work = new Panel();
                        work.BringToFront();
                        work.Size = new Size(Common.reserver_size, Common.reserver_size);
                        work.BackColor = ColorTranslator.FromHtml(Common.readOnlycolor[rnd - 1]);
                        work.Location = new Point(Common.x_reseve_start_pos + ((Common.reserver_size * 3) + (Common.reserver_size * j)), Common.y_reseve3_start_pos + (Common.reserver_size * i));
                        work.Name = "reserve3" + count.ToString();
                        work.Tag = rnd.ToString();
                        work.BorderStyle = BorderStyle.Fixed3D;
                        Main_panel.Controls.Add(work);
                        Control_count++;
                        work.SuspendLayout();
                        work.Show();
                        count++;
                    }
                }
            }
        }

        private void Generate_opa_block()
        {
            int[,] opa_field = methods.Hard_droped_arr(ref Field);
            int count = 0;

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (opa_field[i, j] < 0)//ブロックを生成
                    {
                        Panel work = new Panel();
                        work.BringToFront();
                        work.Size = new Size(Common.size, Common.size);
                        work.BackColor = Color.Gray;
                        work.Location = new Point((j * Common.size) + Common.x_start_pos, (i * Common.size) + Common.y_start_pos);
                        work.Name = "opa" + count.ToString();
                        work.BorderStyle = BorderStyle.Fixed3D;
                        Main_panel.Controls.Add(work);
                        Control_count++;
                        work.SuspendLayout();
                        work.Show();
                        count++;
                        if (count == 4) break;
                    }
                }
                if (count == 4) break;

            }
        }

        private void Change_opa_block()
        {
            int[,] opa_field = methods.Hard_droped_arr(ref Field);
            int count = 0;

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (opa_field[i, j] < 0)//ブロックを生成
                    {
                        Control[] work = Main_panel.Controls.Find("opa" + count.ToString(), true);

                        work[0].Location = new Point((j * Common.size) + Common.x_start_pos, (i * Common.size) + Common.y_start_pos);

                        work[0].SendToBack();

                        count++;
                        if (count == 4) break;
                    }
                }
                if (count == 4) break;

            }
        }



        //キー判定
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyPreview = false;

            if (e.KeyCode.ToString() == Key_bind.move_right)
            {
                //右に動く
                Move_right();

                Change_opa_block();

            }
            else if (e.KeyCode.ToString() == Key_bind.move_left)
            {
                //左に動く
                Move_left();

                Change_opa_block();

            }
            else if (e.KeyCode.ToString() == Key_bind.rotate_right)
            {
                //右回転
                Move_Rotate_right();

                Change_opa_block();

            }
            else if (e.KeyCode.ToString() == Key_bind.rotate_left)
            {
                //左回転
                Move_Rotate_left();

                Change_opa_block();

            }
            else if (e.KeyCode.ToString() == Key_bind.soft_drop)
            {
                //下に落ちる
                Move_down();

            }
            else if (e.KeyCode.ToString() == Key_bind.hard_drop)
            {
                //ハードドロップ
                Move_hard_drop();
                //next
                Next_or_gameover();
            }
            else if (e.KeyCode.ToString() == Key_bind.hold)
            {
                Move_hold();

            }

            if (Game_over!=true)
            {
                KeyPreview=true;
            }

        }


        //右に動く
        private void Move_right()
        {
            int count = 0;
            bool[] ok = new bool[4];

            for (int i = 0; i < 20; i++)
            {
                for (int j = 9; j >= 0; j--)
                {
                    if (Field[i, j] < 0 && (j + 1) < 10)
                    {
                        if ((Field[i, j + 1] * Field[i, j]) >= 0)
                        {
                            ok[count] = true;
                            count++;
                            if (count == 4) break;
                        }
                    }
                }
                if (count == 4) break;
            }


            if (ok.All(n => n == true) == true)
            {
                Cur_point = new Point(Cur_point.X + 1, Cur_point.Y);
                count = 0;
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 9; j >= 0; j--)
                    {

                        if (Field[i, j] < 0)
                        {
                            Field[i, j + 1] = Field[i, j];
                            Field[i, j] = 0;
                            count++;
                            if (count == 4) break;
                        }
                    }
                    if (count == 4) break;
                }

                for (int i = 0; i < 4; i++)
                {
                    Control[] cs = Main_panel.Controls.Find("cur" + i.ToString(), true);
                    cs[0].Location = new Point(cs[0].Location.X + Common.size, cs[0].Location.Y);
                }

                if (methods.Can_soft_drop(ref Field) == true) dropped_jud_timer.Stop();

            }
            else
            {
                return;
            }
        }

        //左に動く
        private void Move_left()
        {
            int count = 0;
            bool[] ok = new bool[4];

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Field[i, j] < 0 && (j - 1) >= 0)
                    {
                        if ((Field[i, j - 1] * Field[i, j]) >= 0)
                        {
                            ok[count] = true;
                            count++;
                            if (count == 4) break;
                        }
                    }
                }
                if (count == 4) break;
            }


            if (ok.All(n => n == true) == true)
            {
                Cur_point = new Point(Cur_point.X - 1, Cur_point.Y);
                count = 0;
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (Field[i, j] < 0)
                        {
                            Field[i, j - 1] = Field[i, j];
                            Field[i, j] = 0;
                            count++;
                            if (count == 4) break;
                        }
                    }
                    if (count == 4) break;
                }

                for (int i = 0; i < 4; i++)
                {
                    Control[] cs = Main_panel.Controls.Find("cur" + i.ToString(), true);
                    cs[0].Location = new Point(cs[0].Location.X - Common.size, cs[0].Location.Y);
                }
                if (methods.Can_soft_drop(ref Field) == true) dropped_jud_timer.Stop();
            }
            else
            {
                return;
            }
        }

        //右回転
        private void Move_Rotate_right()
        {
            dropped_jud_timer.Stop();
            int[,] ret_arr = new int[20, 10];
            ret_arr = methods.Copy_array(ref ret_arr, Field);
            ret_arr = methods.Eliminate_cur_array(ref ret_arr, ret_arr);
            int p_rotate_num = Rotate_num;
            Point work_point = Cur_point;

            p_rotate_num++;
            if (p_rotate_num > 3) p_rotate_num = 0;
            Control[] cs = Main_panel.Controls.Find("cur1", true);
            int p_rnd = Convert.ToInt16(cs[0].Tag);
            int[,] work_form = methods.Insert_form_array(ref p_rnd, p_rotate_num);
            bool over, ok = false;


            if (work_point.X < 0) work_point = new Point(0, work_point.Y);

            for (int up = 0; up < 3; up++)
            {
                for (int horizon = 0; horizon < 3; horizon++)
                {
                    over = false;

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (work_form[i, j] > 0)
                            {
                                if ((work_point.X + j) > 9 || (work_point.Y + i) > 19||(work_point.X<0)||(work_point.Y<0))
                                {
                                    over = true;
                                    break;
                                }
                                else if (ret_arr[work_point.Y + i, work_point.X + j] > 0)
                                {
                                    over = true;
                                    break;
                                }
                                else
                                {
                                    ret_arr[work_point.Y + i, work_point.X + j] = work_form[i, j] * -1;
                                }

                            }
                        }
                        if (over == true) break;
                    }
                    if (over == false)
                    {
                        ok = true;
                        break;
                    }
                    else
                    {
                        ret_arr = methods.Eliminate_cur_array(ref ret_arr, ret_arr);
                        work_point = new Point(work_point.X - 1, work_point.Y);
                    }

                }
                if (ok == true)
                {
                    break;
                }
                else
                {
                    work_point = new Point(work_point.X + 3, work_point.Y - 1);
                }
            }

            if (ok == true)
            {
                Field = methods.Copy_array(ref Field, ret_arr);
                Rotate_num = p_rotate_num;
                Cur_point = work_point;
                int count = 0;

                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (Field[i, j] < 0)
                        {

                            Control[] work = Main_panel.Controls.Find("cur" + count.ToString(), true);
                            work[0].Location = new Point(Common.x_start_pos + (Common.size * j), Common.y_start_pos + (Common.size * i));
                            count++;
                            if (count == 4) break;

                        }
                    }
                    if (count == 4) break;
                }
            }
            else
            {
                dropped_jud_timer.Start();
                return;
            }
        }

        //左回転
        private void Move_Rotate_left()
        {
            dropped_jud_timer.Stop();
            int[,] ret_arr = new int[20, 10];
            ret_arr = methods.Copy_array(ref ret_arr, Field);
            ret_arr = methods.Eliminate_cur_array(ref ret_arr, ret_arr);
            int p_rotate_num = Rotate_num;
            Point work_point = Cur_point;

            p_rotate_num--;
            if (p_rotate_num < 0) p_rotate_num = 3;
            Control[] cs = Main_panel.Controls.Find("cur1", true);
            int p_rnd = Convert.ToInt16(cs[0].Tag);
            int[,] work_form = methods.Insert_form_array(ref p_rnd, p_rotate_num);
            bool over, ok = false;


            if (work_point.X >= 8) work_point = new Point(7, work_point.Y);
            if (work_point.X < 0) work_point = new Point(0, work_point.Y);

            for (int up = 0; up < 3; up++)
            {
                for (int horizon = 0; horizon < 3; horizon++)
                {
                    over = false;

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (work_form[i, j] > 0)
                            {
                                if ((j + work_point.X) > 9 || (i + work_point.Y) > 19 || (work_point.X < 0) || (work_point.Y < 0))
                                {
                                    over = true;
                                    break;
                                }
                                else if (ret_arr[work_point.Y + i, work_point.X + j] > 0)
                                {
                                    over = true;
                                    break;
                                }
                                else
                                {
                                    ret_arr[work_point.Y + i, work_point.X + j] = work_form[i, j] * -1;
                                }

                            }
                        }
                        if (over == true) break;
                    }
                    if (over == false)
                    {
                        ok = true;
                        break;
                    }
                    else
                    {
                        ret_arr = methods.Eliminate_cur_array(ref ret_arr, ret_arr);
                        work_point = new Point(work_point.X + 1, work_point.Y);
                    }

                }
                if (ok == true)
                {
                    break;
                }
                else
                {
                    work_point = new Point(work_point.X - 3, work_point.Y - 1);
                }
            }

            if (ok == true)
            {
                Field = methods.Copy_array(ref Field, ret_arr);
                Rotate_num = p_rotate_num;
                Cur_point = work_point;
                int count = 0;

                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        if (Field[i, j] < 0)
                        {

                            Control[] work = Main_panel.Controls.Find("cur" + count.ToString(), true);
                            work[0].Location = new Point(Common.x_start_pos + (Common.size * j), Common.y_start_pos + (Common.size * i));
                            count++;
                            if (count == 4) break;

                        }
                    }
                    if (count == 4) break;
                }
            }
            else
            {
                dropped_jud_timer.Start();
                return;
            }
        }

        //下に動く
        private void Move_down()
        {
            int count = 0;
            bool[] ok = new bool[4];

            if (methods.Can_soft_drop(ref Field) == true)
            {
                Cur_point = new Point(Cur_point.X, Cur_point.Y + 1);
                count = 0;
                for (int i = 19; i >= 0; i--)
                {
                    for (int j = 0; j < 10; j++)
                    {


                        if (Field[i, j] < 0)
                        {
                            Field[i + 1, j] = Field[i, j];
                            Field[i, j] = 0;
                            count++;
                            if (count == 4) break;
                        }
                    }
                    if (count == 4) break;
                }

                for (int i = 0; i < 4; i++)
                {
                    Control[] cs = Main_panel.Controls.Find("cur" + i.ToString(), true);
                    cs[0].Location = new Point(cs[0].Location.X, cs[0].Location.Y + Common.size);
                }
            }
            else
            {
                dropped_jud_timer.Start();
                return;
            }
        }

        //ハードドロップ
        private void Move_hard_drop()
        {
            Field = methods.Hard_droped_arr(ref Field);

            for (int i = 0; i < 4; i++)
            {
                Control[] opa = Main_panel.Controls.Find("opa" + i.ToString(), true);
                Control[] cur = Main_panel.Controls.Find("cur" + i.ToString(), true);


                cur[0].Location = new Point(opa[0].Location.X, opa[0].Location.Y);
            }

        }

        //ホールド
        private void Move_hold()
        {
            if (Exist_use_hold == false)
            {
                Exist_use_hold = true;
                soft_drop_timer.Stop();

                if (Exist_hold == false)
                {
                    Control[] cur = Main_panel.Controls.Find("cur0", true);
                    int p_rand = Convert.ToInt16(cur[0].Tag);
                    int count = 0;
                    int[,] temp_form = methods.Insert_form_array(ref p_rand, count);


                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (temp_form[i, j] > 0)
                            {
                                Control[] work = Main_panel.Controls.Find("cur" + count.ToString(), true);
                                work[0].Name = "hold" + count.ToString();
                                work[0].Size = new Size(Common.reserver_size, Common.reserver_size);
                                work[0].Location = new Point(Common.x_hold_start_pos + (j * Common.reserver_size), Common.y_hold_pos + (i * Common.reserver_size));
                                count++;
                                if (count == 4) break;
                            }
                        }
                        if (count == 4) break;
                    }

                    Field = methods.Eliminate_cur_array(ref Field, Field);
                    Migrate_reserve_block();
                    Exist_hold = true;
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Control[] work = Main_panel.Controls.Find("cur" + i.ToString(), true);
                        work[0].Name = "temp" + i.ToString();
                    }

                    Field = methods.Eliminate_cur_array(ref Field, Field);
                    Control[] cur = Main_panel.Controls.Find("hold0", true);
                    int p_rand = Convert.ToInt16(cur[0].Tag);
                    int count = 0;
                    int[,] temp_form = methods.Insert_form_array(ref p_rand, count);

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (temp_form[i, j] > 0)
                            {
                                Control[] work = Main_panel.Controls.Find("hold" + count.ToString(), true);
                                work[0].Name = "cur" + count.ToString();
                                work[0].Size = new Size(Common.size, Common.size);
                                work[0].Location = new Point(Common.x_start_pos + ((j + 3) * Common.size), Common.y_start_pos + (i * Common.size));
                                Field[i, j + 3] = temp_form[i, j] * -1;
                                count++;
                                if (count == 4) break;
                            }
                        }
                        if (count == 4) break;
                    }
                    cur = Main_panel.Controls.Find("temp0", true);
                    p_rand = Convert.ToInt16(cur[0].Tag);
                    count = 0;
                    temp_form = methods.Insert_form_array(ref p_rand, count);

                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (temp_form[i, j] > 0)
                            {
                                Control[] work = Main_panel.Controls.Find("temp" + count.ToString(), true);
                                work[0].Name = "hold" + count.ToString();
                                work[0].Size = new Size(Common.reserver_size, Common.reserver_size);
                                work[0].Location = new Point(Common.x_hold_start_pos + (j * Common.reserver_size), Common.y_hold_pos + (i * Common.reserver_size));
                                count++;
                                if (count == 4) break;
                            }
                        }
                        if (count == 4) break;
                    }

                    Rotate_num = 0;
                    Cur_point = new Point(3,0);
                    Change_opa_block();

                }

                Check_score();

                soft_drop_timer.Start();
            }
        }

        //DROPタイマーorハードドロップ
        private void Next_or_gameover()
        {
            dropped_jud_timer.Stop();
            soft_drop_timer.Stop();

            int count = 0;
            //curをobstacleにする
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Field[i, j] < 0)
                    {
                        Field[i, j] *= -1;
                        Control[] temp = Main_panel.Controls.Find("cur" + count.ToString(), true);
                        temp[0].Name = "obstacle" + i.ToString() + j.ToString();
                        count++;
                        if (count == 4) break;
                    }
                }
                if (count == 4) break;
            }

            if (methods.Can_next_game(ref Field) == true)
            {
                string effe = "droped";

                effect_sound_st(ref effe);

                //段が消えてるか判定
                Remove_colum();

                Score_label.Text = User_formation.score.ToString();

                Migrate_reserve_block();

                Exist_use_hold = false;

                Write_field_data();

                soft_drop_timer.Start();
            }
            else
            {//ゲームオーバー
                Game_over = true;
                string bgm ="gameover";
                Bgm_sound_st(ref bgm);
                geme_ov.Start();
                init_object();
                Write_field_data();
                Exist_hold = false;
                Exist_use_hold = false;
            }
        }

        //次のブロックを運ぶ
        private void Migrate_reserve_block()
        {

            int count = 0;
            Control[] work = Main_panel.Controls.Find("reserve10", true);
            int p_rand = Convert.ToInt16(work[0].Tag);
            Rotate_num = 0;
            Cur_point = new Point(3, 0);
            int[,] next_form = methods.Insert_form_array(ref p_rand, Rotate_num);

            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 4; j++)
                {
                    if (next_form[i, j] > 0)
                    {
                        work = Main_panel.Controls.Find("reserve1" + count.ToString(), true);
                        work[0].Location = new Point(Common.x_start_pos + ((j + 3) * Common.size), Common.y_start_pos + (i * Common.size));
                        work[0].Name = "cur" + count.ToString();
                        work[0].Size = new Size(Common.size, Common.size);
                        Field[i, j + 3] = next_form[i, j] * -1;
                        count++;
                        if (count == 4) break;
                    }
                }
                if (count == 4) break;

            }
            for (int i = 0; i < 4; i++)
            {
                work = Main_panel.Controls.Find("reserve2" + i.ToString(), true);
                work[0].Location = new Point(work[0].Location.X, work[0].Location.Y - 100);
                work[0].Name = "reserve1" + i.ToString();
            }


            for (int i = 0; i < 4; i++)
            {
                work = Main_panel.Controls.Find("reserve3" + i.ToString(), true);
                work[0].Location = new Point(work[0].Location.X, work[0].Location.Y - 120);
                work[0].Name = "reserve2" + i.ToString();
            }

            //新規ブロック生成
            Generate_third_block();

            Change_opa_block();

        }

        private void Remove_colum()
        {

            bool[] Colum_flag_arr = new bool[10];
            int count = 0;

            for(int i = 0; i < 20; i++)
            {
                for (int j=0;j<10;j++)
                {
                    if (Field[i,j]>0)
                    {
                        Colum_flag_arr[j] = true;
                    }
                }

                if (Colum_flag_arr.All(n => n == true) == true)
                {
                    //一段消す
                    for (int j=0;j<10;j++)
                    {
                        Control[] work = Main_panel.Controls.Find("obstacle" + i.ToString() + j.ToString(), true);
                        if(work.Length>0) Main_panel.Controls.Remove(work[0]);
                        Control_count--;
                    }

                    for (int colum=0;colum<10;colum++) 
                    {
                        for (int j = i; j >= 0; j--)
                        {
                            if ((j-1)<0) break;
                            Field[j, colum] = Field[j-1,colum];
                            Control[] work = Main_panel.Controls.Find("obstacle"+(j-1).ToString()+colum.ToString(), true);
                            if (work.Length>0)
                            {
                                work[0].Name="obstacle"+j.ToString()+colum.ToString();
                                work[0].Location = new Point(work[0].Location.X, work[0].Location.Y+Common.size) ;  

                            }
                        }
                    }

                    count++;
                    if (count == 4) break;
                }
                Colum_flag_arr= new bool[10];

            }
            if (count > 0) Write_score();

            string effe = "onepoint";

            if (count == 1)
            {
                effect_sound_st(ref effe);
                User_formation.score += 1;
            }
            else if(count==2)
            {
                effect_sound_st(ref effe);
                User_formation.score += 3;
            }
            else if (count == 3)
            {
                effect_sound_st(ref effe);
                User_formation.score += 5;

            }
            else if (count == 4)
            {
                effe = "fourpoint";
                effect_sound_st(ref effe);
                User_formation.score += 8;
            }

        }


        //オーバーライド
        protected override bool ProcessDialogKey(Keys keyData)
        {
            return false;
        }

        private void start_btn_MouseEnter(object sender, EventArgs e)
        {
            start_btn.BackColor = Color.LimeGreen;
            start_btn.ForeColor = Color.Black;
        }

        private void start_btn_MouseLeave(object sender, EventArgs e)
        {
            start_btn.BackColor = Color.Black;
            start_btn.ForeColor = Color.LimeGreen;
        }

        private void soft_drop_timer_Tick(object sender, EventArgs e)
        {
            soft_drop_timer.Stop();
            Move_down();
            soft_drop_timer.Start();
        }

        private void dropped_jud_timer_Tick(object sender, EventArgs e)
        {
            dropped_jud_timer.Stop();
            //next
           Next_or_gameover();
        }

        private void reset_btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (start_btn.Visible == true) return;
            
            Bgm_stop();
            soft_drop_timer.Stop();
            dropped_jud_timer.Stop();
            Score_label.Text = "0";
            User_formation.score = 0;
            KeyPreview = false;
            init_object();
            Write_field_data();
            Write_score();
            Exist_hold = false;
            Exist_use_hold = false;
            Control_count = 0;
        }

        private void setting_btn_MouseClick(object sender, MouseEventArgs e)
        {
            Bgm_stop();
            soft_drop_timer.Stop();
            dropped_jud_timer.Stop();
            KeyPreview = false;
            //設定フォームを開く
            Form form = new forms.Etcsetteing();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            Main_panel.Controls.Add(form);
            form.FormClosed +=Closed_form;
            form.BringToFront();
            form.Show();
        }
        private void Closed_form(object sender,EventArgs e )
        {
            Exist_hold = false;
            Exist_use_hold = false;
            init_object();
            methods.Read_key_bind_file();
        }

        private void st_wait_timer_Tick(object sender, EventArgs e)
        {
            timer--;
            if (timer==0)
            {
                //処理
                Game_over = false;
                Control_count = 0;
                KeyPreview = true;
                init_field();
                timer= 3;
                st_label.Text = "3";
                st_label.Visible = false;
                reset_btn.Visible = true;
                setting_btn.Visible = true;
                st_wait_timer.Stop();
            }
            else
            {
                string p_name = "countdown";
                effect_sound_st(ref p_name);
                st_label.Text=timer.ToString(); 
                st_wait_timer.Start();
            }
        }

        private void Bgm_stop()
        {
            string cmd = "stop " + "BGM";
            mciSendString(cmd, null, 0, IntPtr.Zero);
            //閉じる
            cmd = "close " + "BGM";
            mciSendString(cmd, null, 0, IntPtr.Zero);
        }

        private void Bgm_sound_st(ref string bgm_kind)
        {
            string cmd = "stop " + "BGM";
            mciSendString(cmd, null, 0, IntPtr.Zero);
            //閉じる
            cmd = "close " + "BGM";
            mciSendString(cmd, null, 0, IntPtr.Zero);

            string fileName = @"Resources\"+bgm_kind+".mp3";

            //再生
            cmd = "open \"" + fileName + "\" type mpegvideo alias BGM";
            if (mciSendString(cmd, null, 0, IntPtr.Zero) != 0)
                return;

            cmd = "play BGM" + " repeat";
            mciSendString(cmd, null, 0, IntPtr.Zero);

            cmd = "setaudio BGM"  + " volume to "+User_formation.bgm_volume.ToString();
            mciSendString(cmd, null, 0, IntPtr.Zero);
        }

        private void effect_sound_st(ref string effect_kind)
        {
            string cmd = "stop " + "effect";
            mciSendString(cmd, null, 0, IntPtr.Zero);
            //閉じる
            cmd = "close " + "effect";
            mciSendString(cmd, null, 0, IntPtr.Zero);

            string fileName = @"Resources\" + effect_kind + ".mp3";

            //再生
            cmd = "open \"" + fileName + "\" type mpegvideo alias effect";
            if (mciSendString(cmd, null, 0, IntPtr.Zero) != 0)
                return;

            cmd = "play effect";
            mciSendString(cmd, null, 0, IntPtr.Zero);

            cmd = "setaudio effect" + " volume to " + User_formation.effect_volume.ToString();
            cmd = "setaudio effect" + " volume to " + User_formation.bgm_volume.ToString();
            mciSendString(cmd, null, 0, IntPtr.Zero);
        }

        private void geme_ov_Tick(object sender, EventArgs e)
        {
            string cmd = "stop " + "BGM";
            mciSendString(cmd, null, 0, IntPtr.Zero);
            //閉じる
            cmd = "close " + "BGM";
            mciSendString(cmd, null, 0, IntPtr.Zero);
        }
        private void Check_score()
        {
            if (User_formation.score<100)
            {
                soft_drop_timer.Interval = 500;

            }else if (User_formation.score < 200)
            {
                soft_drop_timer.Interval = 300;
            }
            else if (User_formation.score < 300)
            {
                soft_drop_timer.Interval = 200;
            }
            else if (User_formation.score < 400)
            {
                soft_drop_timer.Interval = 150;
            }
            else if (User_formation.score < 500)
            {
                soft_drop_timer.Interval = 100;
            }

        }
    }
}
