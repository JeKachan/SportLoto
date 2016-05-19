$(function () {
    var $tiles = $("#tiles center").fadeTo(0, 0);
    $(window).scroll(function (d, h) {
        $tiles.each(function (i) {
            a = $(this).offset().top + $(this).height();
            b = $(window).scrollTop() + $(window).height();
            if (a < b) $(this).fadeTo(500, 1);
        });
    });
    $("#kayitbuton").click(function () {
        $("#bs-example-navbar-collapse-1").removeClass("in");
    });
})

