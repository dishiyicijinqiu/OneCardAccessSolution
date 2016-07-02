//-------------ViewBody
//外层内高度-固定(div)外高度-(自适应(div)内外间距)=自适应(div)高度
function adjustHeight() {
    var iospaceheight = $("#ViewBody_Content").outerHeight(true) - $("#ViewBody_Content").height();
    var cheight = $(window).height() - $("#ViewBody_Header").outerHeight(true) - $("#ViewBody_Footer").outerHeight(true) - iospaceheight;
    $("#ViewBody_Content").css("height", cheight + "px");
}
//-------------ContentsPartialView
function adjustContentHeight() {
    var iospaceheight = $("#tabcontent").outerHeight(true) - $("#tabcontent").height();
    var cheight = $("#ViewContent").height() - $("#tabmenu").outerHeight(true) - iospaceheight;
    $("#tabcontent").css("height", cheight + "px");
}
//-------------FooterPartialView
function OnRibbonResize(s, e) {
    adjustHeight();
    adjustContentHeight();
}

$(function () {
    //-------------ViewBody
    $(window).bind("resize", function () {
        adjustHeight();
    });
    //$(window).resize(function () {
    //    adjustHeight();
    //});
    adjustHeight();

    //-------------ContentsPartialView
    setTimeout(function () {
        adjustContentHeight();
    }, 10);
    $(window).bind("resize", function () {
        adjustContentHeight();
    });
});


