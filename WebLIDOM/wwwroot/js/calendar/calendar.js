
const options = {
    settings: {
        visibility: {
            theme: 'dark',
        },
    },
    popups: popup,
    actions: {
        clickDay(event, dates) {

            generateHourOption(selector4, dates[0] + "T");
            var popup = document.getElementById("popup");
            popup.style.display = "flex";
        }
    }
}

document.addEventListener('DOMContentLoaded', () => {
    const calendar = new VanillaCalendar('#calendar', options);
    calendar.init();
});