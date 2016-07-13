



jQuery.fn.extend({
    addTab: function (args) {
        
      //var   <li role='presentation' class='active'>
      //      <a href='#home1' aria-controls='home' role='tab' data-toggle='tab'>Home</a>
      //      <i class='close-tab glyphicon glyphicon-remove' style='display:block;'></i>
      //  </li>

    }
});
 //调用jQuery对象方法
//$.fn.extend({
//    addTab: function (args) {
//        alert(args);
//    }
//});
$(function () {
    var activeTab = function (e) {
        $(e).parent().parent().children().each(function () {
            $(this).find('.close-tab').each(function () {
                $(this).css("display", "none");
            });
        });
        $(e).next().css("display", "block");
    }

    $('.tabs .nav-tabs>li>a[data-toggle="tab"]').on('show.bs.tab', function (e) {
        activeTab(e.target);
    });

    $('.tabs .nav-tabs>li:has(a[data-toggle="tab"])').on('mouseover', function (e) {
        if (!$(this).hasClass("active")) {
            $(this).find('.close-tab').each(function () {
                $(this).css("display", "block");
            });
        }
    });

    $('.tabs .nav-tabs>li:has(a[data-toggle="tab"])').on('mouseout', function (e) {
        if (!$(this).hasClass("active")) {
            $(this).find('.close-tab').each(function () {
                $(this).css("display", "none");
            });
        }
    });

    $('.tabs .close-tab').on('click', function (e) {
        var pel = $(this).parent();
        if ($(pel).hasClass("active")) {
            

            var prev = $(this).parent().prev();
            if ($(prev).length > 0){
                $(prev).find('a[role="tab"]').each(function () {
                    $(this).tab('show');
                });
            } else {
                var next = $(this).parent().next();
                if ($(next).length > 0) {
                    $(next).find('a[role="tab"]').each(function () {
                        $(this).tab('show');
                    });
                }
            }

        }

        $($(this).prev().attr("href")).remove();
        $(this).parent().remove();
    });

});
//$.fn.extend({
//    addTab: function () {
//        alert("aiyao");
//    }
//});


//$.fn.extend({
//    addTab: function () {
//        return this.each(function () {
//            alert("aiyao")
//        });
//    }
//});