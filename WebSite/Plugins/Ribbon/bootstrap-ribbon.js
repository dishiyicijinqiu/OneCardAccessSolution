$(function () {
    $(".ribbon .itemcontent-item.ribbonbutton").click(function () {
        $(this).trigger("buttonclick", $(this).attr("data-target"));
    });
});