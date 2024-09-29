$(function () {
    $(document).on("click", "#LoginBtn", function () {
        Login();
    });

});

// This will loging user.
function Login() {
    var UserCredential = {}
    UserCredential.Email = $("#emailInput").val();
    UserCredential.Password = $("#passwordInput").val();

    $.ajax({
        url: "../Account/Login",
        type: 'Post',
        dataType: "JSON",
        cache: false,
        data: UserCredential,
        success: function (response) {
            try {
                if (response.resultCode == 0) {
                    window.location = "../Dashboard/Dashboard";
                }
                else {
                    alert(response.resultDescription)
                }
            }
            catch (error) {
                console.error("Error occurred while processing response:", error);
                alert("Something is wrong")
            }
        },
        error: function (error) {
            console.error("Error occurred while processing response:", error);
            alert("Something is wrong")
        }
    })
}