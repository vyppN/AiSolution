using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

namespace AiLibraries.Legacy.Old
{
    public class MedMis
    {
        public static void csvToMysql(string filename)
        {
            //StreamReader sr = new StreamReader(@"C:\Users\vyppN\Documents\Visual Studio 2010\Projects\AiLibrary\TestLibrary\ผลตรวจกัลยา30มค55.csv");

            var sr = new StreamReader(filename);

            var stringList = new List<string>();
            var myconn = new MySqlConnection("SERVER=localhost;" + "DATABASE= med" + ";"
                                             + "UID=root" + ";" + "PASSWORD=; CHARSET=utf8");
            myconn.Open();
            while (!sr.EndOfStream)
            {
                string strline = sr.ReadLine();
                while (strline != "")
                {
                    stringList.Add(HelpMe.CutStringNoTrim(ref strline, ","));
                }
                if (stringList.Count < 54) stringList.Add("");
                for (int i = 0; i < 54; i++)
                {
                    if (stringList[i] == "")
                    {
                        stringList[i] = "0";
                    }
                }
                int gg = 0;
                foreach (string item in stringList)
                {
                    ("[" + gg + "] " + item).println();
                    gg++;
                }
                PrintHelp.end();
                //string sql = "INSERT INTO customer" + "(cus_id,initname,fname,lname,age)"
                //    + "VALUES (\"" + stringList[1] + "\","+"\"" + stringList[4] + "\"," + "\"" + stringList[5] + "\","
                //    + "'" + stringList[6] + "'," + "'" + stringList[7] + "');";

                //MySqlCommand command = new MySqlCommand(sql, myconn);

                //command.ExecuteNonQuery();
                stringList.Clear();
            }
            myconn.Close();
            sr.Close();
        }
    }
}

//string sql = "INSERT INTO lab" + "(lab_code,id,prefix,fname,lname,age,sugar1,"
//    + "bun,creat,uric,tchol,hdlc,ldl,trig,tp,alb,tbi,dbi,sgot,sgpt,aklphos,hb,hct,"
//    + "wbc1000,NEUTROPHIL,Lymphocyte,Monocyte,Eosinophil,basophil,atplymph,bandform,"
//    + "pltsmear,pltcount1000,mcv,mch,mchc,rbcmorpho,color,spgr,ph,protein,sugar2,ketone,"
//    + "blood,wbc,rbc,epithclial,bacteria,crystal,mucous,amorphor,cast) "
//    + "VALUES (\"" + stringList[0] + "\"," + "\"" + stringList[1] + "\"," + "\"" + stringList[2] + "\","
//    + "\"" + stringList[3] + "\"," + "\"" + stringList[4] + "\"," + "\"" + stringList[5] + "\","
//    + "'" + stringList[6] + "'," + "'" + stringList[7] + "'," + "'" + stringList[8] + "'," + "'" + stringList[9] + "',"
//    + "'" + stringList[10] + "'," + "'" + stringList[11] + "'," + "'" + stringList[12] + "'," + "'" + stringList[13] + "',"
//    + "'" + stringList[14] + "'," + "'" + stringList[15] + "'," + "'" + stringList[16] + "'," + "'" + stringList[17] + "',"
//    + "'" + stringList[18] + "'," + "'" + stringList[19] + "'," + "'" + stringList[20] + "'," + "'" + stringList[21] + "',"
//    + "'" + stringList[22] + "'," + "'" + stringList[23] + "'," + "'" + stringList[24] + "'," + "'" + stringList[25] + "',"
//    + "'" + stringList[26] + "'," + "'" + stringList[27] + "'," + "'" + stringList[28] + "'," + "'" + stringList[29] + "',"
//    + "'" + stringList[30] + "'," + "\"" + stringList[31] + "\"," + "'" + stringList[32] + "'," + "'" + stringList[33] + "',"
//    + "'" + stringList[34] + "'," + "'" + stringList[35] + "'," + "'" + stringList[36] + "'," + "\"" + stringList[37] + "\","
//    + "\"" + stringList[38] + "\"," + "'" + stringList[39] + "'," + "'" + stringList[40] + "'," + "\"" + stringList[41] + "\","
//    + "\"" + stringList[42] + "\"," + "\"" + stringList[43] + "\"," + "\"" + stringList[44] + "\"," + "\"" + stringList[45] + "\","
//    + "\"" + stringList[46] + "\"," + "\"" + stringList[47] + "\"," + "'" + stringList[48] + "'," + "'" + stringList[49] + "',"
//    + "'" + stringList[50] + "'," + "'" + stringList[51] + "');";