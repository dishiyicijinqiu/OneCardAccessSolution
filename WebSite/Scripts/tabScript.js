
function resetTabs(obj) {
    $(obj).parent().parent().next("div").find("div").hide();
    $(obj).parent().parent().find("li").removeClass("current");
}
function adjustTabWidth(holder) {
    var tabsel = $(holder).find(".tabs");
    var tabrightwidth = $(tabsel).width();
    $(tabsel).children().each(function () {
        if ($(this).hasClass('tabright')) {
        }
        else {
            tabrightwidth = tabrightwidth - $(this).outerWidth(true);
        }
    });
    var tabright = $(holder).find(".tabs .tabright");
    var iospacewidth = $(tabright).outerWidth(true) - $(tabright).width();
    tabrightwidth = tabrightwidth - iospacewidth;
    $(tabright).css("width", tabrightwidth + "px");
}
function ActiveFirst(holder) {
    $(holder).find(".tabcontent > div").hide();
    $(holder).find(".tabs").each(function () {
        $(this).find(".tabTitle:first").addClass("current");
    });
    $(holder).find(".tabcontent").each(function () {
        $(this).find("div:first").fadeIn();
    });
}
function loadTab(holder) {

    $(window).bind("resize", function () {
        adjustTabWidth(holder);
    });

    ActiveFirst(holder);

    $(holder).find(".tabs span").on("click", function (e) {
        e.preventDefault();
        if ($(this).parent().attr("class") == "current") {
            return;
        } else {
            resetTabs(this);
            $(this).parent().addClass("current");
            $($(this).attr("name")).fadeIn();
        }
    });


    $(holder).find(".tabs .tabclose").on("click", function (e) {
        e.preventDefault();
        $(this).parent().parent().remove();
        $($(this).parent().attr("name")).remove();

        ActiveFirst(holder);

    });

    adjustTabWidth(holder);
}

