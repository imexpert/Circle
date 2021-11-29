(() => {
    "use strict";
    var __webpack_exports__ = {};
   
    var KTModalNewCard = function () {
        var submitButton;
        var cancelButton;
        var validator;
        var form;
        var modal;
        var modalEl;

        // Handle form validation and submittion
        var handleForm = function () {
            // Stepper custom navigation

            var cook = readCookie('.AspNetCore.Culture');

            var titleMessage = "Bos olamaz.";

            if (cook != null && cook.endsWith('en-US')) {
                titleMessage = "Cannot be empty.";
            }

            validator = FormValidation.formValidation(
                form,
                {
                    fields: {
                        
                    },

                    plugins: {
                        trigger: new FormValidation.plugins.Trigger(),
                        bootstrap: new FormValidation.plugins.Bootstrap5({
                            rowSelector: '.fv-row',
                            eleInvalidClass: '',
                            eleValidClass: ''
                        })
                    }
                }
            );

            // Action buttons
            submitButton.addEventListener('click', function (e) {
                // Prevent default button action
                e.preventDefault();

                // Validate form before submit
                if (validator) {
                    validator.validate().then(function (status) {
                        console.log('validated!');

                        if (status == 'Valid') {
                            // Show loading indication
                            submitButton.setAttribute('data-kt-indicator', 'on');

                            // Disable button to avoid multiple click 
                            submitButton.disabled = true;

                            var formData = new FormData(form);

                            var totalFiles = document.getElementById('imgAvatar').files.length;
                            for (var i = 0; i < totalFiles; i++) {
                                var file = document.getElementById('imgAvatar').files[i];
                                formData.append("Image", file);
                            }

                            PostFormWithFile("/Admin/Users/AddUser", formData).done(function (response) {
                                if (response.IsSuccess) {
                                    ShowSuccessMessage(response.Message, "/Admin/Users/List");
                                }
                                else {
                                    // Show loading indication
                                    submitButton.setAttribute('data-kt-indicator', 'off');

                                    // Disable button to avoid multiple click 
                                    submitButton.disabled = false;

                                    ShowErrorMessage(response.Message);
                                }
                            });
                        }
                    });
                }
            });

            cancelButton.addEventListener('click', function (e) {
                e.preventDefault();
                modal.hide(); // Hide modal	
            });
        }

        return {
            // Public functions
            init: function () {
                // Elements
                modalEl = document.querySelector('#modalUser_form');

                if (!modalEl) {
                    return;
                }

                modal = new bootstrap.Modal(modalEl);

                form = document.querySelector('#modalUser_form');
                submitButton = document.getElementById('modalUser_submit');
                cancelButton = document.getElementById('modalUser_cancel');

                handleForm();
            }
        };
    }();

    // On document ready
    KTUtil.onDOMContentLoaded(function () {
        console.log("geldi");
        KTModalNewCard.init();
    });
    /******/
})() ;
//# sourceMappingURL=new-card.js.map