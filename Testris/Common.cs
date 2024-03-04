using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testris
{
    public class Common
    {
        public const int size = 30;

        public const int x_start_pos = 90;

        public const int y_start_pos = 60;

        public const int x_reseve_start_pos = 400;

        public const int x_hold_start_pos = 460;

        public const int y_reseve1_start_pos = 120;

        public const int y_reseve2_start_pos = 220;

        public const int y_reseve3_start_pos = 340;

        public const int y_hold_pos = 440;

        public const int reserver_size = 20;

        public static List<string> ColorList = new List<string>() { "#00ffff",//水色
                                                                    "#ffff00",//黄色
                                                                    "#008000",//ミドリ
                                                                    "#ff0000",//レッド
                                                                    "#0000ff",//青
                                                                    "#FFA500",//オレンジ
                                                                    "#800080"};//紫

        public static ReadOnlyCollection<string> readOnlycolor =
            new ReadOnlyCollection<string>(ColorList);
    }


    public class Key_bind
    {
        public static string move_right;
        public static string move_left;
        public static string rotate_right;
        public static string rotate_left;
        public static string soft_drop;
        public static string hard_drop;
        public static string hold;

    }

    public class User_formation
    {
        public static int score;
        public static int bgm_volume;
        public static int effect_volume;
    }

    public class Block_form
    {
        public static int[,,] I_block = new int[,,]
        {
                {
                    {0,0,0,0 },
                    {1,1,1,1 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                 },
                {
                    {0,0,1,0 },
                    {0,0,1,0 },
                    {0,0,1,0 },
                    {0,0,1,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,0,0 },
                    {1,1,1,1 },
                    {0,0,0,0 }
                },
                {
                    {0,1,0,0 },
                    {0,1,0,0 },
                    {0,1,0,0 },
                    {0,1,0,0 }
                }
        };
        public static int[,,] O_block = new int[,,]
       {

                {
                    {2,2,0,0 },
                    {2,2,0,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                 },
                {
                    {2,2,0,0 },
                    {2,2,0,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {2,2,0,0 },
                    {2,2,0,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                },
                {
                    {2,2,0,0 },
                    {2,2,0,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                }


       };
        public static int[,,] S_block = new int[,,]
       {

                {
                    {0,3,3,0 },
                    {3,3,0,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                 },
                {
                    {0,3,0,0 },
                    {0,3,3,0 },
                    {0,0,3,0 },
                    {0,0,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,3,3,0 },
                    {3,3,0,0 },
                    {0,0,0,0 }
                },
                {
                    {3,0,0,0 },
                    {3,3,0,0 },
                    {0,3,0,0 },
                    {0,0,0,0 }
                }


       };
        public static int[,,] Z_block = new int[,,]
      {

                {
                    {4,4,0,0 },
                    {0,4,4,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                 },
                {
                    {0,0,4,0 },
                    {0,4,4,0 },
                    {0,4,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,0,0,0 },
                    {4,4,0,0 },
                    {0,4,4,0 },
                    {0,0,0,0 }
                },
                {
                    {0,4,0,0 },
                    {4,4,0,0 },
                    {4,0,0,0 },
                    {0,0,0,0 }
                }


      };
        public static int[,,] L_block = new int[,,]
      {

                {
                    {5,5,5,0 },
                    {5,0,0,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                 },
                {
                    {0,5,5,0 },
                    {0,0,5,0 },
                    {0,0,5,0 },
                    {0,0,0,0 }
                },
                {
                    {0,0,0,0 },
                    {0,0,5,0 },
                    {5,5,5,0 },
                    {0,0,0,0 }
                },
                {
                    {5,0,0,0 },
                    {5,0,0,0 },
                    {5,5,0,0 },
                    {0,0,0,0 }
                }


      };
        public static int[,,] J_block = new int[,,]
    {

                {
                    {6,6,6,0 },
                    {0,0,6,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                 },
                {
                    {0,0,6,0 },
                    {0,0,6,0 },
                    {0,6,6,0 },
                    {0,0,0,0 }
                },
                {
                    {0,0,0,0 },
                    {6,0,0,0 },
                    {6,6,6,0 },
                    {0,0,0,0 }
                },
                {
                    {6,6,0,0 },
                    {6,0,0,0 },
                    {6,0,0,0 },
                    {0,0,0,0 }
                }


      };
        public static int[,,] T_block = new int[,,]
   {

                {
                    {0,7,0,0 },
                    {7,7,7,0 },
                    {0,0,0,0 },
                    {0,0,0,0 }
                 },
                {
                    {0,7,0,0 },
                    {0,7,7,0 },
                    {0,7,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,0,0,0 },
                    {7,7,7,0 },
                    {0,7,0,0 },
                    {0,0,0,0 }
                },
                {
                    {0,7,0,0 },
                    {7,7,0,0 },
                    {0,7,0,0 },
                    {0,0,0,0 }
                }


     };
    }


}
