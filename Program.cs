using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;

namespace project
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
            Console.WriteLine("請輸入6空弦音 ");
            Console.WriteLine("若為standard,亦可直接輸入 yes ");
            string tuning=Console.ReadLine();
            //判斷輸入tuning是否正確
            if (tuning=="yes")
            {
                tuning="EADGBE";
            }

            
            Console.WriteLine("請從第六弦到第一弦依序輸入指型");
            Console.WriteLine("若不彈該弦，則以x取代");
            string k = Console.ReadLine();
            
            
            //判斷輸入指型是否為六位數
            if (k.Length!=6)
            {
                Console.WriteLine("請輸入六位數的正確指型");
                k=Console.ReadLine();
            }
            //處理不彈的弦
            int[] seeifnull={0,0,0,0,0,0}; //若為1→該弦不彈
            int countofx=0;
            for (int i=0;i<6;i++)
            {
                if (k[i]=='x')
                {
                    seeifnull[i]=1;
                    countofx++;
                }
                else
                {
                    seeifnull[i]=0;
                }
            }

            
            //做效果
            Console.WriteLine("輸入無誤，準備開始轉換");
            //Thread.Sleep(1000);

            
            //令六條弦的音高為剛剛輸入之空弦
            int[] tuningtonum=transformer.notationtonum(tuning);
            

            //輸入的指型
            int[] targetnote=new int[6];
            if (countofx==0)
            {
                for (int i=0; i<6;i++)
                {
                    targetnote[i]=(int)Char.GetNumericValue(k[i]);
                }
            }
            else
            {
                for (int i=0;i<6;i++)
                {
                    if (seeifnull[i]==0)
                    {
                        targetnote[i]=(int)Char.GetNumericValue(k[i]);
                    }
                    else
                    {
                        targetnote[i]=-1;
                    }
                }
            }

            //建立六條弦的物件
            Steel steel_6=new Steel(targetnote[0],tuningtonum[0]);
            Steel steel_5=new Steel(targetnote[1],tuningtonum[1]);
            Steel steel_4=new Steel(targetnote[2],tuningtonum[2]);
            Steel steel_3=new Steel(targetnote[3],tuningtonum[3]);
            Steel steel_2=new Steel(targetnote[4],tuningtonum[4]);
            Steel steel_1=new Steel(targetnote[5],tuningtonum[5]);
            

            //找根音
            int root=chordteller.whatroot(steel_6.realtune,steel_5.realtune,steel_4.realtune,
            steel_3.realtune,steel_2.realtune,steel_1.realtune,tuningtonum,targetnote);
            string Root=transformer.numtonotation(root,root);
           

            //刪除重複，並做成array
            List<int> notelist=new List<int>{steel_6.realtune,steel_5.realtune,steel_4.realtune,
            steel_3.realtune,steel_2.realtune,steel_1.realtune};

            for(int i=0;i<notelist.Count;i++)
            {
                for (int j=notelist.Count-1;j>i;j--)
                {
                    if (notelist[i]==notelist[j])
                    {
                        notelist.RemoveAt(j);
                    }
                }
            }
            for(int i=0;i<notelist.Count;i++)
            {
                if(notelist[i]==-1)
                {
                    notelist.RemoveAt(i);
                }
            }
            int[] notearray=notelist.ToArray();


            //讓數字都<12
            for (int i=0;i<notearray.Length;i++)
            {
                if(notearray[i]>=12)
                {
                    notearray[i]=notearray[i]%12;
                }
            }
            //算跟根音的距離
            for (int i=0;i<notearray.Length;i++)
            {
                notearray[i]-=root;
                if(notearray[i]<0)
                {
                    notearray[i]+=12;
                }
            }
            Array.Sort(notearray);


            //和弦函式庫

            
            //大三和弦
            chorddata majorthirdchord=new chorddata(Root);
            chordteller.distinguish(notearray,majorthirdchord.isthirdchord(0,4,7),Root,"major和弦");

            //小三和弦
            chorddata minorthirdchord=new chorddata(Root);
            chordteller.distinguish(notearray,minorthirdchord.isthirdchord(0,3,7),Root,"minor和弦");

            //增三和弦
            chorddata augthirdchord=new chorddata(Root);
            chordteller.distinguish(notearray,augthirdchord.isthirdchord(0,4,8),Root,"aug和弦");

            //dim三和弦
            chorddata dimthirdchord=new chorddata(Root);
            chordteller.distinguish(notearray,dimthirdchord.isthirdchord(0,3,6),Root,"dim和弦");

            //增二三和弦
            chorddata sus2thirdchord=new chorddata(Root);
            chordteller.distinguish(notearray,sus2thirdchord.isthirdchord(0,2,7),Root,"sus2和弦");

            //增四三和弦
            chorddata sus4thirdchord=new chorddata(Root);
            chordteller.distinguish(notearray,sus4thirdchord.isthirdchord(0,5,7),Root,"sus4和弦");

            //7和弦
            chorddata seventhchord=new chorddata(Root);
            chordteller.distinguish(notearray,seventhchord.isthirdchord(0,4,10),Root,"7和弦");
            chordteller.distinguish(notearray,seventhchord.isseventhchord(0,4,7,10),Root,"7和弦");

            //小7和弦
            chorddata minorseventhchord=new chorddata(Root);
            chordteller.distinguish(notearray,minorseventhchord.isthirdchord(0,3,10),Root,"minor7和弦");
            chordteller.distinguish(notearray,minorseventhchord.isseventhchord(0,3,7,10),Root,"minor7和弦");

            //7sus2和弦
            chorddata seventhsus2chord=new chorddata(Root);
            chordteller.distinguish(notearray,seventhsus2chord.isthirdchord(0,2,10),Root,"7sus2和弦");
            chordteller.distinguish(notearray,seventhsus2chord.isseventhchord(0,2,7,10),Root,"7sus2和弦");

            //7sus4和弦
            chorddata seventhsus4chord=new chorddata(Root);
            chordteller.distinguish(notearray,seventhsus4chord.isthirdchord(0,5,10),Root,"7sus4和弦");
            chordteller.distinguish(notearray,seventhsus4chord.isseventhchord(0,5,7,10),Root,"7sus4和弦");
            
            //aug7和弦
            chorddata augseventhchord=new chorddata(Root);
            chordteller.distinguish(notearray,augseventhchord.isseventhchord(0,4,8,11),Root,"aug maj7和弦");

            //minor aug7和弦
            chorddata minoraugseventhchord=new chorddata(Root);
            chordteller.distinguish(notearray,minoraugseventhchord.isseventhchord(0,3,8,11),Root,"m maj7(#5)和弦");

            //dim7和弦
            chorddata dimseventhchord=new chorddata(Root);
            chordteller.distinguish(notearray,augseventhchord.isseventhchord(0,3,6,9),Root,"dim7和弦");

            //major major7和弦
            chorddata majormajorseventhchord=new chorddata(Root);
            chordteller.distinguish(notearray,majormajorseventhchord.isthirdchord(0,4,11),Root,"major7和弦");
            chordteller.distinguish(notearray,majormajorseventhchord.isseventhchord(0,4,7,11),Root,"major7和弦");

            //minor major7和弦
            chorddata minormajorseventhchord=new chorddata(Root);
            chordteller.distinguish(notearray,minormajorseventhchord.isthirdchord(0,3,11),Root,"m7和弦");
            chordteller.distinguish(notearray,minormajorseventhchord.isseventhchord(0,3,7,11),Root,"m7和弦");

            //sus2 major7和弦
            chorddata sus2majorseventhchord=new chorddata(Root);
            chordteller.distinguish(notearray,sus2majorseventhchord.isthirdchord(0,2,11),Root,"major7sus2和弦");
            chordteller.distinguish(notearray,sus2majorseventhchord.isseventhchord(0,2,7,11),Root,"major7sus2和弦");

            //sus4 major7和弦
            chorddata sus4majorseventhchord=new chorddata(Root);
            chordteller.distinguish(notearray,sus4majorseventhchord.isthirdchord(0,5,11),Root,"major7sus4和弦");
            chordteller.distinguish(notearray,sus4majorseventhchord.isseventhchord(0,5,7,11),Root,"major7sus4和弦");

            //major 6和弦
            chorddata major6chord=new chorddata(Root);
            chordteller.distinguish(notearray,major6chord.isthirdchord(0,4,9),Root,"6和弦");
            chordteller.distinguish(notearray,major6chord.isseventhchord(0,4,7,9),Root,"6和弦");

            //minor 6和弦
            chorddata minor6chord=new chorddata(Root);
            chordteller.distinguish(notearray,major6chord.isthirdchord(0,3,9),Root,"m6和弦");
            chordteller.distinguish(notearray,major6chord.isseventhchord(0,3,7,9),Root,"m6和弦");

            //major 6sus2和弦
            chorddata major6sus2chord=new chorddata(Root);
            chordteller.distinguish(notearray,major6sus2chord.isthirdchord(0,2,9),Root,"6sus2和弦");
            chordteller.distinguish(notearray,major6sus2chord.isseventhchord(0,2,7,9),Root,"6sus2和弦");

            //major 6sus4和弦
            chorddata major6sus4chord=new chorddata(Root);
            chordteller.distinguish(notearray,major6sus4chord.isthirdchord(0,5,9),Root,"6sus4和弦");
            chordteller.distinguish(notearray,major6sus4chord.isseventhchord(0,5,7,9),Root,"6sus4和弦");

            //major 9和弦
            chorddata major9chord=new chorddata(Root);
            chordteller.distinguish(notearray,major9chord.isseventhchord(0,2,4,10),Root,"9和弦");
            chordteller.distinguish(notearray,major9chord.isninthchord(0,2,4,7,10),Root,"9和弦");

            //minor 9和弦
            chorddata minor9chord=new chorddata(Root);
            chordteller.distinguish(notearray,minor9chord.isthirdchord(0,2,10),Root,"m9和弦");
            chordteller.distinguish(notearray,minor9chord.isseventhchord(0,2,5,10),Root,"m9和弦");

            //major 9sus4和弦
            chorddata major9sus4chord=new chorddata(Root);
            chordteller.distinguish(notearray,major9sus4chord.isseventhchord(0,2,5,10),Root,"9sus4和弦");
            chordteller.distinguish(notearray,major9sus4chord.isninthchord(0,2,5,7,10),Root,"9sus4和弦");

            //major 9sus2和弦
            chorddata major9sus2chord=new chorddata(Root);
            chordteller.distinguish(notearray,major9sus2chord.isseventhchord(0,2,4,10),Root,"9sus2和弦");
            chordteller.distinguish(notearray,major9sus2chord.isninthchord(0,2,4,7,10),Root,"9sus2和弦");

            //aug major 9和弦
            chorddata augmajor9chord=new chorddata(Root);
            chordteller.distinguish(notearray,augmajor9chord.isninthchord(0,2,4,8,11),Root,"augmaj9和弦");

            //minor aug major 9和弦
            chorddata maugmajor9chord=new chorddata(Root);
            chordteller.distinguish(notearray,maugmajor9chord.isninthchord(0,2,3,8,11),Root,"m maj9(#5)和弦");

            //dim 9和弦
            chorddata dim9chord=new chorddata(Root);
            chordteller.distinguish(notearray,dim9chord.isninthchord(0,2,3,6,9),Root,"dim7add9和弦");

            //major b9和弦
            chorddata majorb9chord=new chorddata(Root);
            chordteller.distinguish(notearray,majorb9chord.isseventhchord(0,1,4,10),Root,"7(b9)和弦");
            chordteller.distinguish(notearray,majorb9chord.isninthchord(0,1,4,7,10),Root,"7(b9)和弦");

            //minor b9和弦
            chorddata minorb9chord=new chorddata(Root);
            chordteller.distinguish(notearray,minorb9chord.isseventhchord(0,1,3,10),Root,"m7(b9)和弦");
            chordteller.distinguish(notearray,minorb9chord.isninthchord(0,1,3,7,10),Root,"m7(b9)和弦");

            //b9sus2和弦
            chorddata b9sus2chord=new chorddata(Root);
            chordteller.distinguish(notearray,b9sus2chord.isseventhchord(0,1,2,10),Root,"7sus2(b9)和弦");
            chordteller.distinguish(notearray,b9sus2chord.isninthchord(0,1,2,7,10),Root,"7sus2(b9)和弦");

            //b9sus4和弦
            chorddata b9sus4chord=new chorddata(Root);
            chordteller.distinguish(notearray,b9sus4chord.isseventhchord(0,1,5,10),Root,"7sus4(b9)和弦");
            chordteller.distinguish(notearray,b9sus4chord.isninthchord(0,1,5,7,10),Root,"7sus4(b9)和弦");

            //aug b9和弦
            chorddata augb9chord=new chorddata(Root);
            chordteller.distinguish(notearray,augb9chord.isninthchord(0,1,4,8,11),Root,"aug maj7(b9)和弦");

            //maug b9和弦
            chorddata maugb9chord=new chorddata(Root);
            chordteller.distinguish(notearray,maugb9chord.isninthchord(0,1,3,8,11),Root,"m maj7(#5,b9)和弦");

            //dim b9和弦
            chorddata dimb9chord=new chorddata(Root);
            chordteller.distinguish(notearray,dimb9chord.isninthchord(0,1,3,6,9),Root,"dim7addb9和弦");

            //up9sus2和弦
            chorddata up9sus2chord=new chorddata(Root);
            chordteller.distinguish(notearray,up9sus2chord.isseventhchord(0,2,3,10),Root,"7sus2(#9)和弦");
            chordteller.distinguish(notearray,b9sus2chord.isninthchord(0,2,3,7,10),Root,"7sus2(#9)和弦");

            //up9sus4和弦
            chorddata up9sus4chord=new chorddata(Root);
            chordteller.distinguish(notearray,up9sus4chord.isseventhchord(0,2,3,10),Root,"7sus4(#9)和弦");
            chordteller.distinguish(notearray,up9sus4chord.isninthchord(0,2,3,7,10),Root,"7sus4(#9)和弦");

            //aug #9和弦
            chorddata augup9chord=new chorddata(Root);
            chordteller.distinguish(notearray,augup9chord.isninthchord(0,3,4,8,11),Root,"aug maj7(#9)和弦");

            //maug #9和弦
            chorddata maugup9chord=new chorddata(Root);
            chordteller.distinguish(notearray,maugup9chord.isseventhchord(0,3,8,11),Root,"m maj7(#5,#9)和弦");

            //dim #9和弦
            chorddata dimup9chord=new chorddata(Root);
            chordteller.distinguish(notearray,dimup9chord.isseventhchord(0,3,6,9),Root,"dim7add#9和弦");



            //major #9和弦
            chorddata majorup9chord=new chorddata(Root);
            chordteller.distinguish(notearray,majorup9chord.isseventhchord(0,3,4,10),Root,"7(#9)和弦");
            chordteller.distinguish(notearray,majorup9chord.isninthchord(0,3,4,7,10),Root,"7(#9)和弦");

            //minor #9和弦=minor7
            chorddata minorup9chord=new chorddata(Root);
            chordteller.distinguish(notearray,minorup9chord.isthirdchord(0,3,10),Root,"m7(#9)和弦");
            chordteller.distinguish(notearray,minorup9chord.isseventhchord(0,3,7,10),Root,"m7(#9)和弦");



            //11和弦
            chorddata major11chord=new chorddata(Root);
            chordteller.distinguish(notearray,major11chord.isseventhchord(0,4,5,10),Root,"11和弦");
            chordteller.distinguish(notearray,major11chord.isninthchord(0,4,5,7,10),Root,"11和弦");
            chordteller.distinguish(notearray,major11chord.iseleventhchord(0,2,4,5,7,10),Root,"11和弦");
            chordteller.distinguish(notearray,major11chord.isninthchord(0,2,4,5,10),Root,"11和弦");

            //minor 11和弦
            chorddata minor11chord=new chorddata(Root);
            chordteller.distinguish(notearray,minor11chord.isseventhchord(0,3,5,10),Root,"m11和弦");
            chordteller.distinguish(notearray,minor11chord.isninthchord(0,3,5,7,10),Root,"m11和弦");
            chordteller.distinguish(notearray,minor11chord.iseleventhchord(0,2,3,5,7,10),Root,"m11和弦");
            chordteller.distinguish(notearray,minor11chord.isninthchord(0,2,3,5,10),Root,"m11和弦");

            //11sus2和弦
            chorddata major11sus2chord=new chorddata(Root);
            chordteller.distinguish(notearray,major11sus2chord.isseventhchord(0,2,5,10),Root,"11sus2和弦");
            chordteller.distinguish(notearray,major11sus2chord.isninthchord(0,2,5,7,10),Root,"11sus2和弦");

            //11sus4和弦
            chorddata major11sus4chord=new chorddata(Root);
            chordteller.distinguish(notearray,major11sus4chord.isthirdchord(0,5,10),Root,"11sus4和弦");
            chordteller.distinguish(notearray,major11sus4chord.isninthchord(0,2,5,7,10),Root,"11sus4和弦");
            chordteller.distinguish(notearray,major11sus4chord.isseventhchord(0,2,5,10),Root,"11sus4和弦");
            chordteller.distinguish(notearray,major11sus4chord.isseventhchord(0,5,7,10),Root,"11sus4和弦");

            //aug major 11和弦
            chorddata augmajor11chord=new chorddata(Root);
            chordteller.distinguish(notearray,augmajor11chord.iseleventhchord(0,2,4,5,8,11),Root,"augmaj11和弦");

            //maug major 11和弦
            chorddata maugmajor11chord=new chorddata(Root);
            chordteller.distinguish(notearray,maugmajor11chord.iseleventhchord(0,2,3,5,8,11),Root,"m maj11(#5)和弦");
            chordteller.distinguish(notearray,maugmajor11chord.isninthchord(0,3,5,8,11),Root,"m maj11(#5)和弦");

            //dim 11和弦
            chorddata dim11chord=new chorddata(Root);
            chordteller.distinguish(notearray,dim11chord.iseleventhchord(0,2,3,5,6,9),Root,"dim7add9,11和弦");
            chordteller.distinguish(notearray,dim11chord.isninthchord(0,3,5,6,9),Root,"dim7add9,11和弦");

            //dim 11b9和弦
            chorddata dim11b9chord=new chorddata(Root);
            chordteller.distinguish(notearray,dim11b9chord.iseleventhchord(0,1,3,5,6,9),Root,"dim7addb9,11和弦");

            //dim 11up9和弦
            chorddata dim11up9chord=new chorddata(Root);
            chordteller.distinguish(notearray,dim11up9chord.isninthchord(0,3,5,6,9),Root,"dim7add#9,11和弦");

            //dim 11b和弦
            chorddata dim11bchord=new chorddata(Root);
            chordteller.distinguish(notearray,dim11bchord.iseleventhchord(0,2,3,4,6,9),Root,"dim7add9,b11和弦");
            chordteller.distinguish(notearray,dim11bchord.isninthchord(0,3,4,6,9),Root,"dim7add9,b11和弦");

            //dim 11b9b和弦
            chorddata dim11b9bchord=new chorddata(Root);
            chordteller.distinguish(notearray,dim11b9bchord.iseleventhchord(0,1,3,4,6,9),Root,"dim7addb9,b11和弦");

            //dim 11b9#和弦
            chorddata dim11b9upchord=new chorddata(Root);
            chordteller.distinguish(notearray,dim11b9upchord.isninthchord(0,3,4,6,9),Root,"dim7add#9,b11和弦");

            //dim #11和弦
            chorddata dim11upchord=new chorddata(Root);
            chordteller.distinguish(notearray,dim11upchord.isninthchord(0,2,3,6,9),Root,"dim7add9,#11和弦");
            chordteller.distinguish(notearray,dim11upchord.isseventhchord(0,3,6,9),Root,"dim7add9,#11和弦");

            //dim #11b9和弦
            chorddata dim11up9bchord=new chorddata(Root);
            chordteller.distinguish(notearray,dim11up9bchord.isninthchord(0,1,3,6,9),Root,"dim7add9,#11和弦");

            //dim #11#9和弦
            chorddata dim11up9upchord=new chorddata(Root);
            chordteller.distinguish(notearray,dim11up9upchord.isseventhchord(0,3,6,9),Root,"dim7add#9,#11和弦");
            
            //b11sus2和弦
            chorddata majorb11sus2chord=new chorddata(Root);
            chordteller.distinguish(notearray,majorb11sus2chord.isninthchord(0,2,4,7,10),Root,"9sus2(b11)和弦");
            chordteller.distinguish(notearray,majorb11sus2chord.isseventhchord(0,2,4,10),Root,"9sus2(b11)和弦");

            //b11sus4和弦
            chorddata majorb11sus4chord=new chorddata(Root);
            chordteller.distinguish(notearray,majorb11sus4chord.isninthchord(0,4,5,7,10),Root,"9sus4(b11)和弦");
            chordteller.distinguish(notearray,majorb11sus4chord.isseventhchord(0,4,5,10),Root,"9sus4(b11)和弦");
            chordteller.distinguish(notearray,majorb11sus4chord.isninthchord(0,2,4,5,10),Root,"9sus4(b11)和弦");
            chordteller.distinguish(notearray,majorb11sus4chord.iseleventhchord(0,2,4,5,7,10),Root,"9sus4(b11)和弦");

            //aug major b11和弦
            chorddata augmajorb11chord=new chorddata(Root);
            chordteller.distinguish(notearray,augmajorb11chord.isseventhchord(0,4,8,11),Root,"augmaj9(b11)和弦");
            chordteller.distinguish(notearray,augmajorb11chord.isninthchord(0,2,4,8,11),Root,"augmaj9(b11)和弦");

            //aug minor b11和弦
            chorddata augminorb11chord=new chorddata(Root);
            chordteller.distinguish(notearray,augminorb11chord.isninthchord(0,3,4,8,11),Root,"augmaj9(#5,b11)和弦");
            chordteller.distinguish(notearray,augminorb11chord.iseleventhchord(0,2,3,4,8,11),Root,"augmaj9(#5,b11)和弦");

            //9(b11)
            chorddata major9b11chord=new chorddata(Root);
            chordteller.distinguish(notearray,major9b11chord.isthirdchord(0,4,10),Root,"9(b11)");
            chordteller.distinguish(notearray,major9b11chord.isseventhchord(0,2,4,10),Root,"9(b11)和弦");
            chordteller.distinguish(notearray,major9b11chord.isninthchord(0,2,4,7,10),Root,"9(b11)和弦");

            //minor9(b11)
            chorddata minor9b11chord=new chorddata(Root);
            chordteller.distinguish(notearray,minor9b11chord.isseventhchord(0,3,4,10),Root,"m9(b11)和弦");
            chordteller.distinguish(notearray,minor9b11chord.isninthchord(0,3,4,7,10),Root,"m9(b11)和弦");
            chordteller.distinguish(notearray,minor9b11chord.iseleventhchord(0,2,3,4,7,10),Root,"m9(b11)和弦");
            chordteller.distinguish(notearray,minor9b11chord.isninthchord(0,2,3,4,10),Root,"m9(b11)和弦");

            //9(#11)
            chorddata major9up11chord=new chorddata(Root);
            chordteller.distinguish(notearray,major9up11chord.isseventhchord(0,4,6,10),Root,"9(#11)和弦");
            chordteller.distinguish(notearray,major9up11chord.isninthchord(0,4,6,7,10),Root,"9(#11)和弦");
            chordteller.distinguish(notearray,major9up11chord.iseleventhchord(0,2,4,6,7,10),Root,"9(#11)和弦");
            chordteller.distinguish(notearray,major9up11chord.isninthchord(0,2,4,6,10),Root,"9(#11)和弦");

            //minor9(#11)
            chorddata minor9up11chord=new chorddata(Root);
            chordteller.distinguish(notearray,minor9up11chord.isseventhchord(0,3,6,10),Root,"9(#11)和弦");
            chordteller.distinguish(notearray,minor9up11chord.isninthchord(0,3,6,7,10),Root,"9(#11)和弦");
            chordteller.distinguish(notearray,minor9up11chord.iseleventhchord(0,2,3,6,7,10),Root,"9(#11)和弦");
            chordteller.distinguish(notearray,minor9up11chord.isninthchord(0,2,3,6,10),Root,"9(#11)和弦");


            //9(#11)sus2
            chorddata major9up11sus2chord=new chorddata(Root);
            chordteller.distinguish(notearray,major9up11sus2chord.isseventhchord(0,2,6,10),Root,"9sus2(#11)和弦");
            chordteller.distinguish(notearray,major9up11sus2chord.isninthchord(0,2,6,7,10),Root,"9sus2(#11)和弦");

            //9(#11)sus4
            chorddata major9up11sus4chord=new chorddata(Root);
            chordteller.distinguish(notearray,major9up11sus4chord.isseventhchord(0,5,6,10),Root,"9sus4(#11)和弦");
            chordteller.distinguish(notearray,major9up11sus4chord.isninthchord(0,5,6,7,10),Root,"9sus4(#11)和弦");
            chordteller.distinguish(notearray,major9up11sus4chord.iseleventhchord(0,2,5,6,7,10),Root,"9sus4(#11)和弦");
            chordteller.distinguish(notearray,major9up11sus4chord.isninthchord(0,2,5,6,10),Root,"9sus4(#11)和弦");

            //aug major #11和弦
            chorddata augmajup11chord=new chorddata(Root);
            chordteller.distinguish(notearray,augmajup11chord.isninthchord(0,4,6,8,11),Root,"augmaj9(#11)和弦");
            chordteller.distinguish(notearray,augmajup11chord.iseleventhchord(0,2,4,6,8,11),Root,"augmaj9(#11)和弦");

            //aug major #11b9和弦
            chorddata augmajup11b9chord=new chorddata(Root);
            chordteller.distinguish(notearray,augmajup11b9chord.iseleventhchord(0,1,4,6,8,11),Root,"augmaj9(b9,#11)和弦");

            //aug major #11#9和弦
            chorddata augmajup11up9chord=new chorddata(Root);
            chordteller.distinguish(notearray,augmajup11up9chord.iseleventhchord(0,3,4,6,8,11),Root,"augmaj9(#9,#11)和弦");

            //aug minor #11和弦
            chorddata maugup11chord=new chorddata(Root);
            chordteller.distinguish(notearray,maugup11chord.isninthchord(0,3,6,8,11),Root,"m maj9(#5,#11)和弦");
            chordteller.distinguish(notearray,maugup11chord.iseleventhchord(0,2,3,6,8,11),Root,"m maj9(#5,#11)和弦");


            //aug minor #11b9和弦
            chorddata maugup11b9chord=new chorddata(Root);
            chordteller.distinguish(notearray,maugup11b9chord.iseleventhchord(0,1,3,6,8,11),Root,"m maj9(#5,b9,#11)和弦");

            //aug minor #11#9和弦
            chorddata maugup11up9chord=new chorddata(Root);
            chordteller.distinguish(notearray,maugup11up9chord.isninthchord(0,3,6,8,11),Root,"m maj9(#5,#9,#11)和弦");

            //13和弦
            chorddata major13chord=new chorddata(Root);
            chordteller.distinguish(notearray,major13chord.isseventhchord(0,4,9,10),Root,"13和弦");
            chordteller.distinguish(notearray,major13chord.isninthchord(0,4,7,9,10),Root,"13和弦");
            chordteller.distinguish(notearray,major13chord.isninthchord(0,2,4,9,10),Root,"13和弦");
            chordteller.distinguish(notearray,major13chord.isninthchord(0,4,5,9,10),Root,"13和弦");
            chordteller.distinguish(notearray,major13chord.iseleventhchord(0,2,4,7,9,10),Root,"13和弦");
            chordteller.distinguish(notearray,major13chord.iseleventhchord(0,2,4,5,9,10),Root,"13和弦");
            chordteller.distinguish(notearray,major13chord.iseleventhchord(0,4,5,7,9,10),Root,"13和弦");
            chordteller.distinguish(notearray,major13chord.isthirteenthchord(0,2,4,5,7,9,10),Root,"13和弦");

            //m13和弦
            chorddata minor13chord=new chorddata(Root);
            chordteller.distinguish(notearray,minor13chord.isseventhchord(0,3,9,10),Root,"m13和弦");
            chordteller.distinguish(notearray,minor13chord.isninthchord(0,3,7,9,10),Root,"m13和弦");
            chordteller.distinguish(notearray,minor13chord.isninthchord(0,2,3,9,10),Root,"m13和弦");
            chordteller.distinguish(notearray,minor13chord.isninthchord(0,3,5,9,10),Root,"m13和弦");
            chordteller.distinguish(notearray,minor13chord.iseleventhchord(0,2,3,7,9,10),Root,"m13和弦");
            chordteller.distinguish(notearray,minor13chord.iseleventhchord(0,2,3,5,9,10),Root,"m13和弦");
            chordteller.distinguish(notearray,minor13chord.iseleventhchord(0,3,5,7,9,10),Root,"m13和弦");
            chordteller.distinguish(notearray,minor13chord.isthirteenthchord(0,2,3,5,7,9,10),Root,"m13和弦");

            //13sus2和弦
            chorddata m13sus2chord=new chorddata(Root);
            chordteller.distinguish(notearray,m13sus2chord.isseventhchord(0,2,9,10),Root,"13sus2和弦");
            chordteller.distinguish(notearray,m13sus2chord.isninthchord(0,2,5,9,10),Root,"13sus2和弦");
            chordteller.distinguish(notearray,m13sus2chord.iseleventhchord(0,2,5,7,9,10),Root,"13sus2和弦");
            chordteller.distinguish(notearray,m13sus2chord.isninthchord(0,2,7,9,10),Root,"13sus2和弦");

            //13sus4和弦
            chorddata m13sus4chord=new chorddata(Root);
            chordteller.distinguish(notearray,m13sus4chord.isseventhchord(0,5,9,10),Root,"13sus4和弦");
            chordteller.distinguish(notearray,m13sus4chord.isninthchord(0,5,7,9,10),Root,"13sus4和弦");
            chordteller.distinguish(notearray,m13sus4chord.iseleventhchord(0,2,5,7,9,10),Root,"13sus4和弦");
            chordteller.distinguish(notearray,m13sus4chord.isninthchord(0,2,5,9,10),Root,"13sus4和弦");

            //aug13和弦
            chorddata aug13chord=new chorddata(Root);
            chordteller.distinguish(notearray,aug13chord.isseventhchord(0,4,9,11),Root,"aug maj13和弦");
            chordteller.distinguish(notearray,aug13chord.isninthchord(0,4,8,9,11),Root,"aug maj13和弦");
            chordteller.distinguish(notearray,aug13chord.isninthchord(0,2,4,9,11),Root,"aug maj13和弦");
            chordteller.distinguish(notearray,aug13chord.isninthchord(0,4,5,9,11),Root,"aug maj13和弦");
            chordteller.distinguish(notearray,aug13chord.iseleventhchord(0,2,4,8,9,11),Root,"aug maj13和弦");
            chordteller.distinguish(notearray,aug13chord.iseleventhchord(0,2,4,5,9,11),Root,"aug maj13和弦");
            chordteller.distinguish(notearray,aug13chord.iseleventhchord(0,4,5,8,9,11),Root,"aug maj13和弦");
            chordteller.distinguish(notearray,aug13chord.isthirteenthchord(0,2,4,5,8,9,11),Root,"aug maj13和弦");

            //minoraug13和弦
            chorddata maug13chord=new chorddata(Root);
            chordteller.distinguish(notearray,maug13chord.isninthchord(0,3,8,9,11),Root,"m maj13(#5)和弦");
            chordteller.distinguish(notearray,maug13chord.iseleventhchord(0,2,3,8,9,11),Root,"aug mjor13和弦");
            chordteller.distinguish(notearray,maug13chord.iseleventhchord(0,3,5,8,9,11),Root,"aug major13和弦");
            chordteller.distinguish(notearray,maug13chord.isthirteenthchord(0,2,3,5,8,9,11),Root,"aug major13和弦");

            //dim13和弦
            chorddata dim13chord=new chorddata(Root);
            chordteller.distinguish(notearray,dim13chord.isseventhchord(0,3,6,9),Root,"dim7add9,11,13和弦");
            chordteller.distinguish(notearray,dim13chord.isninthchord(0,2,3,6,9),Root,"dim7add9,11,13和弦");
            chordteller.distinguish(notearray,dim13chord.isninthchord(0,3,5,6,9),Root,"dim7add9,11,13和弦");
            chordteller.distinguish(notearray,dim13chord.iseleventhchord(0,2,3,5,6,9),Root,"dim7add9,11,13和弦");
            


            //b13和弦
            chorddata majorb13chord=new chorddata(Root);
            chordteller.distinguish(notearray,majorb13chord.isseventhchord(0,4,8,10),Root,"11(b13)和弦");
            chordteller.distinguish(notearray,majorb13chord.isninthchord(0,4,7,8,10),Root,"11(b13)和弦");
            chordteller.distinguish(notearray,majorb13chord.isninthchord(0,2,4,8,10),Root,"11(b13)和弦");
            chordteller.distinguish(notearray,majorb13chord.isninthchord(0,4,5,8,10),Root,"11(b13)和弦");
            chordteller.distinguish(notearray,majorb13chord.iseleventhchord(0,2,4,7,8,10),Root,"11(b13)和弦");
            chordteller.distinguish(notearray,majorb13chord.iseleventhchord(0,2,4,5,8,10),Root,"11(b13)和弦");
            chordteller.distinguish(notearray,majorb13chord.iseleventhchord(0,4,5,7,8,10),Root,"11(b13)和弦");
            chordteller.distinguish(notearray,majorb13chord.isthirteenthchord(0,2,4,5,7,8,10),Root,"11(b13)和弦");

            //mb13和弦
            chorddata minorb13chord=new chorddata(Root);
            chordteller.distinguish(notearray,minorb13chord.isseventhchord(0,3,8,10),Root,"m11(b13)和弦");
            chordteller.distinguish(notearray,minorb13chord.isninthchord(0,3,7,8,10),Root,"m11(b13)和弦");
            chordteller.distinguish(notearray,minorb13chord.isninthchord(0,2,3,8,10),Root,"m11(b13)和弦");
            chordteller.distinguish(notearray,minorb13chord.isninthchord(0,3,5,8,10),Root,"m11(b13)和弦");
            chordteller.distinguish(notearray,minorb13chord.iseleventhchord(0,2,3,7,8,10),Root,"m11(b13)和弦");
            chordteller.distinguish(notearray,minorb13chord.iseleventhchord(0,2,3,5,8,10),Root,"m11(b13)和弦");
            chordteller.distinguish(notearray,minorb13chord.iseleventhchord(0,3,5,7,8,10),Root,"m11(b13)和弦");
            chordteller.distinguish(notearray,minorb13chord.isthirteenthchord(0,2,3,5,7,8,10),Root,"m11(b13)和弦");

            //#13和弦
            chorddata majorup13chord=new chorddata(Root);
            chordteller.distinguish(notearray,majorup13chord.isthirdchord(0,4,10),Root,"11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isseventhchord(0,4,7,10),Root,"11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isseventhchord(0,2,4,10),Root,"11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isseventhchord(0,4,5,10),Root,"11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.iseleventhchord(0,2,4,7,5,10),Root,"11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isninthchord(0,2,4,5,10),Root,"11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isninthchord(0,4,5,7,10),Root,"11(#13)和弦");

            //mup13和弦
            chorddata minorup13chord=new chorddata(Root);
            chordteller.distinguish(notearray,majorup13chord.isthirdchord(0,3,10),Root,"m11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isseventhchord(0,3,7,10),Root,"m11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isseventhchord(0,2,3,10),Root,"m11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isseventhchord(0,3,5,10),Root,"m11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.iseleventhchord(0,2,3,7,5,10),Root,"m11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isninthchord(0,2,3,5,10),Root,"m11(#13)和弦");
            chordteller.distinguish(notearray,majorup13chord.isninthchord(0,3,5,7,10),Root,"m11(#13)和弦");
           






            
            
            
            Console.ReadLine();
            
          
            }
        }
        
    }
}