$(function () {
    sessionStorage.clear();
    Validation();
});

function Validation() {
    // Custom email validation method
    $.validator.addMethod("customEmail", function (value) {
        return /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/.test(value);
    }, "Please enter a valid email address.");
    // Custom strong password validation method
    $.validator.addMethod("strongPass", function (value) {
        return /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,16}$/.test(value);
    }, "Please enter a strong password.");


    $("#registrationForm").validate({
        rules: {
            FirstNameReg: { 
                required: true
            },
            LastNameReg: {  
                required: true
            },
            EmailReg: { 
                required: true,
                customEmail: true
            },
            PasswordReg: { 
                required: true,
                strongPass: true
            },
            ConfirmPasswordReg: { 
                required: true,
                equalTo: "#InputPasswordReg" 
            }
        },
        messages: {
            FirstNameReg: {
                required: "Please enter your first name"
            },
            LastNameReg: {
                required: "Please enter your last name"
            },
            EmailReg: {
                required: "Please enter your email address",
                customEmail: "Please enter a valid email address"
            },
            PasswordReg: {
                required: "Please enter a password",
                strongPass: "Please enter a strong password."
            },
            ConfirmPasswordReg: {
                required: "Please enter confirm password",
                equalTo: "Passwords do not match"
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element);
            error.addClass("text-danger");
        }
    });
}

/* Registration function */
function Registration() {
    var isRegistrationFormValid = $('#registrationForm').valid();
    if (!isRegistrationFormValid) {
        return false;
    }
    $('#registrationForm').validate().resetForm();

    var UserDetail = {};
    UserDetail.FirstName = $("#InputFirstNameReg").val().charAt(0).toUpperCase() + $("#InputFirstNameReg").val().slice(1).toLowerCase();
    UserDetail.LastName = $("#InputLastNameReg").val().charAt(0).toUpperCase() + $("#InputLastNameReg").val().slice(1);
    UserDetail.Email = $("#InputEmailReg").val();
    UserDetail.Password = $("#InputPasswordReg").val();
    UserDetail.Url = getBaseUrl() + "/Account/VerifyEmail?token=";

    $.ajax({
        url: "../Account/Registration",
        type: 'Post',
        dataType: "JSON",
        cache: false,
        data: UserDetail,
        success: function (response) {
            try {
                if (response.resultCode === 0) {
                    window.location = "../Account/Login";
                }
            } catch (error) {
                console.error("Error occurred while processing the response:", error);
            }
        },
        error: function (error) {
            console.error("Error occurred while processing response:", error);
        }
    });
}
