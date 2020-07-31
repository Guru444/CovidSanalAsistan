using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Web.ChatBot.Controllers
{
    public class KelimeKontrolController : ApiController
    {
        public clsApiDeger Get(string Kelime)
        {
            clsApiDeger caKelimeler = new clsApiDeger();

            //clsUtilities sistemin ana prosesidir bütün ana metodlar burada bulunuyor.
            clsApiDeger apiDeger = clsUtilities.KelimeleriGetir(Kelime);
           
           
            return apiDeger;
        }

        public clsKullanici Get()
        {

            //Burada newid bir 32 karakterlik bir karakter üretiyor.
            Guid g = Guid.NewGuid();
            //Bir clsKullanici api ve id yolluyorum json formatında.
            clsKullanici kullanici = new clsKullanici();
            kullanici.ID= BitConverter.ToString(g.ToByteArray()).Replace("-","");
            //ip adres alır. Localde çalıştırınca :::1 verir..
            string clientAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            kullanici.IP = clientAddress;
            return kullanici;
        }


    }
}
