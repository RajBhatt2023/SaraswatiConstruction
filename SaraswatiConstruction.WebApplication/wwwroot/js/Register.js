$(function () {
    sessionStorage.clear();
});

/*This function is used for SignUp*/
function Registration() {
    var UserDetail = {};
    UserDetail.FirstName = $("#txtFirstName").val().charAt(0).toUpperCase() + $("#txtFirstName").val().slice(1).toLowerCase();
    UserDetail.LastName = $("#txtLastName").val().charAt(0).toUpperCase() + $("#txtLastName").val().slice(1);
    UserDetail.Email = $("#txtEmail").val();
    UserDetail.Password = $("#txtPassword").val();
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
                    window.location = "../Account/Login"
                }
            }
            catch {

            }
        },
        error: function (error) {
            console.error("Error occurred while processing response:", error);
        }
    });
}

