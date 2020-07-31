using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


    public static class clsDosyaOkuma
    {
    //Burada /Text/ bulunan metin belgeleri okunuldu.

    public static List<string> SoruKaliplariGetir()
    {
        List<string> lKelimeler = new List<string>();
        string[] sGereksizKelimeler = File.ReadAllLines(System.Web.HttpContext.Current.Server.MapPath("~/Text/SoruKaliplari.txt"));
        foreach (string sKelime in sGereksizKelimeler)
        {
            lKelimeler.Add(sKelime);
        }


        return lKelimeler;
    }


    public static List<clsUlke> UlkeAdlariniGetir()
    {
        string[] sGereksizKelimeler = File.ReadAllLines(System.Web.HttpContext.Current.Server.MapPath("~/Text/UlkeIsimleri.txt"));
    
            List<clsUlke> lUlkeler = new List<clsUlke>();
            foreach (string sUlke in sGereksizKelimeler)
            {
            if (sUlke=="")
            {
                break;
            }
                string[] sUlkeIdParcala = sUlke.Split('\t');
                clsUlke ulke = new clsUlke();
                ulke.ID = Convert.ToInt32(sUlkeIdParcala[0]);
                string[] sUlkeAlternatif = sUlkeIdParcala[1].Split(',');
                foreach (string sUlkeAltenatifAdi in sUlkeAlternatif)
                {
                Console.Write(sUlkeAltenatifAdi);
                ulke.sUlkeAlternatifAdlari.Add(sUlkeAltenatifAdi.ToLower());
               
                }
                lUlkeler.Add(ulke);
            }
            return lUlkeler;
        
    }



    public static List<clsUlkeKeyword> UlkeKeywordGetir()
    {
        string[] sGereksizKelimeler = File.ReadAllLines(System.Web.HttpContext.Current.Server.MapPath("~/Text/UlkeAlanAnahtarKelime.txt"));

        List<clsUlkeKeyword> lKeywords = new List<clsUlkeKeyword>();
        foreach (string sKeyword in sGereksizKelimeler)
        {
     
            if (sKeyword == "")
            {
                break;
            }

            string[] ssKeywordParcala = sKeyword.Split('\t');
            string[] sKeywordAlternatif = ssKeywordParcala[1].Split(',');
            clsUlkeKeyword clsKeyword = new clsUlkeKeyword();
            clsKeyword.Keyword = ssKeywordParcala[0];
            if (ssKeywordParcala[0]=="min")
            {
              
                foreach (string sKeywordAltenatifAdi in sKeywordAlternatif)
                {
                    // Console.Write(sUlkeAltenatifAdi);
                   clsCozumeleme.sMinKelimeler.Add(sKeywordAltenatifAdi.ToLower());

                }
            }
            else if (  ssKeywordParcala[0] == "max")
            {
                foreach (string sKeywordAltenatifAdi in sKeywordAlternatif)
                {
                    // Console.Write(sUlkeAltenatifAdi);
                    clsCozumeleme.sMaxKelimeler.Add(sKeywordAltenatifAdi.ToLower());

                }
            }

            else
            {
                foreach (string sKeywordAltenatifAdi in sKeywordAlternatif)
                {
                    // Console.Write(sUlkeAltenatifAdi);
                    clsKeyword.KeywordEtiket.Add(sKeywordAltenatifAdi.ToLower());

                }
                lKeywords.Add(clsKeyword);
            }
        }
        return lKeywords;

    }



    public static List<string> GereksizKelimeleriGetir()
    {
        List<string> lKelimeler = new List<string>();
        string[] sGereksizKelimeler = File.ReadAllLines(System.Web.HttpContext.Current.Server.MapPath("~/Text/StopWords.txt"));

        foreach (string sKelime in sGereksizKelimeler)
        {
            lKelimeler.Add(sKelime);
        }

        return lKelimeler;
    }

    public static List<string> CovidKelimeleriGetir()
    {
        List<string> lKelimeler = new List<string>();

        string[] sGereksizKelimeler = File.ReadAllLines(System.Web.HttpContext.Current.Server.MapPath("~/Text/Covid.txt"));

        foreach (string sKelime in sGereksizKelimeler)
        {
            lKelimeler.Add(sKelime);
        }

        return lKelimeler;
    }
}
