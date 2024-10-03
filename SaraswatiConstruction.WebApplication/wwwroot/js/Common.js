function getBaseUrl() {
    var protocol = window.location.protocol;
    var host = window.location.host;
    var pathArray = window.location.pathname.split('/').filter(part => part.length > 0);

    var applicationName = '';
    if (pathArray.length > 2) {
        applicationName = pathArray.slice(0, pathArray.length - 2).join('/');
    }

    return protocol + "//" + host + (applicationName ? "/" + applicationName : "");
}
$(document).on('click', '.toggle-password', function () {
    $(this).toggleClass("fa-eye-slash fa-eye");
    var input = $($(this).attr("toggle"));
    if (input.attr("type") == "password") {
        input.attr("type", "text");
    } else {
        input.attr("type", "password");
    }
});