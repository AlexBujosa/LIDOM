const newOption = (value, text) => {
    var option = document.createElement("option");

    option.value = value;
    option.text = text;

    return option;
}

const selectedOption = (selectorValue, options, anotherSelector, selectorCurrent) => {

    if (selectorCurrent.selected) {
        for (var i = 0; i < options.length; i++) {
            if (selectorCurrent.value === parseInt(options[i].value)) {
                anotherSelector.insertBefore(options[i], anotherSelector.children[i + 1])
            }
        }
    }
    
    if (selectorValue === 0) {
        selectorCurrent.value = 0;
        selectorCurrent.selected = false;
        return;
    }
 
    for (var i = 0; i < options.length; i++) {
        if (selectorValue === parseInt(options[i].value)) {
            anotherSelector.remove(i + 1);
            selectorCurrent.value = selectorValue;
            selectorCurrent.selected = true;
            return;
        }
    }

}

const stadiumOption = (baseballTeamSelected, stadiumSelector) => {
    var stadiumOptions = [];

    //Remove all child elements skipping first element
    while (stadiumSelector.childElementCount > 1) {
        stadiumSelector.remove(1);
    }
    console.log(stadiumSelector.childElementCount);
    //Checking if there is an option selected on first team selector
    if (Object.keys(baseballTeamSelected[0]).length != 0) {
        stadiumOptions.push(baseballTeamSelected[0]);
    }

     //Checking if there is an option selected on first team selector
    if (Object.keys(baseballTeamSelected[1]).length != 0) {
        stadiumOptions.push(baseballTeamSelected[1]);
    }

    //Creating Stadio selector depending on what lidom team you choose
    for (var i = 0; i < stadiumOptions.length; i++) {
        var stadiumOption = newOption(stadiumOptions[i].home, stadiumOptions[i].home);
        stadiumSelector.appendChild(stadiumOption);
    }
}


const generateHourOption = (hourSelectorCurrent, date) => {
    var gameTimes = ["17:30:00", "18:30:00", "19:00:00"];
 
    for (var i = 1; i < hourSelectorCurrent.childElementCount; i++) {
        hourSelectorCurrent.remove(i);
    }

    for (var i = 0; i < gameTimes.length; i++) {
        var optionHome = newOption(date+gameTimes[i], gameTimes[i]);
        hourSelectorCurrent.appendChild(optionHome);
    }
}