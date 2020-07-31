
(function () {
    var Message;
    Message = function (arg) {
        this.text = arg.text, this.message_side = arg.message_side;
        this.draw = function (_this) {
            return function () {
                var $message;
                $message = $($('.message_template').clone().html());
                $message.addClass(_this.message_side).find('.text').html(_this.text);
                $('.messages').append($message);
                return setTimeout(function () {
                    return $message.addClass('appeared');
                }, 0);
            };
        }(this);
        return this;
    };
   
    $(function () {
     
        var getMessageText, message_side, sendMessage;
        message_side = 'right';
        getMessageText = function () {
            var $message_input;
            $message_input = $('.message_input');
            return $message_input.val();
        };
        var durum = 1;
        var IlkDurumKontrol = 0;
        var sKelimeler = "";
      
        var UrlKoduSonucVeren = "";
        SendQuestion = function (text) {        
            var urlKodu = "http://localhost:62357/api/KelimeKontrol";
            urlKodu += "?Kelime=" + text;
            try {

          
            //Bu ajax Cümleyi kelimeleri yarayan Api gelecek
            $.ajax({
                url: urlKodu,
                dataType: 'json',
                type: 'GET',
                success: function (data) {
                    console.log(data);
                    UrlKoduSonucVeren = data.Kelimeler;
                    //Bu ajax ise kişinin vereceği api ile soruyu ve cevap alınacak. 
               

                    $.ajax({

                        url: UrlKoduSonucVeren,
                        dataType: 'json',
                        type: 'GET',
                        success: function (data1) {
                            var metin = new String();
                            console.log(data1);
                            var sDogruSoru = "";
                          //  istatistik bilgileri
                            if (data.Durum == true) {
                                //Burada enlerin istatistik bilgileri
                                if (data1[0].min != null || data1[0].max != null) {
                                    metin = "istatistik <br/>";
                                    var sParcalaMinMax = data.MinMaxDurum.split(',');
                                    for (var i = 0; i < data1.length; i++) {
                                        var sMinMaxKelime = new String();
                                            if (data.AnahtarKelimeler != "" || data.AnahtarKelimeler != null) {
                                                if (data1[i].min != null) {
                                                    sMinMaxKelime = "en az";
                                                    //Toplam Vaka
                                                    if (data1[i].totalCases != null) {
                                                        console.log(data1[i].totalCases);
                                                        metin += sMinMaxKelime + " toplam vaka sayısı: " + data1[i].min + "<br/>";
                                                    }


                                                    if (data1[i].newDeaths != null) {
                                                        metin += sMinMaxKelime + " Bugün Ölüm Sayısı: " + data1[i].min + "<br/>";
                                                    }

                                                    if (data1[i].totalRecovered != null) {
                                                        metin += sMinMaxKelime + " toplam kurtulan sayısı: " + data1[i].min + "<br/>";
                                                    }
                                                    if (data1[i].newCases != null) {
                                                        metin += sMinMaxKelime + " Bugün vaka Sayısı " + data1[i].min + "<br/>";
                                                    }
                                                    if (data1[i].totalDeaths != null) {
                                                        metin += sMinMaxKelime + " toplam Ölüm Sayısı: " + data1[i].min + "<br/>";
                                                    }

                                                    if (data1[i].seriusCritical != null) {
                                                        metin += sMinMaxKelime + " toplam Entübe/Yoğun bakımında(Kritik) bulunan hasta sayısı: " + data1[i].min + "<br/>";
                                                    }


                                                    if (data1[i].population != null) {
                                                        metin += sMinMaxKelime + " Toplam Nüfus: " + data1[i].min + "<br/>";
                                                    }
                                                    if (data1[i].newRecovered != null) {
                                                        metin += sMinMaxKelime + " Bugün İyileşen Hasta Sayisi:  " + data1[i].min + "<br/>";
                                                    }

                                                    if (data1[i].test1MPOP != null) {
                                                        metin += sMinMaxKelime + " 1 milyondaki test sayısı: " + data1[i].min + "<br/>";
                                                    }

                                                    if (data1[i].totalTest != null) {
                                                        metin += sMinMaxKelime + " Toplam test sayısı: " + data1[i].min + "<br/>";
                                                    }
                                                    if (data1[i].deaths1MPOP != null) {
                                                        metin += sMinMaxKelime + " bir milyondaki ölüm sayısı: " + data1[i].min + "<br/>";
                                                    }
                                                    if (data1[i].totCases1MPOP != null) {
                                                        metin += sMinMaxKelime + " bir milyondaki vaka sayısı: " + data1[i].min + "<br/>";
                                                    }

                                                    if (data1[i].activeCases != null) {
                                                        metin += sMinMaxKelime + " toplam Aktif hasta sayısı: " + data1[i].min + "<br/>";
                                                    }

                                                    if (data1[i].totalRecovered != null) {
                                                        metin += sMinMaxKelime + " Toplam Kurtulan sayısı: " + data1[i].min + "<br/>";
                                                    }

                                                    if (data1[i].deathRate != null) {
                                                        metin += sMinMaxKelime + " Ölüm Oranı: " + data1[i].min + "<br/>";
                                                    }


                                                }
                                                else {
                                                    sMinMaxKelime = "en yüksek";
                                                    //Toplam Vaka
                                                    if (data1[i].totalCases != null) {
                                                        console.log(data1[i].totalCases);
                                                        metin += sMinMaxKelime + " toplam vaka sayısı: " + data1[i].max + "<br/>";
                                                    }


                                                    if (data1[i].newDeaths != null) {
                                                        metin += sMinMaxKelime + " Bugün Ölüm Sayısı: " + data1[i].max + "<br/>";
                                                    }

                                                    if (data1[i].totalRecovered != null) {
                                                        metin += sMinMaxKelime + " toplam kurtulan sayısı: " + data1[i].max + "<br/>";
                                                    }
                                                    if (data1[i].newCases != null) {
                                                        metin += sMinMaxKelime + " Bugün vaka Sayısı " + data1[i].max + "<br/>";
                                                    }
                                                    if (data1[i].totalDeaths != null) {
                                                        metin += sMinMaxKelime + " toplam Ölüm Sayısı: " + data1[i].max + "<br/>";
                                                    }

                                                    if (data1[i].seriusCritical != null) {
                                                        metin += sMinMaxKelime + " toplam Entübe/Yoğun bakımında(Kritik) bulunan hasta sayısı: " + data1[i].max + "<br/>";
                                                    }


                                                    if (data1[i].population != null) {
                                                        metin += sMinMaxKelime + " Toplam Nüfus: " + data1[i].max + "<br/>";
                                                    }
                                                    if (data1[i].newRecovered != null) {
                                                        metin += sMinMaxKelime + " Bugün İyileşen Hasta Sayisi:  " + data1[i].max + "<br/>";
                                                    }

                                                    if (data1[i].test1MPOP != null) {
                                                        metin += sMinMaxKelime + " 1 milyondaki test sayısı: " + data1[i].max + "<br/>";
                                                    }

                                                    if (data1[i].totalTest != null) {
                                                        metin += sMinMaxKelime + " Toplam test sayısı: " + data1[i].max + "<br/>";
                                                    }
                                                    if (data1[i].deaths1MPOP != null) {
                                                        metin += sMinMaxKelime + " bir milyondaki ölüm sayısı: " + data1[i].max + "<br/>";
                                                    }
                                                    if (data1[i].totCases1MPOP != null) {
                                                        metin += sMinMaxKelime + " bir milyondaki vaka sayısı: " + data1[i].max + "<br/>";
                                                    }

                                                    if (data1[i].activeCases != null) {
                                                        metin += sMinMaxKelime + " toplam Aktif hasta sayısı: " + data1[i].max + "<br/>";
                                                    }

                                                    if (data1[i].totalRecovered != null) {
                                                        metin += sMinMaxKelime + " Toplam Kurtulan sayısı: " + data1[i].max + "<br/>";
                                                    }

                                                    if (data1[i].deathRate != null) {
                                                        metin += sMinMaxKelime + " Ölüm Oranı: " + data1[i].max + "<br/>";
                                                    }


                                                }



                                            }

                                        

                                    }
                                    metin = metin.toUpperCase();
                                }
                                else {
                                     //Burada Ülkelerin istatistik bilgileri
                                    if (data1.hataMesaji != null) {
                                        metin = data1.hataMesaji;

                                    }
                                    else {

                                        try {
                                            metin = data.UlkeAdi + "<br/>";
                                            if (data.AnahtarKelimeler == "" || data.AnahtarKelimeler == null) {
                                                //Toplam Vaka
                                                if (data1[0].totalCases == "-1") {
                                                    metin += "toplam vaka sayısı açıklanmamıştır <br/>";
                                                } else {
                                                    metin += "toplam vaka sayısı: " + data1[0].totalCases + "<br/>";
                                                }


                                                if (data1[0].newDeaths == "-1") {
                                                    metin += "Bugün ölüm sayısı açıklanmamıştır <br/>";
                                                } else {
                                                    metin += "Bugün Ölüm Sayısı: " + data1[0].newDeaths.replace("+", "") + "<br/>";
                                                }

                                                if (data1[0].totalRecovered == "-1") {
                                                    metin += "toplam kurtulan sayısı açıklanmamıştır <br/>";
                                                } else {
                                                    metin += "toplam kurtulan sayısı: " + data1[0].totalRecovered + "<br/>";
                                                }
                                                if (data1[0].newCases == "-1") {
                                                    metin += "Bugün vaka açıklanmamıştır  <br/>";
                                                } else {
                                                    metin += "Bugün vaka Sayısı " + data1[0].newCases.replace("+", "") + "<br/>";
                                                }
                                                if (data1[0].totalDeaths == "-1") {
                                                    metin += "toplam Ölüm sayısı açıklanmamıştır <br/>";
                                                } else {
                                                    metin += "toplam Ölüm Sayısı: " + data1[0].totalDeaths + "<br/>";
                                                }

                                                if (data1[0].seriusCritical === "-1") {
                                                    metin += "toplam Entübe/Yoğun bakımında  bulunan hasta sayısı açıklanmamıştır" + "<br/>";
                                                }
                                                else {
                                                    metin += "toplam Entübe/Yoğun bakımında(Kritik) bulunan hasta sayısı: " + data1[0].seriusCritical + "<br/>";
                                                }


                                                if (data1[0].population == "-1") {
                                                    metin += "Toplam Nüfus açıklanmamıştır  <br/>";
                                                } else {
                                                    metin += "Toplam Nüfus: " + data1[0].population + "<br/>";
                                                }

                                                if (data1[0].newRecovered == "-1") {
                                                    metin += "Bugün İyileşen Hasta Sayisi açıklanmamıştır. <br/>";
                                                } else {
                                                    metin += "Bugün İyileşen Hasta Sayisi:  " + data1[0].newRecovered.replace("+", "") + "<br/>";
                                                }

                                                if (data1[0].test1MPOP == "-1") {
                                                    metin += "1 milyondaki test sayısı açıklanmamıştır. <br/>";
                                                } else {
                                                    metin += "1 milyondaki test sayısı: " + data1[0].test1MPOP + "<br/>";
                                                }

                                                if (data1[0].totalTest == "-1") {
                                                    metin += "Toplam test sayısı açıklanmamıştır. <br/>";
                                                } else {
                                                    metin += "Toplam test sayısı: " + data1[0].totalTest + "<br/>";
                                                }

                                                if (data1[0].deaths1MPOP == "-1") {
                                                    metin += "bir milyondaki ölüm sayısı açıklanmamıştır. <br/>";
                                                } else {
                                                    metin += "bir milyondaki ölüm sayısı: " + data1[0].deaths1MPOP + "<br/>";
                                                }

                                                if (data1[0].totCases1MPOP == "-1") {
                                                    metin += "bir milyondaki vaka sayısı açıklanmamıştır. <br/>";
                                                } else {
                                                    metin += "bir milyondaki vaka sayısı: " + data1[0].totCases1MPOP + "<br/>";
                                                }

                                                if (data1[0].activeCases == "-1") {
                                                    metin += "toplam Aktif hasta sayısı  açıklanmamıştır. <br/>";
                                                } else {
                                                    metin += "toplam Aktif hasta sayısı: " + data1[0].activeCases + "<br/>";
                                                }

                                                if (data1[0].totalRecovered == "-1") {
                                                    metin += "toplam Kurtulan sayısı  açıklanmamıştır. <br/>";
                                                } else {
                                                    metin += "Toplam Kurtulan sayısı: " + data1[0].totalRecovered + "<br/>";
                                                }

                                                if (data1[0].deathRate == "-1") {
                                                    metin += "Ölüm Oranı  açıklanmamıştır. <br/>";
                                                } else {
                                                    metin += "Ölüm Oranı: " + data1[0].deathRate + "<br/>";
                                                }


                                            }
                                            else {
                                                var KeywordArray = data.AnahtarKelimeler.split(",");

                                                for (var i = 0; i < KeywordArray.length; i++) {
                                                    console.log(KeywordArray[i]);
                                                    switch (KeywordArray[i]) {
                                                        case "totalCases":
                                                            if (data1[0].totalCases == "-1") {
                                                                metin += "toplam vaka sayısı açıklanmamıştır <br/>";
                                                            } else {
                                                                metin += "toplam vaka sayısı: " + data1[0].totalCases + "<br/>";
                                                            }
                                                            break;

                                                        case "newDeaths":
                                                            if (data1[0].newDeaths == "-1") {
                                                                metin += "Yeni ölüm sayısı açıklanmamıştır <br/>";
                                                            } else {
                                                                metin += "Bugün Ölüm Sayısı " + data1[0].newDeaths.replace("+", "") + "<br/>";
                                                            }
                                                            break;

                                                        case "totalRecovered":
                                                            if (data1[0].totalRecovered == "-1") {
                                                                metin += "Yeni toplam kurtulan sayısı açıklanmamıştır <br/>";
                                                            } else {
                                                                metin += "Yeni toplam kurtulan sayısı " + data1[0].totalRecovered + "<br/>";
                                                            }
                                                            break;

                                                        case "newCases":
                                                            if (data1[0].newCases == "-1") {
                                                                metin += "Yeni vaka açıklanmamıştır  <br/>";
                                                            } else {
                                                                metin += "Bugün vaka Sayısı " + data1[0].newCases.replace("+", "") + "<br/>";
                                                            }

                                                            break;
                                                        case "totalDeaths":
                                                            if (data1[0].totalDeaths == "-1") {
                                                                metin += "toplam Ölüm sayısı açıklanmamıştır <br/>";
                                                            } else {
                                                                metin += "toplam Ölüm Sayısı: " + data1[0].totalDeaths + "<br/>";
                                                            }
                                                            break;

                                                        case "seriusCritical":

                                                            if (data1[0].seriusCritical === "-1" || data1[0].seriusCritical == null) {
                                                                metin += "Yeni toplam Entübe/Yoğun bakımında||  bulunan hasta sayısı açıklanmamıştır  <br/>";
                                                            }
                                                            else {
                                                                metin += "toplam Entübe/Yoğun bakımında(Kritik) bulunan hasta sayısı " + data1[0].seriusCritical + "<br/>";
                                                            }

                                                            break;

                                                        case "population":
                                                            if (data1[0].population == "-1") {
                                                                metin += "Toplam Nüfus açıklanmamıştır <br/>";
                                                            } else {
                                                                metin += "Toplam Nüfus: " + data1[0].population + "<br/>";
                                                            }
                                                            break;


                                                        case "newRecovered":
                                                            if (data1[0].newRecovered == "-1") {
                                                                metin += "Bugün İyileşen Hasta Sayisi açıklanmamıştır. <br/>";
                                                            } else {
                                                                metin += "Bugün İyileşen Hasta Sayisi:  " + data1[0].newRecovered.replace("+", "") + "<br/>";
                                                            }
                                                            break;

                                                        case "test1MPOP":
                                                            if (data1[0].test1MPOP == "-1") {
                                                                metin += "1 milyondaki test sayısı açıklanmamıştır. <br/>";
                                                            } else {
                                                                metin += "1 milyondaki test sayısı: " + data1[0].test1MPOP + "<br/>";
                                                            }
                                                            break;


                                                        case "totalTest":

                                                            if (data1[0].totalTest == "-1") {
                                                                metin += "Toplam test sayısı açıklanmamıştır. <br/>";
                                                            } else {                                             
                                                                metin += "Toplam test sayısı: " + Number(data1[0].totalTest) + "<br/>";
                                                            }
                                                            break;


                                                        case "deaths1MPOP":
                                                            if (data1[0].deaths1MPOP == "-1") {
                                                                metin += "bir milyondaki ölüm sayısı açıklanmamıştır. <br/>";
                                                            } else {
                                                                metin += "bir milyondaki ölüm sayısı: " + data1[0].deaths1MPOP + "<br/>";
                                                            }
                                                            break;


                                                        case "totCases1MPOP":
                                                            if (data1[0].totCases1MPOP == "-1") {
                                                                metin += "bir milyondaki vaka sayısı açıklanmamıştır. <br/>";
                                                            } else {
                                                                metin += "bir milyondaki vaka sayısı: " + data1[0].totCases1MPOP + "<br/>";
                                                            }
                                                            break;


                                                        case "activeCases":
                                                            if (data1[0].activeCases == "-1") {
                                                                metin += "toplam Aktif hasta sayısı  açıklanmamıştır. <br/>";
                                                            } else {
                                                                metin += "toplam Aktif hasta sayısı: " + data1[0].activeCases + "<br/>";
                                                            }
                                                            break;


                                                        case "totalRecovered":
                                                            if (data1[0].totalRecovered == "-1") {
                                                                metin += "toplam Kurtulan sayısı  açıklanmamıştır. <br/>";
                                                            } else {
                                                                metin += "Toplam Kurtulan sayısı: " + data1[0].totalRecovered + "<br/>";
                                                            }
                                                            break;

                                                        case "deathRate":
                                                            if (data1[0].deathRate == "-1") {
                                                                metin += "Ölüm Oranı  açıklanmamıştır. <br/>";
                                                            } else {
                                                                metin += "Ölüm Oranı: " + Number(data1[0].deathRate) + "<br/>";
                                                            }
                                                            break;
                                                        default:
                                                            break;
                                                    }

                                                }
                                            }
                                        } catch (e) {
                                            metin = data1[0].hataMesaji;

                                            console.log(e.$message);
                                        }
                                    }


                                }
                            }

                            else {
                                //Sözel soruların cevapları buradadır.
                                if (data1[0].cevapsoru != "" || data1[0].cevapsoru != null) {
                                    metin = data1[0].cevapsoru;
                                    sDogruSoru = data1[0].cevapsoru;
                              // IdGetir(text, data1[0].cevapsoru);
                                    
                                }
                                else {
                                    metin = data1.hataMesaji;
                                }
                            }
                              


                          
                            var $messages, message;
                            $('.message_input').val('');
                            $messages = $('.messages');
                            message_side = "right";
                            message = new Message({
                                text: metin,
                                message_side: message_side
                            });
                            message.draw();
                            $messages.animate({ scrollTop: $messages.prop('scrollHeight') }, 300);

                            //Burada logları DB aktarıyoruz.
                            IdGetir(text, metin.toUpperCase());

                        }, error: function (jqXHR, err) {
                            console.log("hata");
                            var $messages, message;
                            $('.message_input').val('');
                            $messages = $('.messages');
                            message_side = "right";
                            message = new Message({
                                text: 'Ooops! Uzgününüm, Sorduğunuz soruyu anlayamadım :(',
                                message_side: message_side
                            });
                            message.draw();
                            $messages.animate({ scrollTop: $messages.prop('scrollHeight') }, 300);
                        }
                    });

                }, error: function (jqXHR, err) {
                    console.log("hata");
                    var msg = '';
                    if (jqXHR.status === 0) {
                        msg = 'Ooops! internetiniz gitmiş olabilir.';
                    } else if (jqXHR.status == 404) {
                        msg = 'Requested page not found. [404]';
                    } else if (jqXHR.status == 500) {
                        msg = 'Internal Server Error [500].';
                    } else if (exception === 'parsererror') {
                        msg = 'Requested JSON parse failed.';
                    } else if (exception === 'timeout') {
                        msg = 'Time out error.';
                    } else if (exception === 'abort') {
                        msg = 'Ajax request aborted.';
                    } else {
                        msg = 'Uncaught Error.\n' + jqXHR.responseText;
                    }
                    alert(msg);
                }

            });
            }    catch (e) {
                var $messages, message;
                $('.message_input').val('');
                $messages = $('.messages');
                message_side = "right";
                message = new Message({
                    text: 'Ooops! Uzgününüm, Sorunuzu cevaplayamadım :(.',
                    message_side: message_side
                });
                message.draw();
                $messages.animate({ scrollTop: $messages.prop('scrollHeight') }, 300);
            }
        }

        sendMessage = function (text) {
            $("#loading").show();
            var $messages, message;
            if (text.trim() === '') {
                return;
            }
            $('.message_input').val('');
            $messages = $('.messages');

            message_side = message_side === 'left' ? 'right' : 'left';
            message = new Message({
                text: text,
                message_side: message_side
            });
            message.draw();
            $messages.animate({ scrollTop: $messages.prop('scrollHeight') }, 300);
            if (IlkDurumKontrol == 1) {
                SendQuestion(text);
            }

        };
        $('.send_message').click(function (e) {
            message_side = "right";
            return sendMessage(getMessageText());
        });
        $('.message_input').keyup(function (e) {
            if (e.which === 13) {
                message_side = "right";
                return sendMessage(getMessageText());
            }
        });


        var sID = new String();
        var sIpAdres = new String();
   
        var sKullaniciID = new String();
        
        IdGetir = function (text, ssDogruSoru) {

            if (sID == "" || sID == null) {
                $.ajax({

                    url: 'http://localhost:62357/api/KelimeKontrol',
                    dataType: 'json',
                    type: 'GET',
                    success: function (data1) {
                        sID = data1.ID;
                        console.log(data1);

                        var urlEkle = "https://www.oyunpuanla.com/chatbot/insert.php?userID=" + sID + "&userSoru=" + text + "&userSoruCevap=" + ssDogruSoru;

                        $.ajax({

                            url: urlEkle,
                            dataType: 'json',
                            type: 'GET',
                            success: function (data1) {
                                sKullaniciID = data1.ID;
                                console.log(data1);
                            }, error: function (jqXHR, err) {
                                console.log("Ip alırken  ajax hata meydana geldi" + err);

                            }
                        });

                    }, error: function (jqXHR, err) {
                        console.log("ID Apiden çekerken  ajax hata meydana geldi" + err);

                    }
                });

            } else {
        

                        var urlEkle = "https://www.oyunpuanla.com/chatbot/insert.php?userID=" + sID + "&userSoru=" + text;

                        $.ajax({

                            url: urlEkle,
                            dataType: 'json',
                            type: 'GET',
                            success: function (data1) {
                                sKullaniciID = data1.ID;
                                console.log(data1);
                            }, error: function (jqXHR, err) {
                                console.log("veriyi Db eklerken ajax hata meydana geldi" + err);

                            }
                        });

         
            }
        };

        //Ilk açılışta Giriş mesajı yollandı.
        setTimeout(function () {
            message_side = "left";
            sendMessage('Merhaba Ben Covid Sanal Asistanınız, Sorularınızı sormayı esirgemeyin :)');
            IlkDurumKontrol = 1;
        }, 1000);
    });
}.call(this));