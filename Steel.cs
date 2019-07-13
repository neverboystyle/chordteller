using System;

namespace project
{
    
    class Steel
    {
        private int Note;
        public Steel()
        {
            Note=0;
        }

        //各弦建構式
        public Steel(int targetnote,int tuning)
        {
            if (targetnote!=-1)
            {
                Note=targetnote+tuning;
            }
            else
            {
                Note=-1;
            }

            if (Note>12)
            {
                Note=Note%12;
            }
        }
        //各弦空弦+指法的音高(數字)
        public int realtune
        {
            get{ return Note;}
        }
    }
}