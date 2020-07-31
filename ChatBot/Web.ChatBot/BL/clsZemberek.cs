using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using net.zemberek.erisim;
using net.zemberek.yapi;
using net.zemberek.tr.yapi;
using net.zemberek.yapi.ek;


public static class clsZemberek
{
    static Zemberek zZemberek = new Zemberek(new TurkiyeTurkcesi());
    private static string sKelime;

    public static string _Kelime
    {
        get { return sKelime; }
        set { sKelime = value; }
    }

    public static bool EkKontrol(Ek ek)
    {
        bool bKabul = true;
        if (
                    // Cogul Ekleri -ler, -lar
                    (Convert.ToString(ek).Contains("ISIM_COGUL_LER"))
                    || (Convert.ToString(ek).Contains("ISIM_COGUL_LAR"))

                    // Durum (hal) Ekleri -i, -e, -de, -den
                    || (Convert.ToString(ek).Contains("ISIM_BELIRTME_I"))
                    || (Convert.ToString(ek).Contains("ISIM_YONELME_E"))
                    || (Convert.ToString(ek).Contains("ISIM_KALMA_DE"))
                    || (Convert.ToString(ek).Contains("ISIM_CIKMA_DEN"))

                    // Tamlama
                    || (Convert.ToString(ek).Contains("ISIM_TAMLAMA_"))
                    /*
                    || (Convert.ToString(ek).Contains("ISIM_TAMLAMA_I"))
                    || (Convert.ToString(ek).Contains("ISIM_TAMLAMA_IN"))
                     */

                    // Iyelik -im, -in
                    || (Convert.ToString(ek).Contains("ISIM_SAHIPLIK_"))
                    /*
                    || (Convert.ToString(ek).Contains("ISIM_SAHIPLIK_BEN_IM"))
                    || (Convert.ToString(ek).Contains("ISIM_SAHIPLIK_SEN_IN"))
                    || (Convert.ToString(ek).Contains("ISIM_SAHIPLIK_O_I"))
                    || (Convert.ToString(ek).Contains("ISIM_SAHIPLIK_BIZ_IMIZ"))
                    || (Convert.ToString(ek).Contains("ISIM_SAHIPLIK_SIZ_INIZ"))
                    || (Convert.ToString(ek).Contains("ISIM_SAHIPLIK_ONLAR_LERI"))
                     */

                    // Kisi
                    || (Convert.ToString(ek).Contains("ISIM_KISI_"))
                    /*
                    || (Convert.ToString(ek).Contains("ISIM_KISI_BEN_IM"))
                    || (Convert.ToString(ek).Contains("ISIM_KISI_SEN_SIN"))
                    || (Convert.ToString(ek).Contains("ISIM_KISI_BIZ_IZ"))
                    || (Convert.ToString(ek).Contains("ISIM_KISI_SIZ_SINIZ"))
                     */

                    // Diger
                    || (Convert.ToString(ek).Contains("ISIM_TANIMLAMA_DIR"))

                    // Fiiler icin

                    || (Convert.ToString(ek).Contains("FIIL_GECMISZAMAN_"))
                    || (Convert.ToString(ek).Contains("FIIL_SIMDIKIZAMAN_"))
                    || (Convert.ToString(ek).Contains("FIIL_GENISZAMAN_"))
                    || (Convert.ToString(ek).Contains("FIIL_GELECEKZAMAN_"))
                    || (Convert.ToString(ek).Contains("FIIL_KISI_"))
                    //    || (Convert.ToString(ek).Contains("FIIL_OLUMSUZLUK_"))
                    || (Convert.ToString(ek).Contains("FIIL_EMIR_"))
                    //|| (Convert.ToString(ek).Contains("FIIL_DONUSUM_"))
                    //  || (Convert.ToString(ek).Contains("ISIM_DONUSUM_"))
                    || (Convert.ToString(ek).Contains("FIIL_SART_"))
                    || (Convert.ToString(ek).Contains("FIIL_ISTEK_"))
                    || (Convert.ToString(ek).Contains("IMEK_"))
                    || (Convert.ToString(ek).Contains("FIIL_SUREKLILIK_"))
                    || (Convert.ToString(ek).Contains("FIIL_ZORUNLULUK_"))
                      || (Convert.ToString(ek).Contains("FIIL_MASTAR_"))
                    )
        {

            bKabul = false;
        }
        return bKabul;
    }

    public static Kelime[] Cozumle(string sKelime)
    {
        Kelime[] cozumler = zZemberek.kelimeCozumle(sKelime);
        if (cozumler.Length == 0)
        {
            return null;
        }
        return cozumler;
    }

    public static List<Kok> KokGetir(string sKelime)
    {
        List<Kok> kokler = new List<Kok>();
        Kelime[] cozumler = Cozumle(sKelime);

        if (cozumler != null)
        {
            foreach (Kelime cozum in cozumler)
            {
                kokler.Add(cozum.kok());
                break;
            }
        }
        return kokler;
    }

    public static List<Ek> EkGetir(Kelime kKelime)
    {
        List<Ek> ekler = new List<Ek>();

        foreach (Ek ek in kKelime.ekler())
        {
            if (EkKontrol(ek))
            {
                ekler.Add(ek);
            }
            else
                break;
        }

        return ekler;
    }

    public static List<string> KelimelerGetir(string sKelime)
    {
        Kok kok;
        List<Ek> ekler;
        List<string> sKelimeler = new List<string>();
        string sYeniKelime;
        Kelime[] cozumler = Cozumle(sKelime);

        foreach (Kelime cozum in cozumler)
        {
            kok = cozum.kok();
            ekler = EkGetir(cozum);
            sKelimeler.Add(kok.icerik());
            if (ekler.Count > 0)
            {
                sYeniKelime = zZemberek.kelimeUret(kok, ekler);
                sKelimeler.Add(sYeniKelime);
            }
        }

        return sKelimeler;
    }

}