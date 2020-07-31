using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class clsUlke
    {
    //Ulke eşleşmesi ve listeye yüklenmesi yapıldı.

    public static string UlkeAdi { get; set; }
    public int ID { get; set; }
    public List<string> sUlkeAlternatifAdlari { get; set; }

    public clsUlke()
    {
        sUlkeAlternatifAdlari = new List<string>();
    }


    public static int UlkeAdiGeciyorMu(string sParagraf)
    {
        int sUlkeID = 0;
        string sUlkeAdi = "";
        foreach (clsUlke cUlke in clsCozumeleme.lUlkeler)
        {
            int iIkKarakter = -1;
          //  string sUlkeAlternatif in cUlke.sUlkeAlternatifAdlari
            for (int j=0;j<cUlke.sUlkeAlternatifAdlari.Count;j++)
            {
                int iIndex = sParagraf.IndexOf(cUlke.sUlkeAlternatifAdlari[j]);
                if ( iIndex> -1)
                {
                    clsUlke.UlkeAdi = cUlke.sUlkeAlternatifAdlari[0];
                    char[] cUlkeKarakter = sParagraf.ToCharArray();
                    sUlkeAdi = cUlke.sUlkeAlternatifAdlari[j];

                    if (iIndex==0)
                    {
                        return cUlke.ID;
                    }
                    else
                    {
                        for (int i = iIndex; i > 0; i--)
                        {
                            try
                            {
                                if (cUlkeKarakter[i] == ' ')
                                {
                                    iIkKarakter = i;
                                    break;
                                }

                            }
                            catch (Exception ex)
                            {
                                return cUlke.ID;

                            }
                        }
                        if ((iIkKarakter+1) <iIndex)
                        {
                            return 0;
                        }
                        else
                        {
                            return cUlke.ID;
                        }

                    }

                }

            }

        }
        clsUlke.UlkeAdi = "";
        return sUlkeID;
    }

}
