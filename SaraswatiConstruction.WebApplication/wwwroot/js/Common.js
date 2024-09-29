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