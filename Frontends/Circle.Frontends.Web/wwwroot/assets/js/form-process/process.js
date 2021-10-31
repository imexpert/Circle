$(document).ready(function () {
    $("body").on("click", "a[data-mode]", function () {
        var mode = $(this).data("mode");
        var formData = new FormData();
        var buton = $(this);

        if (mode === "GetGroupFormModal") {
            $(buton).data('url', "/Admin/Groups/GetGroupFormModal");
            formData.append("ID", $(buton).data("data1"));
            ButtonExecute("partial", "", this, formData, function () {
                $("#mdl").modal("show");
            }, function () { }, "false", "#dvMdlDialog");
        }
        //else if (mode === "GroupIslem") {
        //    $(buton).data('url', "/Admin/Groups/GroupIslem/");
        //    formData.append("ID", $(buton).data("data1"));
        //    formData.append("type", $(buton).data("type"));
        //    if (FormValidate(buton)) {
        //        ButtonExecute("post", "#mdlForm", buton, formData, function (result) {
        //            if (result.split("|")[0] === "Ok") {
        //                location.reload();
        //            }
        //            else {
        //                alert(result.split("|")[1]);
        //            }
        //        }, function () { }, "false", "");
        //    }
        //}
        else if (mode === "GroupIslem") {
            //$(buton).data('url', "/Admin/Groups/GroupIslem/");
            if ($(buton).data("url") === "/Admin/Groups/DeleteGroup/") formData.append("ID", $(buton).data("data1"));

            var roleList = "";
            $('input[data-mode=role]').each(function () {
                if (this.checked)
                    roleList += $(this).val() + "#" + $(this).data("data1") + ",";
            });
            formData.append("roleList", roleList);

            //if (FormValidate(buton)) {
            ButtonExecute("post", "#mdlForm", buton, formData, function (result) {
                if (result.split("|")[0] === "Ok") {
                    location.reload();
                }
                else {
                    alert(result.split("|")[1]);
                }
            }, function () { }, "false", "");
            //}
        }
        else if (mode === "GetGroupSilModal") {
            var id = $(buton).data("data1");
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#d33',
                cancelButtonColor: '#3085d6',
                confirmButtonText: 'Sil'
            }).then((result) => {
                if (result.isConfirmed) {
                    $(buton).data('url', "/Admin/Groups/DeleteGroup/");
                    formData.append("ID", id);
                    ButtonExecute("post", "#mdlForm", buton, formData, function (result) {
                        Swal.fire('Silme işlemi tamamlandı!', '', 'success');
                    }, function (result) {
                        Swal.fire("Bir hata oluştu!" + result, 'warning');
                    }, "false", "");
                }
            });

            //$(buton).data('url', "/Admin/Groups/GetGroupSilModal/");
            //formData.append("ID", $(buton).data("data1"));
            //ButtonExecute("partial", "#", buton, formData, function () {
            //    $("#mdl").modal("show");
            //}, function () { }, "false", "#dvMdlDialog");
        }



    });
});