/* Views helper functions */
function getCount(selector) {
    return $(selector).length;
}

function removeOption(optionIndex) {
    var optionInputs = $(".option-text");

    if (optionInputs.length > optionIndex) {
        optionInputs[optionIndex].value = "";
        $(".option")[optionIndex].classList.add("hide");
    }
}