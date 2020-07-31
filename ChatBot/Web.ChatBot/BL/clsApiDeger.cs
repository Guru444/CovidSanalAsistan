using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class clsApiDeger
    {
    //True istatistik false Metin
    public bool Durum { get; set; }
    //Kelimelerin gövdelerini gövde,gövde veri yollanıldı.
    public string Kelimeler { get; set; }
    //Burada ulkeadlari.txt alınan verilerde ülke adlarında eşleşenin id yollanır.
    public string UlkeAdi { get; set; }
    //Min max eşleşince min,max şeklinde yollanır.
    public string MinMaxDurum { get; set; }
    //Keywordlari alan,alan yollandı.
    public string AnahtarKelimeler { get; set; }
}
