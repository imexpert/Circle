var noDataFound = "Hiçbir kayıt bulunamadı";

function createSelect(select) {
    Create(select);

    FillSelect(select);
}

function FillSelect(select, param) {
    var url = $(select).attr("url");
    var queryParam = $(select).attr("query-param");
    var dataText = $(select).attr("text-field");
    var dataValue = $(select).attr("value-field");
    var childControl = $(select).attr("child-kontrol-id");

    if (isEmpty(childControl)) {
        childControl = "#" + childControl;
        $(select).on('change', function () {
            var childSelect = $(childControl);
            Create(childSelect);
            FillSelect(childSelect, select.value);
        });
    }

    var isReturn = false;

    if (isEmpty(url)) {
        var data = null;

        if (isEmpty(queryParam)) {
            data = {
                [queryParam]: getParameterByName(queryParam)
            }
            isReturn = queryParam != undefined;
        }
        else {
            
            data = {
                [$(select).attr("param-name")]: param
            }

            isReturn = param != undefined;
        }

        if (isReturn) {
            Get(url, data).done(function (response) {
                if (response.IsSuccess) {
                    
                    $.each(response.Data, function (i, item) {
                        if (isEmpty(dataText) && isEmpty(dataValue)) {
                            if (item[dataValue] != null && item[dataValue] != "") {
                                $(select).append(new Option(item[dataText], item[dataValue]));
                            }
                            else {
                                $(select).append(new Option(item[dataText], item["Id"]));
                            }
                        }
                        else if (Object.keys(item).length > 1) {
                            $(select).append(new Option(item[Object.keys(item)[1]], item[Object.keys(item)[0]]));
                        }
                    });
                }
            });
        }
    }
}

function Create(select) {
    var parentModal = $(select).attr("parent-modal");
    var placeHolder = $(select).attr("place-holder");
    
    if (parentModal != null && parentModal != undefined) {
        parentModal = "#" + parentModal;
    }

    $(select).select2({
        dropdownParent: $(parentModal),
        "placeholder": placeHolder,
        "language": {
            "noResults": function () {
                return noDataFound;
            }
        },
        "allowClear": true,
    });

    $(select).empty();
    $(select).append(`<option value='0' selected disabled>${placeHolder}</option>`);
}

$(document).ready(function () {
    $(".customSelect").each(function (i, item) {
        createSelect(this);
    });
});