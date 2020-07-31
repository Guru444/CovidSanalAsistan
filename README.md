"# Covid-Sanal-Asistan" 
BL Klasöründeki;
----
clsApiDeger => Sözel kelimeler ve istatisksel bilgileri alınıp clsApiDeger ile degeri json formatında döndürmek için kullanıldı.

clsCozumeleme=> Cumle de bulunan stopwords,covid kelimeler ve 5N 1K soru kaliplarını direkt eşleştirmek için kullanıldı.

clsCumle=> Bir cümle de cumlenin kelimesini bulmaya ve propertyleri tutulur.

clsDosyaOkuma=> ~/Text/ altında bulunan .txt metin belgesini okumak için kullanır.

clsKelime=> Bir kelimenin adı ve kök property var ve bu kelimenin de kök bulmak için metot kullanılır.

clsKullanici=> Chat ortamında logları tutmak için bir 32 karakteri üretiliyor ve
 bu üretilen karakteri json formatında bu class döndürülür.
//Chati kullananlar adi gibi bilgileri girmeyeceğinden bir karakter üretilir.

clsUlke => ülkelerin adları listelenmesi ve paragraftan ülke eşlemesi için kullanılır.

clsUlkeKeyword =>CovidTableData tablomuzdaki alanları tespit etmek için 14 alanı bir txt dosyasında okunulur.
 paragraftan keywordlari eşleşmesi için bir methot ve list bulunur.

clsUtilities => Ana metot olup cumleleribol method paragraftan cumleye, cumleden kelimeye, kelimeden kök bulmaya giden
bir süreç bulunur.
Kelimelerigetir ise cumleleribol de bulunan cevap karşın bir kullanıcı bir istatistiksel metin mi sözel soru mu olduğunu
anlayıp sonuç olarak ona göre döndüren bir metotdur.

clsZemberek => Kelimeyi Zemberek içerisinde bulunan methodları kulanarak bize kökü döndüren bir method kullanılır.
Bulunduğu Konum: ~/dll/NZemberek.dll
Controllers Klasöründeki;

KelimeKontrolController => 2 Get method kullanılır.

1-get(string Kelime) 
~/api/KelimeKontrol?Kelime=paragraf
paragrafta alınır ve clsUtilities classındaki KelimeGetir methoduna gönderir.
2- get()
~/api/KelimeKontrol
32 karakterli bir user id oluşturulur ve Ip bilgisini clsKullanici yoluyla json formatında geri döndürür.

Js Klasöründeki,

jsChatBott => ilk olarak paragrafı ilk ajaxla Controllers/KelimeKontrolController atıp paragraf kelimeleri
alınır. ajaxtan dönen  data.kelimeler alınır.
 2. ajaxta ise diğer bir https://www.oyunpuanla.com/chatbot/covidapi.php url gerekli parametreleri yollayıp dönen değerleri ekrana yazdırılır.
(*) Parametreler;
minMaxDurum => paragrafta min max ilgili bir eşleşen kelime varsa ona göre min,max veya(min,max) şeklinde değer gönderilir.
Keyword= CovidTableData alanları(toplam vaka,ölüm oranı gibi) dinamik olarak alan1,alan2,.. alanları eklemek için kullanılır.
countryID => Ülke adları ile eşleşen bir ülke var ise id verisini eklemek için kullanıllır.
Kelimeler=> sözel veri ise paragraftan kelimeleri (kelime1,kelime2,..) verisini eklemek için kullanıllır.

Text Klasöründeki,
Klasördeki metin belgeleri okunur.

Covid.txt => Covid virüsünün birden fazla anlamını bu metin belgesinden alınır. Yani içerisindeki kelimeler köke gitmeden
direkt geri döndürülür.

StopWords.txt => 
StopWords = Cümle de tek başına anlam ifade etmeyen kelimelerdir.
Burada stopwords kelimeler alınır paragrafın içerinde stopwords atılır.
StopWords.txt alınan yer (kaynak):  https://github.com/ahmetax/trstop 

UlkeAlanAnahtarKelime.txt => Ülkeler ve diğer istatistiksel verilerin (CovidTableData DB)tablodaki alanların alternatif etiketleri tutulur.

UlkeIsimleri.txt => Ülkelerin id(bu alan veritabanından birebir yazıldı) ve ülke adi ve alternatif adlar 

ChatBot.html web sayfası
Web sayfasının alındığı kaynak : https://bootsnipp.com/snippets/ZlkBn
Bu sayfada http://localhost:61483/api/KelimeKontrol?Kelime=paragraf ilk ajaxla bir url alınır.
Bu alınan url (http://www.oyunpuanla.com/chatbot/chatbot.php? parametreler(gerekli parametreler '(*)' kısımdan bakabilirsiniz)) dönen json değeri 
istatiksel veri mi sözel veri diye algılatıp(jsChatBott.js) mesaj ekrana yazdırılır.

Global.asax
Application_Start methodunda orada statik olan listleri ilk siteye bağlanan kişiyi yükletiriyoruz. 





 
