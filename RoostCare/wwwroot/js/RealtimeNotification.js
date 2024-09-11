var connection = new signalR.HubConnectionBuilder().withUrl('/notificationhub').build();
connection.start().then(function () {
    console.log("Notification hub connected");
}).catch(function (err) {
    console.error("error");
});

$(function () {
    connection.on("notificationAlert", function (count) {   
        $("#notif-icon").append(`<span class="p-1 rounded-circle bg-danger text-white position-absolute" style="top: -5px; right: -5px"></span>`);
    });
});
