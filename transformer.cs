using System;

namespace project
{
    class transformer
    {
        //輸入跟音及欲換的音
        public static string numtonotation(int realtune,int stair)
        {
            string a;//取得調式
            
            //以12為一個輪迴
            if (realtune>=12)
            {
                realtune=realtune%12;
            }
            if (realtune==0)
            {
                a= "C";
            }
            else if (realtune==1)
            {
                a= "C#";
            }
            else if (realtune==2)
            {
                a= "D";
            }
            else if (realtune==3)
            {
                a= "Eb";
            }
            else if (realtune==4)
            {
                a= "E";
            }
            else if (realtune==5)
            {
                a= "F";
            }
            else if (realtune==6)//
            {
                a= "F#";
            }
            else if (realtune==7)
            {
                a= "G";
            }
             else if (realtune==8)//
            {
                a= "Ab";
            }
            else if (realtune==9)
            {
                a= "A";
            }
            else if (realtune==10)//
            {
                a= "A#";
            }
            else if (realtune==11)
            {
                a= "B";
            }
            else
            {
                a= "x";
            }

            if (a.Length!=1)
            {
                a=a[0].ToString();
            }

            //用音階來換
            if (stair>=12)
            {
                stair=stair%12;
            }
            if (stair==0)
            {
                return "C";
            }
            else if (stair==1)
            {
                if (a=="A"||a=="F"||a=="D")
                {
                    return "C#";
                }
                else if (a=="B"||a=="G"||a=="E")
                {
                    return "Db";
                }
                else
                {
                    return "C#";
                }
            }
            else if (stair==2)
            {
                return "D";
            }
            else if (stair==3)
            {
                if (a=="G"||a=="B"||a=="E")
                {
                    return "D#";
                }
                else if (a=="C"||a=="A"||a=="F")
                {
                    return "Eb";
                }
                else
                {
                    return "D#";
                }
            }
            else if (stair==4)
            {
                return "E";
            }
            else if (stair==5)
            {
                return "F";
            }
            else if (stair==6)
            {
                if (a=="G"||a=="B"||a=="D")
                {
                    return "F#";
                }
                else if (a=="A"||a=="C"||a=="E")
                {
                    return "Gb";
                }
                else
                {
                    return "F#";
                }
            }
            else if (stair==7)
            {
                return "G";
            }
             else if (stair==8)
            {
                if (a=="A"||a=="C"||a=="E")
                {
                    return "G#";
                }
                else if (a=="B"||a=="D"||a=="F")
                {
                    return "Ab";
                }
                else
                {
                    return "G#";
                }
            }
            else if (stair==9)
            {
                return "A";
            }
            else if (stair==10)
            {
                if (a=="A"||a=="C"||a=="E")
                {
                    return "Bb";
                }
                else if (a=="B"||a=="D"||a=="F")
                {
                    return "A#";
                }
                else
                {
                    return "A#";
                }
            }
            else if (stair==11)
            {
                return "B";
            }
            else
            {
                return "x";
            }
        }


        public static int[] notationtonum(string tuning)
        {
            int[] tuningtonum=new int[tuning.Length];
            for (int i=0; i<tuning.Length;i++)
            {
                switch (tuning[i])
                {
                    case 'C':
                        tuningtonum[i]=0;
                        break;
                    case 'D':
                        tuningtonum[i]=2;
                        break;
                    case 'E':
                        tuningtonum[i]=4;
                        break;
                    case 'F':
                        tuningtonum[i]=5;
                        break;
                    case 'G':
                        tuningtonum[i]=7;
                        break;
                    case 'A':
                        tuningtonum[i]=9;
                        break;
                    case 'B':
                        tuningtonum[i]=11;
                        break;
                    default:
                        break;
                }
                if (i!=0)
                {
                    if (tuning[i-1]=='#')
                    {
                        tuningtonum[i]++;
                    }
                    if (tuning[i-1]=='b')
                    {
                        tuningtonum[i]--;
                    }
                }
            }

            int[] tuningtonumwithout0= new int[tuningtonum.Length];
            int count=0;
            for (int i =0;i<tuningtonum.Length;i++)
            {
                if (tuningtonum[i]==0)
                {
                    count++;
                }
                else
                {
                    tuningtonumwithout0[i-count]=tuningtonum[i];
                }
            }
            return tuningtonumwithout0;
        }
    }
}