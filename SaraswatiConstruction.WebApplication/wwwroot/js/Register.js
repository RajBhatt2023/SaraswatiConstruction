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


    $.ajax({
        url: "../Account/Registration",
        type: 'Post',
        dataType: "JSON",
        cache: false,
        data: UserDetail,
        success: function (response) {

            if (response.resultCode == 0) {

            }
        },
        error: function (data) {
        }
    });
}

