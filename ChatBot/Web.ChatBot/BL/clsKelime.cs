using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using net.zemberek.yapi;

//Kelimeler ilgili işlemler bulunur. Burada zemberek kök verisi alındı.
public class clsKelime
{

    int _iKodu = 0;
    string _sAdi = "";
    string _sKok = "";

    public int iKodu
    {
        get { return _iKodu; }
        set { _iKodu = value; }
    }

    public string sAdi
    {
        get { return _sAdi; }
        set
        {
            _sAdi = value;
        }
    }

    public string sKok
    {
        get { return _sKok; }
        set { _sKok = value; }
    }

    public void KokBul()
    {
        List<Kok> kokler;
        kokler = clsZemberek.KokGetir(this.sAdi);
        if (kokler.Count > 0)
            sKok = kokler[0].icerik();
    }

}