using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class clsUlkeKeyword
    {
    //CovidTableData alanları(field) burada eşleşenleri listeye alındı.
        public string Keyword { get; set; }
        public List<string> KeywordEtiket { get; set; }
        public static bool bDurumMinKontrol { get; set; }
        public static bool bDurumMaxKontrol { get; set; }
    public clsUlkeKeyword()
    {
        KeywordEtiket = new List<string>();
    }

    internal static string KeywordGeciyorMu(string sParagraf)
    {
        string sKeyword = "";
        foreach (clsUlkeKeyword cKeyword in clsCozumeleme.lKeyword)
        {

            foreach (string sMinKelime in clsCozumeleme.sMinKelimeler)
            {
                if (sParagraf.IndexOf(sMinKelime)>-1)
                {
                    bDurumMinKontrol = true;
                    break;
                }
            }

            foreach (string sMaxKelime in clsCozumeleme.sMaxKelimeler)
            {
                if (sParagraf.IndexOf(sMaxKelime) > -1)
                {
                    bDurumMaxKontrol = true;
                    break;
                }
            }

            foreach (string sKeywordd in cKeyword.KeywordEtiket)
            {
                if (sParagraf.IndexOf(sKeywordd) > -1)
                {
                    sKeyword += cKeyword.Keyword + ",";
                    sParagraf = sParagraf.Replace(sKeywordd, "");
                    break;
                }
            }

        }
        if (sKeyword!="")
        {
            sKeyword = sKeyword.Substring(0, sKeyword.Length - 1);
        }
       
        
            return sKeyword;
        
   

    }

}
