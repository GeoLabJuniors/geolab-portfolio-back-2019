$("#search").click(function () {
    $("#searchdiv").css({ "display": "block" });
    $(".close").css({ "display": "block" });
    $(this).css({ "display": "none" });
    $('input.search-input').focus();
});

$(".close").click(function () {
    $("#searchdiv").css({ "display": "none" });
    $("#search").css({ "display": "inline-block" });
    $(this).css({ "display": "none" });

});

$(".tags").click(function () {
    $(".alltag").css({ "display": "block" });
    $(this).css({ "display": "none" });

});

jssor_1_slider_init = function () {

    var jssor_1_options = {
        $AutoPlay: 1,
        $SlideWidth: 720,
        $ArrowNavigatorOptions: {
            $Class: $JssorArrowNavigator$
        },
        $BulletNavigatorOptions: {
            $Class: $JssorBulletNavigator$
        }
    };

    var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

    /*#region responsive code begin*/

    var MAX_WIDTH = 980;

    function ScaleSlider() {
        var containerElement = jssor_1_slider.$Elmt.parentNode;
        var containerWidth = containerElement.clientWidth;

        if (containerWidth) {

            var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);

            jssor_1_slider.$ScaleWidth(expectedWidth);
        }
        else {
            window.setTimeout(ScaleSlider, 30);
        }
    }

    ScaleSlider();

    $Jssor$.$AddEvent(window, "load", ScaleSlider);
    $Jssor$.$AddEvent(window, "resize", ScaleSlider);
    $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
    /*#endregion responsive code end*/
};
jssor_1_slider_init();