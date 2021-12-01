var select2g_noDataFound = "Hiçbir kayıt bulunamadı";
var select2g_maximumSelected = "En fazla args.maximum seçim yapabilirsiniz"; // args.maximum is a variable, which select2generator will replace it a number with maximumSelectedText
var select2g_minimumInputLength = 3; // you can set a default value for minimumInputLength here
var select2g_maximumInputLength = 0; // 0 is infinite
var select2g_inputTooShortText = "Lütfen args.minimum ve üzeri karakter girin"; // args.minimum is a variable, which select2generator will replace it a number with inputTooShortText. Also args.input is the user-typed text
var select2g_inputTooLongText = "En fazla args.maximum karakter girebilirsiniz"; // args.maximum is a variable, which select2generator will replace it a number with inputTooLongText. Also args.input is the user-typed text

function select2GeneratorTryCatchFunc(thisFromReady) {
    try {
        var thisSelect = $(thisFromReady);

        if ($(thisSelect).attr("data-maximumSelectedText") == "" || $(thisSelect).attr("data-maximumSelectedText") == undefined) { $(thisSelect).attr("data-maximumSelectedText", select2g_maximumSelected); }
        if ($(thisSelect).attr("data-inputTooShortText") == "" || $(thisSelect).attr("data-inputTooShortText") == undefined) { $(thisSelect).attr("data-inputTooShortText", select2g_inputTooShortText); }
        if ($(thisSelect).attr("data-inputTooLongText") == "" || $(thisSelect).attr("data-inputTooLongText") == undefined) { $(thisSelect).attr("data-inputTooLongText", select2g_inputTooLongText); }

        var parentModal = "#" + $(thisSelect).attr("data-dropdownParent");

        $(thisSelect).select2({
            dropdownParent: $(parentModal),
            "language": $(thisSelect).attr("data-language") != undefined ? $(thisSelect).attr("data-language") : "",
            "placeholder": $(thisSelect).attr("data-placeholder") != undefined ? $(thisSelect).attr("data-placeholder") : "",
            "language": {
                "noResults": function () {
                    return $(thisSelect).attr("data-noDataFound") == undefined ? select2g_noDataFound : $(thisSelect).attr("data-noDataFound");
                },
                "maximumSelected": function (e) {
                    return $(thisSelect).attr("data-maximumSelectedText").replace("args.maximum", e.maximum);
                }
            },
            "allowClear": ($(thisSelect).attr("data-allowClear") == undefined || $(thisSelect).attr("data-allowClear") == "") ? false : true, // null or undefined => false, else anything is true! ex: a is true, asdasd is true, too!
            "maximumSelectionLength": ($(thisSelect).attr("data-maximumSelectionLength") == undefined || $(thisSelect).attr("data-maximumSelectionLength") == "0" || isNaN($(thisSelect).attr("data-maximumSelectionLength")) == true || $(thisSelect).attr("data-maximumSelectionLength") == "") ? 0 : $(thisSelect).attr("data-maximumSelectionLength")
        });
    }
    catch (err) {
        console.log(`An error occured: ${err}`);
    }
}

function generateSelect(thisFromReady) {

    var dataUrl = $(thisFromReady).attr("data-url");
    var dataText = $(thisFromReady).attr("data-text");
    var dataValue = $(thisFromReady).attr("data-value");
    var childName = $(thisFromReady).attr("data-childName");
    var queryString = $(thisFromReady).attr("data-queryString");

    $(thisFromReady).on('change', function () {
        var bolgeAdi = "";

        if ($(thisFromReady).attr("data-childName") != undefined && $(thisFromReady).attr("data-childName") != "" && $(thisFromReady).attr("data-childName") != null) {

            var childName = "#" + $(thisFromReady).attr("data-childName");

            var targetUrl = $(childName).attr("data-url");

            if (targetUrl == undefined) {
                return;
            }

            if (thisFromReady.value == null || thisFromReady.value == undefined || thisFromReady.value == "" || thisFromReady.value == "Seçiniz") {
                clearAndInitialize($(thisFromReady).attr("data-childName"), true);
                clearAndInitialize($(thisFromReady).attr("name"), false);
                return;
            }

            var data = { [$(thisFromReady).attr("data-paramName")]: thisFromReady.value };

            Get(targetUrl, data).done(function (response) {
                if (response.IsSuccess) {
                    clearAndInitialize(childName, true);

                    $.each(response.Data, function (i, item) {
                        var dataTargetSelectText = $(childName).attr("data-text");
                        var dataTargetSelectValue = $(childName).attr("data-value");

                        if ((dataTargetSelectText != "" && dataTargetSelectText != undefined) && (dataTargetSelectValue != "" && dataTargetSelectValue != undefined)) {
                            $(childName).append(new Option(item[dataTargetSelectText], item[dataTargetSelectValue]));
                        }
                        else if (Object.keys(item).length > 1) {
                            $(childName).append(new Option(item[Object.keys(item)[1]], item[Object.keys(item)[0]]));
                        }
                    });
                }
            });
        }
    });

    function clearAndInitialize(name, isEmpty) {
        var childName = "";
        if (!name.includes("#")) {
            childName = "#" + name;
        }
        else {
            childName = name;
        }

        if (isEmpty) {
            $(childName).empty();
        }

        $(childName).append(`<option value="${$(childName).attr("data-defaultOptionValue") != undefined ? $(childName).attr("data-defaultOptionValue") : $(childName).attr("data-defaultOptionText")}" disabled="disabled" selected="selected">${$(childName).attr("data-defaultOptionText")}</option>`);

        if ($(childName).attr("data-childName") != "" && $(childName).attr("data-childName") != undefined) {
            clearAndInitialize($(childName).attr("data-childName"), true);
        }
    }

    if ($(thisFromReady).attr("data-defaultOptionText") != undefined) {
        $(thisFromReady).append(`<option value="${$(thisFromReady).attr("data-defaultOptionValue") != undefined ? $(thisFromReady).attr("data-defaultOptionValue") : $(thisFromReady).attr("data-defaultOptionText")}" disabled="disabled" selected="selected">${$(thisFromReady).attr("data-defaultOptionText")}</option>`);
    }

    if (dataUrl != undefined && dataUrl != "") {

        var data = null;

        if (queryString != null) {
            data = {
                [queryString]: getParameterByName(queryString)
            }
        }
        Get(dataUrl, data).done(function (response) {
            if (response.IsSuccess) {

                $(thisFromReady).append(new Option("", ""));

                $.each(response.Data, function (i, item) {
                    if ((dataText != "" && dataText != undefined) && (dataValue != "" && dataValue != undefined)) {
                        if (item[dataValue] != null && item[dataValue] != "") {
                            $(thisFromReady).append(new Option(item[dataText], item[dataValue]));
                        }
                        else {
                            $(thisFromReady).append(new Option(item[dataText], item["Id"]));
                        }
                    } // If json returns values with data-text and data-value values
                    else if (Object.keys(item).length > 1) {
                        $(thisFromReady).append(new Option(item[Object.keys(item)[1]], item[Object.keys(item)[0]]));
                    } // If json returns more than one key and no data-value or data-text; we returns value with first key of object and text with second key of object
                    else {/* If something happenes wrong, we returns no data found message without any data or option. And you can modify the no data found message with data-nodatafound="your custom no data found message". If you do not define a custom message, "select2Generator_noDataFound" variable will be shown. */ }
                });

                select2GeneratorTryCatchFunc(thisFromReady);
            }
        });
    }
}

$(document).ready(function () {
    $(".customSelect").each(function (i, item) {
        generateSelect(this);
    });
});