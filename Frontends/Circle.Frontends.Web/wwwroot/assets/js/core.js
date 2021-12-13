var url;

function PostForm(methodName, formName) {
    url = methodName;
    return $.ajax({
        url: url,
        type: "POST",
        data: $("#" + formName).serialize(),
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        cache: false,
        error: handleError,
    });
}

function PostFormWithFile(methodName, formData) {
    url = methodName;
    return $.ajax({
        url: url,
        type: "POST",
        data: formData,
        dataType: 'json',
        contentType: false,
        processData: false,
        error: handleError,
    });
}

function PostData(methodName, data) {
    url = methodName;
    return $.ajax({
        url: url,
        type: "POST",
        data: data,
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        cache: false,
        error: handleError,
    });
}

function Post(methodName, data) {
    return $.ajax({
        url: methodName,
        type: 'POST',
        data: data,
        error: handleError,
    });
}

function Get(Url, Data) {
    url = Url;
    return $.ajax({
        url: Url,
        type: "GET",
        data: Data,
        beforeSend: function (xhr) {
            if (localStorage.getItem("token") != undefined) {
                xhr.setRequestHeader('Authorization', "Bearer " + localStorage.getItem("token"));
            }
        },
        dataType: 'json',
        contentType: "application/json",
        cache: false,
        error: handleError,
    });

}

function handleError(err) {
    if (err.status == 404) {
        ShowErrorMessage("Method bulunamadı - " + url);
    }
    else {
        ShowErrorMessage(err.responseText + " - " + url);
    }
}

function ShowSuccessMessage(message) {

    var customTitle = "Başarılı";
    var customButtonText = "Tamam";

    var cook = readCookie('.AspNetCore.Culture');

    if (cook != null && cook.endsWith('en-US')) {
        customTitle = "Success";
        customButtonText = "Ok";
    }

    swal.fire({
        title: customTitle,
        html: message,
        icon: "success",
        allowOutsideClick: false,
        allowEscapeKey: true,
        buttonsStyling: false,
        confirmButtonText: customButtonText,
        customClass: {
            confirmButton: "btn font-weight-bold btn-light-primary"
        }
    }).then(function () {

        KTUtil.scrollTop();
    });
}

function ShowSuccessMessage(message, url) {

    var customTitle = "Başarılı";
    var customButtonText = "Tamam";

    var cook = readCookie('.AspNetCore.Culture');

    if (cook != null && cook.endsWith('en-US')) {
        customTitle = "Success";
        customButtonText = "Ok";
    }

    swal.fire({
        title: customTitle,
        html: message,
        icon: "success",
        allowOutsideClick: false,
        allowEscapeKey: true,
        buttonsStyling: false,
        confirmButtonText: customButtonText,
        customClass: {
            confirmButton: "btn font-weight-bold btn-light-primary"
        }
    }).then(function () {
        window.location = url;
    });
}

function readCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function ShowErrorMessage(message) {
    var customTitle = "Hata Oluştu";
    var customButtonText = "Tamam";

    var cook = readCookie('.AspNetCore.Culture');

    if (cook != null && cook.endsWith('en-US')) {
        customTitle = "Error Occured";
        customButtonText = "Ok";
    }

    swal.fire({
        title: customTitle,
        html: message,
        icon: "error",
        allowOutsideClick: false,
        allowEscapeKey: false,
        buttonsStyling: false,
        confirmButtonText: customButtonText,
        customClass: {
            confirmButton: "btn font-weight-bold btn-light-primary"
        }
    }).then(function () {

        KTUtil.scrollTop();
    });
}

function showModal(name) {
    $('#' + name).modal('show');
}

$(document).ready(function () {
    var target = document.getElementById("kt_body");

    var blockUI = new KTBlockUI(target, {
        message: '<div class="blockui-message"><span class="spinner-border text-primary"></span> Loading...</div>',
    });

    $(document).ajaxStart(function () {
        console.log("geldi");
        blockUI.block();
    });
    $(document).ajaxComplete(function () {
        //blockUI.release();
    });
});

function setLanguage(lang) {
    var data = {
        "Culture": lang,
        "ReturnUrl": window.location.href + "?culture=" + lang
    };

    Post("/Base/SetLanguage", data).done(function (response) {
        window.location.href = window.location.origin + window.location.pathname + "?culture=" + lang;
    });

}

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function isEmpty(val) {
    return !!val;
}