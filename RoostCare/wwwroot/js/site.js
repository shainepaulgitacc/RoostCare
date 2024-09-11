new DataTable('#table-data');
new DataTable('#table-data2');

$(function () {
    $("#menu-toggle").on("click", function () {
        console.log("hello");
        $("#sidebar").toggleClass("sidebar-hide");
        $("#sidebar").toggleClass("sidebar-show");
    })
    let menus = $(".menus");
    menus.each(function () {
        // Use .on() to attach the click event
        $(this).on("click", function () {
            if (!$(this).hasClass("active")) {
                menus.removeClass("active");
            }
            $(this).toggleClass("active");
        });
    });

    $("#alert-close").on("click", function () {
        $("#alert").removeClass("active-alert");
    })

})


