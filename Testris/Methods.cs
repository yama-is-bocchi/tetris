using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Testris
{
    public class Methods
    {
        //ソフトドロップできるか?
        public bool Can_soft_drop(ref int[,] p_arr)
        {
            int count = 0;
            bool[] ok=new bool[4];
            for (int i = 19; i >= 0; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (p_arr[i, j] < 0 && (i + 1) < 20)
                    {
                        if ((p_arr[i + 1, j] * p_arr[i, j]) >= 0)
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
                return true;
            }
            else
            {
                return false;
            }
        }

        //配列をコピー
        public int[,] Copy_array(ref int[,] out_array, int[,] cp_array)
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    out_array[i, j] = cp_array[i, j];
                }
            }
            return out_array;
        }

        public int[,] Eliminate_cur_array(ref int[,] out_array, int[,] p_array)
        {
            int cout = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (p_array[i, j] < 0)
                    {
                        out_array[i, j] = 0;
                        cout++;
                        if (cout == 4) break;
                    }
                }
                if (cout == 4) break;
            }
            return out_array;
        }

        //カレントブロックの情報を配列に格納
        public int[,] Insert_form_array(ref int p_rand, int rotate)
        {
            int[,] form = new int[4, 4];
            if (p_rand == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        form[i, j] = Block_form.I_block[rotate, i, j];

                    }
                }
            }
            else if (p_rand == 2)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        form[i, j] = Block_form.O_block[rotate, i, j];
                    }
                }
            }
            else if (p_rand == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        form[i, j] = Block_form.S_block[rotate, i, j];
                    }
                }
            }
            else if (p_rand == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        form[i, j] = Block_form.Z_block[rotate, i, j];
                    }
                }
            }
            else if (p_rand == 5)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        form[i, j] = Block_form.L_block[rotate, i, j];
                    }
                }
            }
            else if (p_rand == 6)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        form[i, j] = Block_form.J_block[rotate, i, j];
                    }
                }
            }
            else if (p_rand == 7)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        form[i, j] = Block_form.T_block[rotate, i, j];
                    }
                }
            }
            return form;
        }

        public void Read_key_bind_file()
        {
            StreamReader sr = new StreamReader(@"Resources\key_bind.csv");//フィールドデータ読み取り
            {
                while (!sr.EndOfStream)
                {
                    Key_bind.move_right = sr.ReadLine() + "";
                    Key_bind.move_left = sr.ReadLine() + "";
                    Key_bind.rotate_right = sr.ReadLine() + "";
                    Key_bind.rotate_left = sr.ReadLine() + "";
                    Key_bind.soft_drop = sr.ReadLine() + "";
                    Key_bind.hard_drop = sr.ReadLine() + "";
                    Key_bind.hold = sr.ReadLine() + "";
                    User_formation.bgm_volume = Convert.ToInt16(sr.ReadLine());
                    User_formation.effect_volume = Convert.ToInt16(sr.ReadLine());
                    User_formation.score = Convert.ToInt16(sr.ReadLine());
                }
            }
            sr.Close();
        }



        //ハードドロップした時の配列を返す  
        public int[,] Hard_droped_arr(ref int[,] p_arr)
        {

            Methods methods = new Methods();
            int[,] ret_arr=new int[20,10];
            ret_arr =methods.Copy_array(ref ret_arr,p_arr);

            for(; ; )
            {
                if (methods.Can_soft_drop(ref ret_arr)==true)
                {
                    //配列を1下げる
                     int count = 0;
                    for (int i = 19; i >= 0; i--)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            if (ret_arr[i, j] < 0)
                            {
                                ret_arr[i + 1, j] = ret_arr[i, j];
                                ret_arr[i, j] = 0;
                                count++;
                                if (count == 4) break;
                            }
                        }
                        if (count == 4) break;
                    }
                }
                else
                {
                    break;
                }
            }
            return ret_arr;
        }

        //ゲームオーバーか判定
        public bool Can_next_game(ref int[,] p_field)
        {
            for(int i=0;i<2;i++)
            {
                for (int j=0;j<4;j++)
                {
                    if (p_field[i, j + 3] > 0)
                    {
                        return false;
                    }
                }

            }

            return true;
        }

    }
}
