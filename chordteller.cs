using System;

namespace project
{
    
    class chordteller
    {
        //找根音
        public static int whatroot(int note6,int note5,int note4,int note3,int note2,int note1,int[] tuning
                                    ,int[] targetnote)
        {
            int[] notematrix1={note6,note5,note4,note3,note2,note1};
            

            //第六弦不為悶音 且 注意最低音兩弦到底誰高
            if (note6!=-1 && targetnote[0]-(tuning[1]-tuning[0])<targetnote[1])
            {
                for (int i=0;i<6;i++)
                {
                    if (notematrix1[i]>=12)
                    {
                        notematrix1[i]=notematrix1[i]%12;
                    }
                }
                return notematrix1[0];
            }
            else
            {
                if (note5!=-1 && targetnote[1]-(tuning[2]-tuning[1]+12)<targetnote[2])
                { 
                    for (int i=0;i<6;i++)
                    {
                        if (notematrix1[i]>=12)
                        {
                            notematrix1[i]=notematrix1[i]%12;
                        }
                    }
                    return notematrix1[1];
                }
                else
                {
                    if (note4!=-1 && targetnote[2]-(tuning[3]-tuning[2])<targetnote[3])
                    {   
                        for (int i=0;i<6;i++)
                        {
                            if (notematrix1[i]>=12)
                            {
                                notematrix1[i]=notematrix1[i]%12;
                            }
                        }
                        return notematrix1[2];
                    }
                    else
                    {
                        for (int i=0;i<6;i++)
                        {
                            if (notematrix1[i]>=12)
                            {
                                notematrix1[i]=notematrix1[i]%12;
                            }
                        }
                        return notematrix1[3];
                        
                    }
                }
            }
        }



        //判別函式庫中的排列組合是否相同
        public static void distinguish(int[] notearray,int[]chordorder,string Root,string wtf)
        {
            int countforchord=0;
            if (notearray.Length==chordorder.Length)
            {
                for(int i=0;i<notearray.Length;i++)
                {
                    if (notearray[i]==chordorder[i])
                    {
                        countforchord++;
                    }
                }
                if (countforchord==chordorder.Length)
                {
                    Console.WriteLine(Root+wtf);
                }
            }
        }


       

    }
}
