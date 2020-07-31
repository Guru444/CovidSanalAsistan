using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class clsCumle
{
    string _sAdi = "";
    List<clsKelime> _Kelimeler = new List<clsKelime>();
    List<string[]> _KelimeGruplari = new List<string[]>();
    int iMaxGrupAraligi = 5;
    //Geçersiz karakterleri cumleden çıkarmak için kullanılır.
    public static string[] sGecersizKarakterler = { "=","(", ")", "#", "-", "/", "@", "[", "]", "é", "~", "^", "!", "?" };

    public string sAdi
    {
        get { return _sAdi; }
        set { _sAdi = value; }
    }

    public List<clsKelime> Kelimeler
    {
        get { return _Kelimeler; }
        set { _Kelimeler = value; }
    }

    public List<string[]> KelimeGruplari
    {
        get { return _KelimeGruplari; }
        set { _KelimeGruplari = value; }
    }


  

    public void KelimeEkle(string sKelime)
    {
        //kelimenin kokunu ve kendisini eklemelisin
        clsKelime Kelime;
        if (sKelime.Trim() != "")
        {
            //Stopwords kelimeler varsa onları almıyoruz.
            if (StopWordsKelimeMi(sKelime))
            {
                return;
            }

            //Covid -Koronavirüs kökünü gideremiyor O yüzden bir listede verileri tutup
            //onu eşitliyoruz(ekleri atmak için)
            //Koronavirüsten -> Koronavirüs 
            bool bDurumCovidKontrol = false;
            string sTmp = CovidKelimeMi(sKelime);
            if (sTmp!="")
            {
                sKelime = sTmp;
                bDurumCovidKontrol = true;
            }
            Kelime = new clsKelime();
            //Burada Covid text uyuşan varsa kökünü de kelimeyi yazıyoruz.
            if (!bDurumCovidKontrol)
            {
                    Kelime.sAdi = sKelime;
                    Kelime.KokBul(); 
            }
            else
            { 
                Kelime.sAdi = sKelime;
                //Kelime.sKok = sKelime;
            }       
            _Kelimeler.Add(Kelime);
        }
    }




    //StopWords Kelime kontrol ediliyor.
    private bool StopWordsKelimeMi(string sKelime)
    {
        foreach (string sGereksizKelime in clsCozumeleme.lStopWordsKelimeler)
        {

            if (sGereksizKelime == sKelime)
            {
                return true;
            }
        }
        return false;
    }

    //Covid alternatifi olarak girilecek kelimeleri kontrolu yapılıyor.
    private string CovidKelimeMi(string sKelime)
    {
        foreach (string kelime in clsCozumeleme.sCovidKelimeler)
        {
            int iIndex = sKelime.IndexOf(kelime);
            if (iIndex > -1)
            {
                char[] cKelimeKarakterler = sKelime.ToCharArray();
                string sKelimee = "";
                try
                {
                    for (int i = iIndex ; i < cKelimeKarakterler.Length; i++)
                    {

                        if (cKelimeKarakterler[i] == ' ')
                        {
                            break;
                        }
                        else
                        {
                            sKelimee += cKelimeKarakterler[i];
                        }
                    }

                    string sSoldanKelime = "";
                    for (int i =iIndex-1; i >= 0; i--)
                    {

                        if (cKelimeKarakterler[i] == ' ')
                        {
                            break;
                        }
                        else
                        {
                            sSoldanKelime += cKelimeKarakterler[i];
                        }
                    }
                    if (iIndex==0)
                    {
                        return kelime;
                    }
                    else if(kelime==sKelime)
                    {
                        return kelime;
                    }
                    else if (sKelimee.Length > -1 && sKelimee != "")
                    {
                            if (sSoldanKelime=="")
                            {
                                return sKelime;
                            }
                            else 
                            {
                                return "";
                            }                  
                    }
                    else
                    {
                        return "";
                    }


                }
                catch (Exception ex)
                {
                    // Burda hepsi boşluk hatası verdiğinde!
                    return "";
                }
            }
        }

        return "";
    }
}
