using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class clsUtilities
{


    public static string CiftBosluklariTemizle(string sParagraf)
    {
        while (sParagraf.IndexOf("  ") != -1)
        {
            sParagraf = sParagraf.Replace("  ", " ");
        }
        return sParagraf;
    }

    public static string ElenenKelimeleriTemizle(string sParagraf, List<clsKelime> ElenenKelimeler)
    {
        foreach (clsKelime ElenenKelime in ElenenKelimeler)
        {
            sParagraf.Replace(" " + ElenenKelime.sAdi + " ", " ");
            sParagraf.Replace("." + ElenenKelime.sAdi + " ", ".");
            sParagraf.Replace(" " + ElenenKelime.sAdi + ".", ".");
        }
        return sParagraf;
    }
    static string sUrl = "";
    static int iUlkeId;
    static  string sKeyword;
    static string sMinMax = "";
    public static List<clsCumle> CumlelereBol(string sParagraf)
    {
        sParagraf = sParagraf.ToLower();
        List<clsCumle> Cumleler = new List<clsCumle>();
        clsCumle Cumle;
        string[] sCumleler;
        string[] sKelimeler;
        string[] sCumleAyiranNoktalamaIsaretleri = { ".", ":", ";" };
        string sTempParagraf = sParagraf;
        //Soru 5N 1K sorularını zemberek atmamyı yani ham halini aldık.
       sParagraf = clsCozumeleme.SoruKalibiVarMi(sTempParagraf,Cumleler);

        //Burada stopword karakterleri atıyoruz.
        foreach (string sGecersizKarakter in clsCumle.sGecersizKarakterler)
        {
            sParagraf = sParagraf.Replace(sGecersizKarakter, "");
        }
        //UlkeAdları.txt alınan ülkeler id verisini alıyoruz.
        iUlkeId = clsUlke.UlkeAdiGeciyorMu(sParagraf);
        //UlkeKeyword ile bizim tableCorona tablomuzdaki verilerde uyuşan bir tane veri gelirse veri,veri şeklinde diğer sunucuya yollanacak.
        sKeyword = clsUlkeKeyword.KeywordGeciyorMu(sParagraf);
        if (iUlkeId!=0 || sKeyword !="")
        {
            if (sKeyword.Split(',').Length>=1)
            {
                if (sKeyword.Split(',').Length == clsCozumeleme.lKeyword.Count || sKeyword=="")
                {
                    sUrl = "countryID=" + iUlkeId + "&Keyword=*";
                }
                else
                {  
                    sUrl = "countryID=" + iUlkeId + "&Keyword=" + sKeyword;
                }
            }
            else
            {
                if (iUlkeId!=0)
                {
                    sUrl = "countryID=" + iUlkeId + "&Keyword=*";
                }
            }

            if (clsUlkeKeyword.bDurumMaxKontrol && clsUlkeKeyword.bDurumMinKontrol)
            {
                sUrl += "&minmaxDurum=max,min";
                sMinMax = "max,min";
            }
            else  if (clsUlkeKeyword.bDurumMinKontrol && !clsUlkeKeyword.bDurumMaxKontrol)
            {
                sUrl += "&minmaxDurum=min";
                sMinMax = "min";
            }
            else if(clsUlkeKeyword.bDurumMaxKontrol && !clsUlkeKeyword.bDurumMinKontrol)
            {
                sUrl += "&minmaxDurum=max";
                sMinMax = "max";
            }
            //Enleri de verilerinden birini eşleşirse onun da verisini anlayıp diğer sunucuya yollanacak.
            clsUlkeKeyword.bDurumMinKontrol = false;
            clsUlkeKeyword.bDurumMaxKontrol = false;

            return null;
        }
   
        foreach (string sCumleAyiranNoktalamaIsareti in sCumleAyiranNoktalamaIsaretleri)
        {
            sParagraf = sParagraf.Replace(sCumleAyiranNoktalamaIsareti, "#");
        }

        //cumleleri parçalıyoruz.
        sCumleler = sParagraf.Split('#');

        foreach (string sCumle in sCumleler)
        {
            Cumle = new clsCumle();
            Cumle.sAdi = CiftBosluklariTemizle(sCumle);
            sKelimeler = Cumle.sAdi.Split(' ');
            foreach (string sKelime in sKelimeler)
            {
                Cumle.KelimeEkle(sKelime);
            }
            Cumleler.Add(Cumle);
        }


        return Cumleler;
    }

    public static clsApiDeger KelimeleriGetir(string sParagraf)
    {
        //clsApiDeger ile durum keywordleri verileri tutmak için kullanıldı.
        clsApiDeger ApiDeger = new clsApiDeger();
        string sKelime = "";
        List<clsCumle> lCumleler = CumlelereBol(sParagraf);
        sKelime = "https://www.oyunpuanla.com/chatbot/covidapi.php?";
        if (lCumleler==null)
        {
          ApiDeger.Kelimeler= sKelime+sUrl;
            ApiDeger.UlkeAdi = clsUlke.UlkeAdi.ToUpper();
            ApiDeger.AnahtarKelimeler = sKeyword;
            ApiDeger.Durum = true;
            ApiDeger.MinMaxDurum = sMinMax;
            sMinMax = "";
            clsUlkeKeyword.bDurumMaxKontrol = false;
            clsUlkeKeyword.bDurumMinKontrol = false;
            return ApiDeger;
        }

        sKelime += "kelimeler=";
        foreach (clsCumle Cumle in lCumleler)
        {
           
            foreach (clsKelime Kelime in Cumle.Kelimeler)
            {
                if (Kelime.sKok=="")
                {
                    sKelime += "'" + Kelime.sAdi + "',";
                }
                else
                {
                    sKelime += "'" + Kelime.sKok + "',";
                }
              
            }
        }

        sKelime = sKelime.Substring(0, sKelime.Length - 1);
        ApiDeger.Kelimeler = sKelime;
        ApiDeger.Durum = false;
        return ApiDeger;
    }
}