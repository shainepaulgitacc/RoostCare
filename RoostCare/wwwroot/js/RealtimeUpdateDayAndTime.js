
function updateRemainingTimes() {
    var now = new Date();

    $('.remaining-time').each(function () {
        var $element = $(this);
        var completeDate = new Date($element.data('complete'));
        var remainingTime = completeDate - now;
        console.log("complete time");
        console.log(completeDate);
        if (remainingTime > 0) {
            var remainingDays = Math.floor(remainingTime / (1000 * 60 * 60 * 24));
            var remainingHours = Math.floor((remainingTime % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var remainingMinutes = Math.floor((remainingTime % (1000 * 60 * 60)) / (1000 * 60));

            var remainingDaysString = remainingDays > 1 ? `${remainingDays}days` : `${remainingDays}day`;
            var remainingHoursString = remainingHours > 1 ? `${remainingHours}hrs` : `${remainingHours}hr`;
            var remainingMinutesString = remainingMinutes > 1 ? `${remainingMinutes}mins` : `${remainingMinutes}min`;

            $element.text(`${remainingDaysString}, ${remainingHoursString}, ${remainingMinutesString}`);
        } else {
            $element.text("Completed");
        }
    });
}
setInterval(updateRemainingTimes, 60000); // Update every minute
$(document).ready(updateRemainingTimes);