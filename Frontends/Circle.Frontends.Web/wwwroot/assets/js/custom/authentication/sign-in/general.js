/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
var __webpack_exports__ = {};
/*!*******************************************************************************************!*\
  !*** ../../../themes/metronic/html/demo1/src/js/custom/authentication/sign-in/general.js ***!
  \*******************************************************************************************/


// Class definition
var KTSigninGeneral = function() {
    // Elements
    var form;
    var submitButton;
    var validator;

    // Handle form
    var handleForm = function(e) {
        validator = FormValidation.formValidation(
			form,
			{
				fields: {					
					'email': {
                        validators: {
							notEmpty: {
								message: 'Zorunlu alan'
							},
                            emailAddress: {
								message: 'The value is not a valid email address'
							}
						}
					},
                    'password': {
                        validators: {
                            notEmpty: {
                                message: 'Zorunlu alan'
                            }
                        }
                    } 
				},
				plugins: {
					trigger: new FormValidation.plugins.Trigger(),
					bootstrap: new FormValidation.plugins.Bootstrap5({
                        rowSelector: '.fv-row'
                    })
				}
			}
		);		

        // Handle form submit
        submitButton.addEventListener('click', function (e) {
            // Prevent button default action
            e.preventDefault();

            // Validate form
            validator.validate().then(function (status) {
                if (status == 'Valid') {
                    // Show loading indication
                    submitButton.setAttribute('data-kt-indicator', 'on');

                    // Disable button to avoid multiple click 
                    submitButton.disabled = true;

                    // Prepare form data
                    var data = $(form).serialize();

                    PostData("/Login/Login", data).done(function (response) {
                        submitButton.removeAttribute("data-kt-indicator");
                        // Disable button to avoid multiple click 
                        submitButton.disabled = false;

                        if (response.IsSuccess) {
                            window.location.href = "/Home/Index";
                        }
                        else {
                            ShowErrorMessage("Hata Oluþtu",response.Message);
                        }
                    });
                }
            });
		});
    }

    // Public functions
    return {
        // Initialization
        init: function() {
            form = document.querySelector('#kt_sign_in_form');
            submitButton = document.querySelector('#kt_sign_in_submit');
            
            handleForm();
        }
    };
}();

// On document ready
KTUtil.onDOMContentLoaded(function() {
    KTSigninGeneral.init();
});
})();