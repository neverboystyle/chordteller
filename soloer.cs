using System;

namespace project
{
    class soloer
    {
        public static int[] solooftransform (int[] tuning,int[] fingering,int eight)
        {
            int[] thediffernce={0,tuning[1]-tuning[0],tuning[2]-tuning[1],tuning[3]-tuning[2],
                                tuning[4]-tuning[3],tuning[5]-tuning[4]};
            int thenote =0;
            int count=0;
            for (int i=0;i<6;i++)
            {
                if (fingering[i]!=0)
                {
                    thenote=fingering[i];
                    count=6-i;
                }
            }
            thenote+=12;
            for (int i=0;i<6;i++)
            {
                if (count==6 || thenote>15)
                {
                    count--;
                    thenote-=5;
                }
                else
                {
                    break;
                }
            }
            return fingering;

        }
    }
}