using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class clsCozumeleme
{
//Text okunacak verilerin list bu class toplanıldı.
    static bool bDurumKontrol = false;
    public static List<string> lStopWordsKelimeler = new List<string>();
    public static List<string> sSoruKaliplari=new List<string>();
    public static List<string> sCovidKelimeler = new List<string>();
    public static List<clsUlke> lUlkeler = new List<clsUlke>();
    public static List<clsUlkeKeyword> lKeyword = new List<clsUlkeKeyword>();

    public static List<string> sMinKelimeler = new List<string>();
    public static List<string> sMaxKelimeler = new List<string>();
    public static void StopWordsGetir()
    {

        if (!bDurumKontrol)
        {
            bDurumKontrol = true;
            lStopWordsKelimeler = clsDosyaOkuma.GereksizKelimeleriGetir();
        }
 
    
    }

    public static void CovidKelimeleriGetir()
    {
        sCovidKelimeler = clsDosyaOkuma.CovidKelimeleriGetir();
    }

    public static void  SorulariGetir()
    {
        sSoruKaliplari = clsDosyaOkuma.SoruKaliplariGetir();
    }

    public static void KeywordGetir()
    {
        lKeyword = clsDosyaOkuma.UlkeKeywordGetir();
    }

    public static void UlkeleriGetir()
    {
        lUlkeler = clsDosyaOkuma.UlkeAdlariniGetir();
    }


    public static string SoruKalibiVarMi(string sParagraf,List<clsCumle> Cumleler)
    {
        foreach (string sSoruKalip in clsCozumeleme.sSoruKaliplari)
        {
            int index = 0;
            index = sParagraf.IndexOf(sSoruKalip);
            if (index > -1)
            {
                string sTemp = sParagraf.Substring(index, sSoruKalip.Length);
                char[] cHarfler = sParagraf.ToCharArray();
                string sKelime = "";
                try
                {
                    for (int i = index+1; i < cHarfler.Length; i++)
                    {

                        if (cHarfler[i] !=' ')
                        {
                            break;
                        }
                        else
                        {
                            sKelime += cHarfler[i];
                        }
                    }


   

                    if (sKelime.Length > -1 && sKelime!="")
                    {
                        if (sKelime!="")
                        {
                            sParagraf = sParagraf.Replace(sKelime, "");
                        }
                        else
                        {
                            sParagraf = sParagraf.Replace(sSoruKalip, "");
                        }
                       
                      
                    }
                    else
                    {
                        sParagraf = sParagraf.Replace(sSoruKalip, "");

                    }
                    Cumleler.Add(new clsCumle
                    {
                        Kelimeler = new List<clsKelime> { new clsKelime {
                    sAdi=sTemp
                } }
                    });

                }
                catch (Exception ex)
                {
                    // Burda hepsi boşluk hatası verdiğinde!
                    return "";
                }              
            }

        }
        return sParagraf;
    }
}
