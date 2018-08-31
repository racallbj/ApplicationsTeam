$(document).ready(readyFunction);

function readyFunction(jQuery) {
    $("#divPleaseWait").hide().css("visibility", "hidden");

    var effectiveDate = $("#txtEffectiveDate");
    var currentDate = new Date();

    var newdate = $.datepicker.formatDate("mm/dd/yy", new Date());
    effectiveDate.val(newdate);

    effectiveDate.datepicker({
        dateFormat: "mm/dd/yy",
        startDate: currentDate
    });
}

function HideCellphoneStipendDiv() {
    var cellphoneStipendDiv = $("#divCellPhoneStipend");
    var pleaseWaitDiv = $("#divPleaseWait");

    if (Page_ClientValidate()) {
        cellphoneStipendDiv.hide().css("visibility", "hidden");
        pleaseWaitDiv.show().css("visibility", "visible");
    }
}