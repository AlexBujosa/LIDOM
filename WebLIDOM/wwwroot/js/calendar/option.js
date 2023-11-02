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

const stadiosOption = (stadios1, stadios2, selector) => {
    var options = [];

    for (var i = 1; i < selector.childElementCount; i++) {
        selector.remove(i);
    }
    if (stadios1.length !== 0) {
        options.push(stadios1);
    }

    if (stadios2.length !== 0) {
        options.push(stadios2);
    }
    console.log(options);
  

    for (var i = 0; i < options.length; i++) {
        var optionHome = newOption(options[i].home, options[i].home);
        selector.appendChild(optionHome);
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