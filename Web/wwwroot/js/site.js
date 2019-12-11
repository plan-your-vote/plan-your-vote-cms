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

function removeDate(dateIndex) {
  $("#removedDates").val($("#removedDates").val() + dateIndex + ",");
  $(".date-group")[dateIndex].classList.add("hide");
}

function removeDetail(detailIndex) {
  $("#removedDetails").val($("#removedDetails").val() + detailIndex + ",");
  $(".detail-group")[detailIndex].classList.add("hide");
}

function removeContact(contactIndex) {
  $("#removedContacts").val($("#removedContacts").val() + contactIndex + ",");
  $(".contact-group")[contactIndex].classList.add("hide");
}

(function($) {
  "use strict";
  $(document).ready(function() {
    tinymce.init({
      selector: ".rich-text-editor",
      editor_selector: "rte",
      menubar: "edit view format tools",
        browser_spellcheck: true,
    });
  });
})(this.jQuery);

(function($) {
  "use strict";
  $(document).ready(function() {
    setTimeout(function() {
      $(".mce-notification-warning").remove();
    }, 1000);
  });
})(this.jQuery);
