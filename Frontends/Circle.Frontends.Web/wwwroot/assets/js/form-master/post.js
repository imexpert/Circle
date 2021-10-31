//Linkteki data1,data2,data3,data4,data5 alanlarından oluşan linki çıkarır.
function GetUrlEk(item) {
    var urlEk = '';
    if (!($(item).data("data1") === undefined || $(item).data("data1") === '' || $(item).data("data1") === null)) {
        urlEk += '/' + $(item).data("data1");
    }

    if (!($(item).data("data2") === undefined || $(item).data("data2") === '' || $(item).data("data2") === null)) {
        urlEk += '/' + $(item).data("data2");
    }

    if (!($(item).data("data3") === undefined || $(item).data("data3") === '' || $(item).data("data3") === null)) {
        urlEk += '/' + $(item).data("data3");
    }

    if (!($(item).data("data4") === undefined || $(item).data("data4") === '' || $(item).data("data4") === null)) {
        urlEk += '/' + $(item).data("data4");
    }

    if (!($(item).data("data5") === undefined || $(item).data("data5") === '' || $(item).data("data5") === null)) {
        urlEk += '/' + $(item).data("data5");
    }

    //Bu alan kullanılmıyor. Önemli bir kod!!!
    //for (var pair of formData.entries()) {
    //    urlEk += "/" + pair[1];
    //}

    return urlEk;
}

//tab,modal,url,post
function ButtonExecute(buttontype, formID, item, formData, successfunction_, errorfunction_, isUrlData, content_) {

    var url = "";
    //All
    if (isUrlData === "true") {
        url = $(item).data("url") + GetUrlEk(item);
    }
    else {
        url = $(item).data("url");
    }

    //Tab
    var title = $(item).data("title");

    if (buttontype === "modal") {
        $.get(url, function (result, status) {
            $('#dvMdlDialog').html(result);
            $('#mdl').modal('show');
            successfunction_(result);
        });
    }
    else if (buttontype === "url") {
        window.location.href = url;
        successfunction_(null);
    }
    else if (buttontype === "post") {
        if (!(formID === undefined || formID === '' || formID === null)) {
            $(formID + ' input').each(function () {
                if ($(this).attr('type') === 'checkbox')
                    formData.append($(this).attr('name'), $(this).is(":checked"));
                else if ($(this).attr('type') === 'radio') {
                    if (formData.get($(this).attr('name')) === null && $(this).is(":checked") === true) {
                        formData.append($(this).attr('name'), $(this).val());
                    }
                }
                else
                    formData.append($(this).attr('name'), $(this).val());
            });

            $(formID + ' textarea').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });

            $(formID + ' select').each(function () {
                formData.append($(this).attr('name'), $(this).val());
            });

            $(formID + ' input[type=file]').each(function () {
                if ($(this)[0].hasAttribute("multiple")) {//Kaan Kandemir
                    var files = $(this)[0].files;
                    for (var i = 0; i < files.length; i++) {
                        formData.append($(this).attr('name'), files[i]);
                    }
                }
                else {
                    formData.append($(this).attr('name'), $(this)[0].files[0]);
                }
            });
        }

        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                loader("show");
            },
            success: function (result) {
                //MessageBoxShow("success", "Mesajiniz Var!", "Isleminiz basariyla tamamlandi");
                loader("hide");
                successfunction_(result);
            },
            error: function (result) {
                //MessageBoxShow("error", "Mesajiniz Var!", "Isleminiz gerceklestirilirken bir hata meydana geldi");
                loader("hide");
                errorfunction_(result);
            }
        });
    }
    else if (buttontype === "partial") {
        $.ajax({
            type: 'POST',
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                //loader("show");
            },
            success: function (result) {
                //loader("hide");
                $(content_).html(result);
                successfunction_(result);
            },
            error: function () {
                //loader("hide");
                errorfunction_();
            }
        });
    }
}



function loader(mode) {
    if (mode === "show") {
        $('#preloader').css('display', 'block');
        $('#preloader').css('z-index', '99999');

        $('#wrapper').css('filter', 'alpha(opacity=100)');
        $('#wrapper').css('opacity', '0.2');
    }
    else {
        $('#preloader').css('display', 'none');

        $('#wrapper').css('filter', 'none');
        $('#wrapper').css('opacity', '1');

    }
}