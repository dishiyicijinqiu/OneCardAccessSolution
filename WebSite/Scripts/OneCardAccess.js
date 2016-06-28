$(function () {
    $(document).ready(function () {
        $(document).contextmenu(function () {
            return false;
        });
        //$("div").select(function () {
        //    return false;
        //});
        //$("div").disableSelection();
        //$("a").disableSelection();
        $("*").attr('unselectable', 'on');
        $("input").attr('unselectable', 'off');
    });
});