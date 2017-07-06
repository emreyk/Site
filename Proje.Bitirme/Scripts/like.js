$(function () {


    var noteids = [];

    //sayfadaki notların id lerini dizi olarak al
    //bu dive sahip not id lerde gez
    $("div[data-note-id]").each(function (i, e) {

        //dizeye ekle
        noteids.push($(e).data("note-id"));

    });


    //dizi olarak data gönder
    $.ajax({

        method: "POST",

        url: "/Note/GetLiked",

        data: { ids: noteids }

    }).done(function (data) { //başarılıysa sonuc



        if (data.result != null && data.result.length > 0) {

            for (var i = 0; i < data.result.length; i++) {

                var id = data.result[i];

                //div olup data id si gelen id ye eşit olan divi bul yani o notu bul
                var likedNote = $("div[data-note-id=" + id + "]");

                //butonu bul
                var btn = likedNote.find("button[data-liked]");

                
                var span = btn.find("span.like-star");


                //begenildi true çek
                btn.data("liked", true);

                //içi boş olanı sil
                span.removeClass("glyphicon-star-empty");

                //içi dolu olanı ekle
                span.addClass("glyphicon-star");

            }



        }



    }).fail(function () {



    });




    //-----------------------------------------


    $("button[data-liked]").click(function () {

        var btn = $(this);

        var liked = btn.data("liked");

        var noteid = btn.data("note-id");
        //classında like star olan span
        var spanStar = btn.find("span.like-star");

        var spanCount = btn.find("span.like-count");



        console.log(liked);

        console.log("like count : " + spanCount.text());


        //clikc gerceklesti controllera ajax isteği gönder
        $.ajax({

            method: "POST",

            url: "/Note/SetLikeState",

            data: { "noteid": noteid, "liked": !liked } //note id ve like olup olmadıgı true false gönder
            //!liked tersine like ise unlike yap

        }).done(function (data) {



            console.log(data);



            if (data.hasError) {

                alert(data.errorMessage);

            } else {

                liked = !liked;

                btn.data("liked", liked);

                spanCount.text(data.result);



                console.log("like count(after) : " + spanCount.text());





                spanStar.removeClass("glyphicon-star-empty");

                spanStar.removeClass("glyphicon-star");



                if (liked) {

                    spanStar.addClass("glyphicon-star");

                } else {

                    spanStar.addClass("glyphicon-star-empty");

                }



            }



        }).fail(function () {

            alert("Sunucu ile bağlantı kurulamadı.");

        })



    });

});