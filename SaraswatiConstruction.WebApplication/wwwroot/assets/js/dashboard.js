(function ($) {
    'use strict';
    $(function () {
        // Check if the element exists before accessing properties
        const proBanner = document.querySelector('#proBanner');
        const navbar = document.querySelector('.navbar');
        const pageBodyWrapper = document.querySelector('.page-body-wrapper');
        const bannerClose = document.querySelector('#bannerClose');

        if (proBanner && navbar) {
            if ($.cookie('skydash-pro-banner') != "true") {
                proBanner.classList.add('d-flex');
                navbar.classList.remove('fixed-top');
            } else {
                proBanner.classList.add('d-none');
                navbar.classList.add('fixed-top');
            }

            if ($(navbar).hasClass("fixed-top")) {
                pageBodyWrapper.classList.remove('pt-0');
                navbar.classList.remove('pt-5');
            } else {
                pageBodyWrapper.classList.add('pt-0');
                navbar.classList.add('pt-5');
                navbar.classList.add('mt-3');
            }

            if (bannerClose) {
                bannerClose.addEventListener('click', function () {
                    proBanner.classList.add('d-none');
                    proBanner.classList.remove('d-flex');
                    navbar.classList.remove('pt-5');
                    navbar.classList.add('fixed-top');
                    pageBodyWrapper.classList.add('pt-5');
                    navbar.classList.remove('mt-3');

                    var date = new Date();
                    date.setTime(date.getTime() + 24 * 60 * 60 * 1000);
                    $.cookie('skydash-pro-banner', "true", { expires: date });
                });
            }
        }
    });
})(jQuery);
