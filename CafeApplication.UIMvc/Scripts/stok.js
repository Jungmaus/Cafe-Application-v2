
$(document).ready(function () {

    var tikUrunList = 0;
    var tikUrunEkle = 0;
    $("div#divUrunEkleStok").hide();
    $("div#divStokDurumu").hide();

    $("button#btnUrunList").click(function () {
        tikUrunList++;
        if (tikUrunList % 2 == 0) {
            $("div#divStokDurumu").hide(1000);
            $("span#spanList").attr("class", "glyphicon glyphicon-arrow-down");
        } else if (tikUrunList % 2 == 1) {
            $("div#divStokDurumu").show(1000);
            $("span#spanList").attr("class", "glyphicon glyphicon-arrow-up");
        }
    });

    $("button#btnUrunEkle").click(function () {
        tikUrunEkle++;

        if (tikUrunEkle % 2 == 0) {
            $("div#divUrunEkleStok").hide(1000);
            $("span#spanEkle").attr("class", "glyphicon glyphicon-arrow-down");
        } else if (tikUrunEkle % 2 == 1) {
            $("div#divUrunEkleStok").show(1000);
            $("span#spanEkle").attr("class", "glyphicon glyphicon-arrow-up");
        }

    });

});
