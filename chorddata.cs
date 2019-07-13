using System;

namespace project
{
    class chorddata
    {
        public string chordname;


        //三和弦
        public chorddata(string Root)
        {
            chordname=Root;
        }

        public int[] isthirdchord(int first,int sec,int third)
        {
            int[] newarray={first,sec,third};
            return newarray;
        }
        public int[] isseventhchord(int first,int sec,int third,int fourth)
        {
            int[] newarray={first,sec,third,fourth};
            return newarray;
        }
        public int[] isninthchord(int first,int sec,int third,int fourth,int fifth)
        {
            int[] newarray={first,sec,third,fourth,fifth};
            return newarray;
        }
        public int[] iseleventhchord(int first,int sec,int third,int fourth,int fifth,int sixth)
        {
            int[] newarray={first,sec,third,fourth,fifth,sixth};
            return newarray;
        }
        public int[] isthirteenthchord(int first,int sec,int third,int fourth,int fifth,int sixth,int seventh)
        {
            int[] newarray={first,sec,third,fourth,fifth,sixth,seventh};
            return newarray;
        }
    }
}