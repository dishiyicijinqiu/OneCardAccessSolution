//-------------ViewBody
//外层内高度-固定(div)外高度-(自适应(div)内外间距)=自适应(div)高度
function adjustHeight() {
    var cheight = $(document).innerHeight() - $("#ViewBody_Header").outerHeight(true) - $("#ViewBody_Footer").outerHeight(true);
    var iospaceheight = $("#ViewBody_Content").outerHeight(true) - $("#ViewBody_Content").innerHeight();
    $("#ViewBody_Content").css("height", cheight + "px");
}
//-------------ContentsPartialView
function adjustContentHeight() {
    var iospaceheight = $("#navmenu").outerHeight(true) - $("#navmenu").innerHeight();
    var cheight = $("#tabs").innerHeight() - $("#navmenu").outerHeight(true) - iospaceheight;
    $("#tab-content").css("height", cheight + "px");
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


