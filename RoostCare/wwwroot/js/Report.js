$(function () {
    $("#button-excel").on("click", function () {
        var table_excel = new Table2Excel();
        table_excel.export($("#table-data"));
    });


    /*
    $("#button-print").on("click", function () {
        //start_loader();
        var head = $('head').clone();
        var p = $('#outprint').clone();
        var el = $('<div class="print-wrapper">');
        var pContent = $($('noscript#print-content').html()).clone();
        head.find('title').text("Incubated Reports - Print View");
        el.append(head);
        el.append(pContent);
        var nw = window.open("", "_blank", "width=1010,height=1000,top=50,left=75");
        nw.document.write(el.html());
        nw.document.close();

        // Maximize the window before printing

        nw.moveTo(0, 0);
        nw.resizeTo(screen.width, screen.height);

        setTimeout(() => {
            nw.print();
            setTimeout(() => {
                nw.close();
                end_loader();
            }, 200);
        }, 500);
    });


    */
    $("#button-print").on("click", function () {
        //start_loader();
        var head = $('head').clone();
        var p = $('#outprint').clone();
        var el = $('<div class="print-wrapper">');
        var pContent = $($('noscript#print-content').html()).clone();
        head.find('title').text("Incubated Reports - Print View");
        el.append(head);
        el.append(pContent);

        var nw = window.open("", "_blank", "width=1010,height=1000,top=50,left=75");
        nw.document.write(el.html());
        nw.document.close();

        // Maximize the window before printing
        nw.moveTo(0, 0);
        nw.resizeTo(screen.width, screen.height);

        // Zoom preview
        var zoomFactor = 0.1; // Adjust the zoom factor as needed
        var style = `
        <style>
            .print-wrapper {
                transform: scale(${zoomFactor});
                transform-origin: top left;
                width: ${100 / zoomFactor}%;
                height: ${100 / zoomFactor}%;
            }
        </style>
    `;
        $(nw.document.head).append(style);

        setTimeout(() => {
            nw.print();
            setTimeout(() => {
                nw.close();
                end_loader();
            }, 200);
        }, 500);
    });

})


