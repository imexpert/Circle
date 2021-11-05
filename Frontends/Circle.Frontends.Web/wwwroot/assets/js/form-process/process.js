$(document).ready(function () {
    $("body").on("click", "a[data-mode]", function () {
        var mode = $(this).data("mode");
        var formData = new FormData();
        var buton = $(this);

        if (mode === "GetGroupFormModal") {
            $(buton).data('url', "/Admin/Groups/GetGroupFormModal");
            formData.append("ID", $(buton).data("data1"));
            ButtonExecute("partial", "", buton, formData, function () {
                $("#mdl").modal("show");
            }, function () { }, "false", "#dvMdlDialog");
        }
        else if (mode === "GroupIslem") {
            //if ($(buton).data("url") === "/Admin/Groups/DeleteGroup/") formData.append("ID", $(buton).data("data1"));
            var roleList = "";
            $('input[data-mode=role]').each(function () {
                if (this.checked)
                    roleList += $(this).val() + ",";
            });
            formData.append("roleList", roleList);

            //if (FormValidate(buton)) {
            ButtonExecute("post", "#mdlForm", buton, formData, function (result) {
                console.log("result.IsSuccess---" + result.IsSuccess)
                console.log("result.StatusCode---" + result.StatusCode)
                console.log("result.RecordsTotal---" + result.RecordsTotal)
                console.log("result.RecordsFiltered---" + result.RecordsFiltered)
                console.log("result.Data---" + result.Data)
                if (result.IsSuccess) {
                    location.reload();
                }
                else {
                    //alert(result.split("|")[1]);
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
                        if (result.IsSuccess) {
                            Swal.fire(result.Message, '', 'success');
                            location.reload();
                        }
                        else {
                            Swal.fire(result.Message, '', 'warning');
                        }

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